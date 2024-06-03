using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public Transform target;
    GameObject obj;

    NavMeshAgent nmAgent;

    bool isHit = false;
    int goldAmount;

    // Start is called before the first frame update
    void Start()
    {
        this.AddComponent<NavMeshAgent>();
        gameObject.AddComponent<Rigidbody>();
        nmAgent = GetComponent<NavMeshAgent>();
        nmAgent.stoppingDistance= 2;
        obj = GameObject.FindWithTag("player");
        goldAmount = Random.Range(100, 500);
    }

    // Update is called once per frame
    void Update()
    {
        //네비게이션으로 플레이어 추적
        target = obj.transform;
        nmAgent.SetDestination(target.position);
        if(isHit)
        {
            GameManager.instance.player.Add_coin(goldAmount);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hit_range"))
        {
            isHit = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hit_range"))
        {
            isHit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hit_range"))
        {
            isHit = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 20f);
    }
}
