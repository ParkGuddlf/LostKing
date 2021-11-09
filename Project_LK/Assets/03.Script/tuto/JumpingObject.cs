using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingObject : MonoBehaviour
{
    //레이를 쏘고 레이에 감지되고 콜라이더가 다았으면 점프시긴다 
    Player player;
    RaycastHit2D hitPlayer;
    Animator anim;
    bool ahitPlayer;
    void Start()
    {
        player = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    private void Update()
    {
       ahitPlayer = Physics2D.OverlapCircle(transform.GetChild(0).transform.position, 0.1f, LayerMask.GetMask("Player"));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && ahitPlayer)
        {
            player.GetComponent<AudioSource>().clip = player.clips[0];
            player.GetComponent<AudioSource>().Play();
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, 8);
            anim.SetBool("isJump", true);
            Invoke("Animoff", 0.5f);
           // hitPlayer.collider.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 8);
        }
    }
    void Animoff()
    {
        anim.SetBool("isJump", false);
    }
}
