using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapKey : MonoBehaviour
{
    public Transform UiKey;
    public Transform UiKeyparent;
    BoxCollider2D box;
    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }
    private void OnEnable()
    {
        box.isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player")
        {
            UiKey.SetParent(UiKeyparent);
        }else if (!(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player"))
        {
            box.isTrigger = false;
        }
    }
}
