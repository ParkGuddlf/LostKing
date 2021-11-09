using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField] enum Type { drop, push, razer } //투사체 발사만들기 마리오 쿵쿵이
public class Trap : MonoBehaviour
{
    [SerializeField] Type type;
    Rigidbody2D rigi;
    [SerializeField] float rayDir;//함정의 사정거리
    [SerializeField] Sprite[] sprArray;//낙하물함정 그림변화
    SpriteRenderer sprRen;
    bool isDrop;
    float startRay;//레이저용거리측정

    Vector2 startPos;

    void Start()
    {
        switch (type)
        {
            case Type.drop:
                sprRen = GetComponent<SpriteRenderer>();
                rigi = GetComponent<Rigidbody2D>();
                startPos = transform.position;
                break;
            case Type.push:
                break;
            case Type.razer:
                startRay = rayDir;
                break;

        }
        
    }

    void Update()
    {
        switch (type)
        {
            case Type.drop:
                //레이를 쏘고 레이가 감지되면 자식오브젝트를 아래로 내려보낸다
                RaycastHit2D ray = Physics2D.BoxCast(transform.position ,new Vector2(2,rayDir),0,new Vector2(0,1),0.5f, LayerMask.GetMask("Player"));
                
                if (ray.collider != null)
                {
                    rigi.isKinematic = false;                    
                    isDrop = false;
                }
                else if(ray.collider == null && isDrop)
                {
                    rigi.isKinematic =true;                    
                    transform.position = startPos;
                }
                break;
            case Type.push:
                break;
            case Type.razer:
                StartCoroutine(Razer());
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Keycap")//낙하오브젝트 그림변경
        {
            sprRen.sprite = sprArray[1];
            StartCoroutine(PositionChange());
        }
    }

    IEnumerator PositionChange()
    {
        yield return new WaitForSeconds(1f);
        sprRen.sprite = sprArray[0];
        transform.position = startPos;
        isDrop = true;
    }
    enum Direction//방향 아직사용X
    {
        Left, Right, Down, Up
    }

    [SerializeField] bool onoff;

    IEnumerator Razer()//레이저 감지
    {
        Debug.DrawRay(transform.position, Vector2.down * rayDir, Color.white);
        if (onoff)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, rayDir, ~(LayerMask.GetMask("Bounder")));
            if (hit.collider != null)
            {
                rayDir = Mathf.Abs(transform.position.y - hit.point.y) + 0.3f; 
                yield return new WaitForSeconds(0.2f);
                if (hit.collider.tag == "Player")
                {
                    hit.collider.GetComponent<Player>().isDie = true;                    
                }
            }
            else
            {
                rayDir = startRay;
            }

        }
        transform.GetChild(0).GetComponent<SpriteRenderer>().size = new Vector2(1, rayDir);
        yield return new WaitForSeconds(0.2f);
    }
}
