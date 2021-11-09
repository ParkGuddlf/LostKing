using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;
    AudioSource audioSource;
    public OpenClear openClear;
    private void Start()
    {
        
        openClear = FindObjectOfType<OpenClear>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            for (int i = 0; i < openClear.coin.Count; i++)
            {
                openClear.coin.RemoveAt(i);
            }
            Destroy(gameObject,0.1f);
        }
    }
}
