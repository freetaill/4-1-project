using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_tree : MonoBehaviour
{
    [Header("Tree")]
    public GameObject[] tree;

    private void Start()
    {
        GameObject.Instantiate(tree[0], transform.position, Quaternion.identity).transform.parent = this.transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1f);
    }
}
