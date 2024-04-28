using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Monster : MonoBehaviour
{
    [Header("Monster")]
    public GameObject[] Monster;

    private void Start()
    {
        //Instantiate(Monster[0], transform.position, Quaternion.identity);
    }

    private void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, 1f);
    }
}
