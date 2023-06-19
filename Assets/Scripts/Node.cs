using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private void Update()
    {
        float parentscale = transform.parent.localScale.x;
        transform.localScale = new Vector3(1f / parentscale, 1f / parentscale, 1f / parentscale);
    }
}
