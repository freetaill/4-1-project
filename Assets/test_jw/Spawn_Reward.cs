using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Reward : MonoBehaviour
{
    [Header("Reward")]
    public GameObject[] Reward;

    private void Start()
    {
        Vector3 vec = new Vector3(0, 1, 0);
        GameObject.Instantiate(Reward[0], transform.position + vec, Quaternion.identity).transform.parent = this.transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 1f);
    }
}
