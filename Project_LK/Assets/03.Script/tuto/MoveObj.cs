using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObj : MonoBehaviour
{
    protected enum moveType { Auto, Button, playerSensor }//사라지는발판,점핑오브젝트
    [SerializeField] moveType setMoveType;
    protected enum eDirection { Left, Right, Up, Down, Diaganal }
    [SerializeField] eDirection setDirection;

    Vector2 Direct(eDirection eDirection)//방향설정
    {
        switch (eDirection)
        {
            case eDirection.Left:
                return Vector2.left;
            case eDirection.Right:
                return Vector2.right;
            case eDirection.Up:
                return Vector2.up;
            case eDirection.Down:
                return Vector2.down;
            default:
                return Vector2.zero;
        }
    }
    [SerializeField] float speed;
    float Speed { get { return this.speed * Time.deltaTime; } }//움직이는 속도

    [SerializeField] float distance;//거리
    public Vector2 myPos;
    public Vector2 frontPoint;//+최대거리
    public Vector2 backPoint;//-최대거리


    [SerializeField] float waitTime;//끝에 도착했을때 딜레이시간
    public bool turnOnOff;//버튼방식 버튼 온오프
    public GameObject buttonType;//연결버트

    [SerializeField] bool isPlayer;//플레이어센싱용 참거짓

    Rigidbody2D rigi;
    private void Start()
    {
        switch (setMoveType)
        {
            case moveType.Auto:
                Moveplat();
                break;
            case moveType.Button:
                rigi = GetComponent<Rigidbody2D>();
                this.myPos = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y);
                SetPosB();
                break;
            case moveType.playerSensor:
                rigi = GetComponent<Rigidbody2D>();                
                this.SetPos();
                break;
        }

    }
    void Update()
    {
        switch (setMoveType)
        {
            case moveType.Auto:
                break;
            case moveType.Button:
                MovePlanB();
                break;
            case moveType.playerSensor:
                RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.up, 1, LayerMask.GetMask("Player"));
                if (raycastHit2D.collider)
                {
                    this.myPos = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y);
                    MovePlayerSensor();
                }
                break;
        }

    }

    #region Auto
    void Moveplat()
    {
        this.myPos = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y);
        this.SetPos();
        StartCoroutine(Move());
    }
    void SetPos()
    {
        var dis = this.distance / 2;
        switch (this.setDirection)
        {
            case eDirection.Left:
                this.frontPoint = new Vector2(this.transform.localPosition.x - dis, this.transform.localPosition.y);
                this.backPoint = new Vector2(this.transform.localPosition.x + dis, this.transform.localPosition.y);
                break;
            case eDirection.Right:
                this.frontPoint = new Vector2(this.transform.localPosition.x + dis, this.transform.localPosition.y);
                this.backPoint = new Vector2(this.transform.localPosition.x - dis, this.transform.localPosition.y);
                break;
            case eDirection.Up:
                this.frontPoint = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y + dis);
                this.backPoint = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y - dis);
                break;
            case eDirection.Down:
                this.frontPoint = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y - dis);
                this.backPoint = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y + dis);
                break;
        }
    }
    IEnumerator Move()
    {
        while (true)
        {
            if (distance != 0)
            {
                while (true)
                {
                    this.transform.Translate(this.Direct(setDirection) * this.Speed);
                    var dis = Vector2.Distance(new Vector2(this.transform.localPosition.x, this.transform.localPosition.y), this.frontPoint);
                    if (dis <= 0.2f)
                        break;
                    yield return null;
                }
                yield return new WaitForSeconds(waitTime);
                while (true)
                {
                    this.transform.Translate(-this.Direct(setDirection) * this.Speed);
                    var dis = Vector2.Distance(new Vector2(this.transform.localPosition.x, this.transform.localPosition.y), this.backPoint);
                    if (dis <= 0.2f)
                        break;
                    yield return null;
                }
                yield return new WaitForSeconds(waitTime);
            }

        }
    }
    #endregion
    #region Button
    void SetPosB()
    {
        var dis = this.distance;

        switch (this.setDirection)
        {
            case eDirection.Left:
                this.frontPoint = new Vector2(this.transform.localPosition.x - dis, this.transform.localPosition.y);
                break;
            case eDirection.Right:
                this.frontPoint = new Vector2(this.transform.localPosition.x + dis, this.transform.localPosition.y);
                break;
            case eDirection.Up:
                this.frontPoint = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y + dis);
                break;
            case eDirection.Down:
                this.frontPoint = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y - dis);
                break;

        }
    }
    Vector3 newPos;
    void MovePlanB()
    {
        turnOnOff = buttonType.GetComponent<ButtonType>().laver;
        if (turnOnOff)
        {
            if (Vector3.Distance(transform.position, frontPoint) < 0.2)
                newPos = Vector3.zero;
            newPos = Vector3.MoveTowards(transform.position, frontPoint, Speed);
            rigi.MovePosition(newPos);
        }
        else if (!turnOnOff)
        {
            if (Vector3.Distance(transform.position, myPos) < 0.2)
                newPos = Vector3.zero;
            newPos = Vector3.MoveTowards(transform.position, myPos, Speed);
            rigi.MovePosition(newPos);
        }
    }
    #endregion
    #region PlayerSensor

    int i = 1;//방향돌리기 플레이어센서
    void MovePlayerSensor()
    {
        
        if (i == 1)
        {
            if (Vector3.Distance(transform.position, frontPoint) < 0.2)
            {
                newPos = Vector3.zero;
                i = 0;
            }
            newPos = Vector3.MoveTowards(transform.position, frontPoint, Speed);
            rigi.MovePosition(newPos);
        }
        else if (i == 0)
        {
            if (Vector3.Distance(transform.position, backPoint) < 0.2)
            {
                newPos = Vector3.zero;
                i = 1;
            }
            newPos = Vector3.MoveTowards(transform.position, backPoint, Speed);
            rigi.MovePosition(newPos);
        }
    }    
    #endregion
}

