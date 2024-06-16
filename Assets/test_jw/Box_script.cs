using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box_script : MonoBehaviour
{
    bool isPlayerEnter, isCollided = false;
    [SerializeField] private Text gold;
    AudioSource breakBox;
    public AudioClip breaksound;
    int goldAmount;
    // Start is called before the first frame update
    void Start()
    {
        isPlayerEnter = false;
        breakBox = this.gameObject.GetComponent<AudioSource>();
        goldAmount = Random.Range(10000, 50000);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerEnter && !isCollided)
        {
            GameManager.instance.player.Add_coin(goldAmount);
            breakBox.Play();
            //Destroy(gameObject, breakBox.clip.length);
            OnDestroy();
            isCollided = true;
        }
    }

    void OnDestroy()
    {
        Destroy(gameObject);
        PlaySound();
    }

    void PlaySound()
    {
        if (breaksound != null)
        {
            AudioSource.PlayClipAtPoint(breaksound, transform.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            isPlayerEnter = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            isPlayerEnter = false;
        }
    }
}
