using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("JUMP-----------------")]
    public float jumpForce;
    public Transform feetPos;
    public float circleRadius;
    public LayerMask whatIsGround;
    public float jumpTime;
    float jumpCount;
    bool isjump;
    public bool isGround;

    [Header("Move-----------------")]
    int speed;
    bool onLadder;
    public float climbSpeed;

    [Header("State-----------------")]
    public bool isClear;
    public bool isDie;

    float RightArrowDown = 1;
    float LeftArrowDown = -1;
    public GameObject[] keyLock;

    [Header("Audio-----------------")]
    public AudioClip[] clips;

    SpriteRenderer spren;
    Rigidbody2D rigi;
    Animator ani;
    AudioSource audioSource;

    void Start()
    {
        spren = GetComponent<SpriteRenderer>();
        rigi = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isDie || isClear || !GameManager.instance.playerHold)
            return;        
        Move();
        Jump();
        EnemyHit();
        Ladder();
    }

    void Move()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            speed = 1;
            ani.SetBool("isWalk", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            speed = -1;
            ani.SetBool("isWalk", true);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            ani.SetBool("isWalk", false);
        }
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, new Vector3(speed, 0, 0), 0.5f, LayerMask.GetMask("Ground"));        

        if (hitGround.collider == null )
        {
            //이동키가 맵에 배치되어있으면 키 잠금
            if (Input.GetKey(KeyCode.RightArrow) && keyLock[1].transform.childCount != 0)
            {
                transform.Translate(new Vector2(RightArrowDown * 3 * Time.deltaTime, 0));
                spren.flipX = false;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && keyLock[0].transform.childCount != 0)
            {
                transform.Translate(new Vector2(LeftArrowDown * 3 * Time.deltaTime, 0));
                spren.flipX = true;
            }
        }

    }
    void Jump()
    {
        //점프키가 맵에 배치되있으면 키 잠금
        if (keyLock[2].transform.childCount != 0)
        {
            isGround = Physics2D.OverlapCircle(feetPos.position, circleRadius, whatIsGround);
            //플레이어가 땅에 닫고있으면
            if (isGround && Input.GetKeyDown(KeyCode.Space))
            {
                isjump = true;
                jumpCount = jumpTime;
                ani.SetBool("isJump", true);
                audioSource.clip = clips[0];
                audioSource.Play();
                rigi.velocity = new Vector2(rigi.velocity.x, jumpForce);
            }

            //스페이스바 누른시간에 따라 점프높이가 달라진다
            if (isjump && Input.GetKey(KeyCode.Space))
            {
                if (jumpCount > 0)
                {
                    rigi.velocity = new Vector2(rigi.velocity.x, jumpForce);
                    jumpCount -= Time.deltaTime;
                }
                else if (jumpCount < 0)
                    isjump = false;
            }
            if (Input.GetKeyUp(KeyCode.Space))
                isjump = false;
        }

            
    }
    //적을 밟았을떄
    void EnemyHit()
    {
        RaycastHit2D hitEnemy = Physics2D.CircleCast(feetPos.position, circleRadius*2,Vector2.down,0, LayerMask.GetMask("Enemy"));
        if(hitEnemy.collider != null)
        {
            rigi.velocity = new Vector2(rigi.velocity.x, jumpForce);
            hitEnemy.collider.GetComponent<Enemy>().isDie = true;
            hitEnemy.collider.GetComponent<SpriteRenderer>().flipY = true;
            hitEnemy.collider.GetComponent<BoxCollider2D>().enabled = false;
            
        }
    }
    void Ladder()
    {
        switch (onLadder)
        {
            case true:
                rigi.velocity = Vector2.zero;
                rigi.gravityScale = 0f;
                ani.SetBool("Ladder", true);
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.Translate(new Vector2(0, climbSpeed*Time.deltaTime));
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.Translate(new Vector2(0, -climbSpeed * Time.deltaTime));
                }
                break;
            case false:
                rigi.gravityScale = 1f;
                ani.SetBool("Ladder", false);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Trap":
                isDie = true;
                break;
            case "SavePoint":
                GameManager.instance.savePoint = transform.position;
                break;
            case "Ladder":
                onLadder = true;
                break;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Ladder":
                onLadder = true;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Ladder":                
                onLadder = false;
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground" :
                ani.SetBool("isJump", false);
                break;
            case "Keycap":
                ani.SetBool("isJump", false);
                break;
            case "End":
                isClear = true;
                break;
            case "Trap":
                isDie = true;
                break;
        }
    }
}
