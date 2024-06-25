using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Spawn_Reward : MonoBehaviour
{
    [Header("Reward")]
    public GameObject[] Reward;

    private void Start()
    {
        Vector3 vec = new Vector3(0, 1, 0);
        int k = Random.Range(0, Reward.Length);
        GameObject temp =  Instantiate(Reward[k], transform.position + vec, Quaternion.identity);
        temp.transform.parent = this.transform;
        if (k != 0)
        {
            temp.AddComponent<Box_script>();
            temp.AddComponent<AudioSource>();
            AudioClip audio = Resources.Load<AudioClip>("Assets/에셋스토어/sound asset/wood-smash-3-170418.mp3");
            temp.GetComponent<Box_script>().breaksound = audio;
            AudioSource audioS = temp.GetComponent<AudioSource>();
            audioS.clip = audio;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 1f);
    }
}
