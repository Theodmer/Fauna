using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class RigidBodyOrigin : MonoBehaviour
{
    public List<GameObject> nodesList = new List<GameObject>();
    public float effectiveDistanceFromOrigin;
    private float distanceFromOrigin = 20f;
    [Range(.01f, 10f)] public float scale = 1f;
    private void Start()
    {
        GatherNodes();
        effectiveDistanceFromOrigin = distanceFromOrigin * scale;
    }

    private void Update()
    {
        effectiveDistanceFromOrigin = distanceFromOrigin * scale;
        SetPushingRadius();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ArrangeNodes();
        }

        #region scale

        transform.localScale = new Vector3(scale, scale, scale);

        #endregion
    }

    private void GatherNodes()
    {
        nodesList.Clear();
        GameObject[] nodeObjects = GameObject.FindGameObjectsWithTag("Node");
        foreach (GameObject nodeObject in nodeObjects)
        {
            nodesList.Add(nodeObject);
        }
    }

    private void SetPushingRadius()
    {
        int totalNodes = nodesList.Count;
        float bigSphereVolume = 4f / 3f * Mathf.PI * Mathf.Pow(effectiveDistanceFromOrigin, 3);
        float smallSphereVolume = .65f * bigSphereVolume / totalNodes;
        float smallSphereRadius = Mathf.Pow(smallSphereVolume * (3f / (4f * Mathf.PI)), 1f / 3f);
        for (int i = 0; i < totalNodes; i++)
        {
            nodesList[i].GetComponent<SphereCollider>().radius = smallSphereRadius;
        }
    }

    private void ArrangeNodes()
    {
        int totalNodes = nodesList.Count;
        if (totalNodes == 0)
            return;

        Vector3 originPosition = transform.position;

        for (int i = 0; i < totalNodes; i++)
        {
            Vector3 newPosition = originPosition + Random.insideUnitSphere * effectiveDistanceFromOrigin * 0.95f;
            newPosition = originPosition + Vector3.ClampMagnitude(newPosition - originPosition, effectiveDistanceFromOrigin);
            nodesList[i].transform.position = newPosition;
        }
    }
}
