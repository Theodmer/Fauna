using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NodeConnection : MonoBehaviour
{
    public GameObject nodeOriginObject;
    public RigidBodyOrigin nodeOrigin;
    public Material lineMaterial;
    public List<GameObject> connections = new List<GameObject>();

    private void Start()
    {
        nodeOrigin = nodeOriginObject.GetComponent<RigidBodyOrigin>();
    }

    private void Update()
    {
        UpdateLines();
    }

    private void CreateRandomConnections()
    {
        int totalNodes = nodeOrigin.nodesList.Count;

        for (int i = 0; i < totalNodes; i++)
        {
            Node nodeA = nodeOrigin.nodesList[i].GetComponent<Node>();

            for (int j = i + 1; j < totalNodes; j++)
            {
                Node nodeB = nodeOrigin.nodesList[j].GetComponent<Node>();

                // Randomly connect nodes based on a probability (adjust as desired)
                float connectProbability = 0.5f;
                if (Random.value <= connectProbability)
                {
                    nodeA.AddNeighbor(nodeB);
                }
            }
        }
    }
    

    // private void UpdateLines()
    // {
    //     List<Node> nodesAlreadyConnected = new List<Node>();
    //     for (int i = 0; i < nodeOrigin.nodesList.Count; i++)
    //     {
    //         Node node = nodeOrigin.nodesList[i].GetComponent<Node>();
    //
    //         foreach (Node neighbor in node.GetNeighbors())
    //         {
    //             if (nodesAlreadyConnected.Contains(neighbor))
    //                 continue;
    //             for (int c = 0; c < neighbor.transform.childCount; c++)
    //             {
    //                 neighbor.transform.GetChild(c).GetComponent<LineRenderer>().
    //                     SetPositions(new Vector3[] { node.transform.position, neighbor.transform.position });
    //             }
    //         }
    //         nodesAlreadyConnected.Add(node);
    //     }
    // }
    private void UpdateLines()
    {
        List<Node> nodesAlreadyConnected = new List<Node>();
        for (int i = 0; i < nodeOrigin.nodesList.Count; i++)
        {
            Node node = nodeOrigin.nodesList[i].GetComponent<Node>();

            foreach (Node neighbor in node.GetNeighbors())
            {
                if (nodesAlreadyConnected.Contains(neighbor))
                    continue;
                for (int c = 0; c < neighbor.transform.childCount; c++)
                {
                    neighbor.transform.GetChild(c).GetComponent<LineRenderer>().
                        SetPositions(new Vector3[] { node.transform.position, neighbor.transform.position });
                }
            }
            nodesAlreadyConnected.Add(node);
        }
    }
}