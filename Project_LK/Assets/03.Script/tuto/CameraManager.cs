using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform playerTr;
    public float moveSpeed;
    public BoxCollider2D bounder;

    Vector3 minBound, maxBound;
    Vector3 targetPos;

    float halfWidth, halfHight;

    private void Update()
    {
        SetBounder();
        minBound = bounder.bounds.min;
        maxBound = bounder.bounds.max;
        halfHight = Camera.main.orthographicSize;
        halfWidth = Screen.width * halfHight / Screen.height;
    }    

    // Update is called once per frame
    void LateUpdate()
    {        
        targetPos.Set(playerTr.position.x, playerTr.position.y+2, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

        //카메라 일정범위안에 가두기 x,y값을 길이의 절반값을 더하여서 카메라가 나가는것을 방지한다
        float clampX = Mathf.Clamp(transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
        float ClampY = Mathf.Clamp(transform.position.y, minBound.y + halfHight, maxBound.y - halfHight);

        transform.position = new Vector3(clampX, ClampY, transform.position.z);
    }

    void SetBounder()
    {
        if(bounder == null)
        {
            bounder = GameObject.FindWithTag("Bounder").GetComponent<BoxCollider2D>();
        }
    }
}
