using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NodeOrigin : MonoBehaviour
{
    public List<GameObject> nodesList = new List<GameObject>();
    public float distanceFromOrigin = 5f;

    private void Start()
    {
        GameObject[] nodeObjects = GameObject.FindGameObjectsWithTag("Node");
        foreach (GameObject nodeObject in nodeObjects)
        {
            nodesList.Add(nodeObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ArrangeNodes();
        }
    }
    
    void ArrangeNodes()
    {
        int totalNodes = nodesList.Count;
        if (totalNodes == 0)
            return;

        Vector3 originPosition = transform.position;

        // Shuffle the nodes randomly
        for (int i = 0; i < totalNodes; i++)
        {
            GameObject temp = nodesList[i];
            int randomIndex = Random.Range(i, totalNodes);
            nodesList[i] = nodesList[randomIndex];
            nodesList[randomIndex] = temp;
        }

        // Calculate the pushing radius based on the average diameter of the nodes
        float pushingRadius = 0.5f * distanceFromOrigin / totalNodes;

        // Position the nodes with repulsion and distance restriction
        for (int i = 0; i < totalNodes; i++)
        {
            Vector3 nodePosition = nodesList[i].transform.position;
            Vector3 newPosition = originPosition + Random.insideUnitSphere * distanceFromOrigin * 0.95f;

            for (int j = 0; j < i; j++)
            {
                Vector3 otherPosition = nodesList[j].transform.position;
                float distanceBetweenNodes = Vector3.Distance(newPosition, otherPosition);

                if (distanceBetweenNodes < pushingRadius)
                {
                    // Calculate repulsion direction
                    Vector3 repulsionDirection = (newPosition - otherPosition).normalized;
                    float repulsionForce = (pushingRadius - distanceBetweenNodes) / pushingRadius;

                    // Apply repulsion force
                    newPosition += repulsionDirection * repulsionForce * pushingRadius;
                }
            }

            // Limit the node position within the distance from the node origin
            newPosition = originPosition + Vector3.ClampMagnitude(newPosition - originPosition, distanceFromOrigin);

            nodesList[i].transform.position = newPosition;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceFromOrigin);
    }
}
