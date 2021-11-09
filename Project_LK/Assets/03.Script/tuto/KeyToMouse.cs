using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeyToMouse : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum KeyCount {left, right, space}
    public KeyCount keyCount;
    public static GameObject draggingItem = null;
    [SerializeField] Transform mapTr;
    [SerializeField] Transform keyListTr;
    Transform itemTr;
    CanvasGroup cg;
    Player player;
    Image img;

    public GameObject key;
    public Sprite[] keySprite;

    string startParentname;
    public void OnBeginDrag(PointerEventData eventData)
    {        
        if (player.isDie)
            return;
        //이동키 이미지의 알파값을 255
        ImgColor(255);
        //키오브젝트를 비활성화
        key.SetActive(false); 
        //오브젝트의 부모를 mapTr로 바꾼다
        transform.SetParent(mapTr);
        //draggingItem을 현재 게임오브젝트로 집어 넣는다
        draggingItem = this.gameObject;
        //레이케스터블록을 비활성화
        cg.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //마우스 포지션을 자신의 포지션으로 변경   
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //이동키 이미지의 알파값을 0
        ImgColor(0);
        //키오브젝트를 활성화
        key.SetActive(true);
        //키오브젝트의 위치 설정
        //카메라에서 보이는 마우스위치를 월드 포지션으로 변경
        Vector3 camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        key.transform.position = new Vector3(camPos.x, camPos.y, 0);
        //드래그 종료
        draggingItem = null;
        cg.blocksRaycasts = true;
        //이동키를 드래그엔 드롭으로 원위치 시킬때
        if (itemTr.parent == mapTr && startParentname == eventData.pointerCurrentRaycast.gameObject.name)
            transform.SetParent(keyListTr);
    }

    private void Start()
    {
        //초기화
        img = GetComponent<Image>();
        itemTr = GetComponent<Transform>();
        cg = GetComponent<CanvasGroup>();
        player = FindObjectOfType<Player>();

        key.transform.position = Camera.main.ScreenToWorldPoint(transform.position);
        
        switch (keyCount)
        {
            case KeyCount.left:
                startParentname = transform.parent.name;
                break;
            case KeyCount.right:
                startParentname = transform.parent.name;
                break;
            case KeyCount.space:
                startParentname = transform.parent.name;
                break;

        }
    }

    private void Update()
    {
        if (transform.parent == keyListTr)
        {
            ImgColor(255);
            key.SetActive(false);
            ImgChange();
        }
        //카메라 밖으로 나가도 위치고정
        if(transform.parent.name == "PanelA")
        {
            transform.position = Camera.main.WorldToScreenPoint(key.transform.position);
        }
        
    }

    void ImgColor(int alpha)
    {
        Color color = img.color;
        color.a = alpha;
        img.color = color;

    }

    void ImgChange()
    {
        if (draggingItem == null)
        {
            switch (keyCount)
            {
                case KeyCount.left:
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                        img.sprite = keySprite[1];
                    else if(Input.GetKeyUp(KeyCode.LeftArrow))
                        img.sprite = keySprite[0];
                    break;
                case KeyCount.right:
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                        img.sprite = keySprite[1];
                    else if (Input.GetKeyUp(KeyCode.RightArrow))
                        img.sprite = keySprite[0];
                    break;
                case KeyCount.space:
                    if (Input.GetKeyDown(KeyCode.Space))
                        img.sprite = keySprite[1];
                    else if (Input.GetKeyUp(KeyCode.Space))
                        img.sprite = keySprite[0];
                    break;
            }            
        }

            
    }
}
