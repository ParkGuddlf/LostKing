using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAtk : MonoBehaviour
{
    Rigidbody2D rigi;
    public float x;
    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        Destroy(gameObject, 15);
    }

    private void Update()
    {
        transform.Translate(new Vector2(-3 * Time.deltaTime, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Keycap")
            Destroy(gameObject);
    }
    IEnumerator MoveFloor()
    {
        while (true)
        {
            rigi.velocity = Vector2.right * -x * Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
