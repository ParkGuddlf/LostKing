using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picket : MonoBehaviour//맵에 설명을 판넬식 애니메이션
{
    Animator ani;

    private void Start()
    {
        ani = GetComponent<Animator>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch (gameObject.tag)
            {
                case "Picket":
                    transform.GetChild(0).gameObject.SetActive(true);
                    break;
                case "SavePoint":
                    ani.SetTrigger("SavePoint");
                    break;
            }
            
        }
           
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch (gameObject.tag)
            {
                case "Picket":
                    transform.GetChild(0).gameObject.SetActive(false);
                    break;
            }
        }       
    }
}
