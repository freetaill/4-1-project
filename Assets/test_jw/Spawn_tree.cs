using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_tree : MonoBehaviour
{
    [Header("Tree")]
    public GameObject[] tree;

    private void Start()
    {
        Instantiate(tree[0], transform.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1f);
    }
}
