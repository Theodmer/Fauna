using UnityEngine;

public class NodeConnection : MonoBehaviour
{
    public GameObject nodeOriginObject;
    public LineRenderer lineRenderer;
    public Material lineMaterial;
    
    private NodeOrigin nodeOrigin;

    private void Start()
    {
        CreateConnections();
        nodeOrigin = nodeOriginObject.GetComponent<NodeOrigin>();
    }

    private void CreateConnections()
    {
        int totalNodes = nodeOrigin.nodesList.Count;

        for (int i = 0; i < totalNodes; i++)
        {
            for (int j = i + 1; j < totalNodes; j++)
            {
                GameObject nodeA = nodeOrigin.nodesList[i];
                GameObject nodeB = nodeOrigin.nodesList[j];

                // Check if nodes are within the connection distance
                float distance = Vector3.Distance(nodeA.transform.position, nodeB.transform.position);
                if (distance <= nodeOrigin.distanceFromOrigin)
                {
                    // Create a line renderer component for the connection
                    GameObject connection = new GameObject("Connection");
                    connection.transform.SetParent(transform);

                    LineRenderer line = connection.AddComponent<LineRenderer>();
                    line.material = lineMaterial;

                    // Set the start and end positions of the line
                    line.SetPositions(new Vector3[] { nodeA.transform.position, nodeB.transform.position });

                    // Customize line appearance as desired (e.g., color, width, etc.)
                    line.startWidth = 0.1f;
                    line.endWidth = 0.1f;
                }
            }
        }
    }
}