using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title_char : MonoBehaviour
{
    Vector3 startPos;
    Animator anim;    
    int speed =3;

    public RectTransform canvasRt;
    RectTransform tagBoxRt;

    void Start()
    {
        
        startPos = transform.position;
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (transform.position.x >= 7.3)
        {
            int spriteNum = Random.Range(0, 2);
            anim.SetInteger("AniNum", spriteNum);
            transform.position = startPos;
        }        
    }
    public void NextScene()
    {
        StartCoroutine(ReadtToOverScene());
        SceneManager.LoadScene("Tutorial");
    }
    IEnumerator ReadtToOverScene()
    {
        yield return new WaitForSeconds(0.5f);
        //speed =0 kingJump Ani
        //fadeOut
    }
    public void ButtonON(GameObject a)
    {
        a.SetActive(true);
    }
    public void ButtonOFF(GameObject a)
    {
        a.SetActive(false);
    }   
   
}
