using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCountUi : MonoBehaviour
{
    [SerializeField] OpenClear coin;
    [SerializeField] Text text;
    [SerializeField] int coinMax;
    [SerializeField] bool isCount;
    void Start()
    {
        text = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isCount)
        {
            coin = FindObjectOfType<OpenClear>();
            coinMax = coin.coin.Count;
            isCount = true;
        }
        if (coinMax > 0)
            text.text = coinMax - coin.coin.Count + "/" + coinMax; //반대로 올려주시 앞에 숫자
        else
            gameObject.SetActive(false);
    }
}
