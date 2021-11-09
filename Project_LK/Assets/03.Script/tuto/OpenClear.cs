using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClear : MonoBehaviour
{

    public List<GameObject> coin = new List<GameObject>();
    [SerializeField] GameObject[] coinasd;
    AudioSource audioSou;
    private void Awake()
    {
        audioSou = GetComponent<AudioSource>();
        coinasd = GameObject.FindGameObjectsWithTag("Coin");
        for (int i = 0; i < coinasd.Length; i++)
        {
            coin.Add(coinasd[i]);
        }
    }
    private void Update()
    {

        if (coin.Count == 0)
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            audioSou.Play();
        }
    }
}
