using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotalDoor : MonoBehaviour
{
    enum InOut { IN, OUT }
    [SerializeField] InOut inout;
    public Transform potalPos;
    //페이드인아웃 오브젝트
    GameObject fadeOut;

    private void Start()//포탈이미지만들기
    {
        fadeOut = FindObjectOfType<CanvasGroup>().gameObject;
        switch (inout)
        {
            case InOut.IN:
                potalPos = transform.GetChild(0).transform;
                break;
            case InOut.OUT:
                potalPos = transform.parent.transform;
                break;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = FindObjectOfType<Player>();
        if (Input.GetKeyDown(KeyCode.UpArrow) && collision.tag == "Player" && player.isGround)
        {
            //페이드인아웃 찾아서 코루틴으로 실행
            StartCoroutine(FadeOut());
            collision.transform.position = potalPos.position;//이동하고
            StartCoroutine(FadeIn());
        }
    }
    IEnumerator FadeOut()
    {
        //못움직이게 잠그고
        GameManager.instance.playerHold = false;
        //어두어지고
        fadeOut.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(2.5f);

    }
    IEnumerator FadeIn()
    {
        float fadeColorA = 1;
        //다시밝아진다
        while (fadeColorA > 0)
        {
            fadeColorA -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            fadeOut.GetComponent<Image>().color = new Color(0, 0, 0, fadeColorA);
        }
        GameManager.instance.playerHold = true;
    }
}
