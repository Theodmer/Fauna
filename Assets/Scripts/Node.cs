using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private List<Node> neighbors = new List<Node>();
    private void Update()
    {
        float parentscale = transform.parent.localScale.x;
        transform.localScale = new Vector3(1f / parentscale, 1f / parentscale, 1f / parentscale);
    }
    
    public List<Node> GetNeighbors()
    {
        return neighbors;
    }
    public void AddNeighbor(Node neighbor)
    {
        neighbors.Add(neighbor);
        neighbor.neighbors.Add(this);
    }
    public void RemoveNeighbor(Node neighbor)
    {
        neighbors.Remove(neighbor);
        neighbor.neighbors.Remove(this);
    }
    public void RemoveAllNeighbors()
    {
        foreach (Node neighbor in neighbors)
        {
            neighbor.neighbors.Remove(this);
        }
        neighbors.Clear();
    }
}
