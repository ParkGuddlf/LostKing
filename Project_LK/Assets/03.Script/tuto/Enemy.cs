using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]int rayDir;
    [SerializeField] bool flip;
    public bool isDie;
    SpriteRenderer sprRen;
    void Start()
    {
        sprRen = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDie)
        {
            transform.Translate(new Vector2(rayDir * 2 * Time.deltaTime, 0));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(rayDir, 0), 1f, ~(LayerMask.GetMask("Bounder") | LayerMask.GetMask("Player")| LayerMask.GetMask("Enemy")));
            if (hit.collider != null)
            {
                flip = !flip;
                sprRen.flipX = flip;
            }
            switch (flip)
            {
                case true:
                    rayDir = -1;
                    break;
                case false:
                    rayDir = 1;
                    break;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Trap")
        {
            
        }
    }
}
