using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;
using Random = UnityEngine.Random;

public class NodeCreator : MonoBehaviour
{
    public GameObject nodePrefab;
    public GameObject nodeOrigin;
    public GameObject nodeConnection;
    public int numberOfNodesToCreate = 1;
    public Material lineMaterial;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateNodes();
        }
    }
    
    private void CreateNodes()
    {
        for (int i = 0; i < numberOfNodesToCreate; i++)
        {
            GameObject node = Instantiate(nodePrefab, findRandomPosition(), Quaternion.identity);
            AddNodeToList(node);
            MakeChild(node, nodeOrigin);
            CreateRandomConnections(node.GetComponent<Node>());
            CreateLines(node.GetComponent<Node>());
        }
    }
    void MakeChild(GameObject childObject, GameObject parentObject)
    {
        childObject.transform.SetParent(parentObject.transform);
    }
    
    private void AddNodeToList(GameObject node)
    {
        nodeOrigin.GetComponent<RigidBodyOrigin>().nodesList.Add(node);
    }
    
    private Vector3 findRandomPosition()
    {
        return nodeOrigin.transform.position + Random.insideUnitSphere * (nodeOrigin.GetComponent<RigidBodyOrigin>().effectiveDistanceFromOrigin * 0.95f);
    }
    
    private void CreateRandomConnections(Node node)
    {
        List<GameObject> nodesList = nodeOrigin.GetComponent<RigidBodyOrigin>().nodesList;
        int totalNodes = nodesList.Count;

        for (int j = 0; j < totalNodes; j++)
        {
            Node nodeB = nodesList[j].GetComponent<Node>();

            // Randomly connect nodes based on a probability (adjust as desired)
            float connectProbability = 0.5f;
            if (Random.value <= connectProbability)
            {
                node.AddNeighbor(nodeB);
                nodeConnection.GetComponent<NodeConnection>().connections.Add(nodeB.gameObject);
            }
        }
    }
    
    private void CreateLines(Node node)
    {
        List<Node> neighbors = node.GetNeighbors();

        foreach (Node neighbor in neighbors)
        {
            // Create a line renderer component for the connection
            GameObject connection = new GameObject("Connection");
            connection.transform.SetParent(neighbor.transform);
            
            LineRenderer line = connection.AddComponent<LineRenderer>();
            line.material = lineMaterial;

            // Set the start and end positions of the line
            line.SetPositions(new Vector3[] { node.transform.position, neighbor.transform.position });

            // Customize line appearance as desired (e.g., color, width, etc.)
            line.startWidth = 0.1f;
            line.endWidth = 0.1f;

            line.enabled = true; // Enable the line renderer to make it visible in the game
        }
    }
}
