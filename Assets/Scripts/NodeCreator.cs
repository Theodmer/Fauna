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
    public int numberOfNodesToCreate = 1;

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
        return nodeOrigin.transform.position + Random.insideUnitSphere * nodeOrigin.GetComponent<RigidBodyOrigin>().effectiveDistanceFromOrigin * 0.95f;
    }
}
