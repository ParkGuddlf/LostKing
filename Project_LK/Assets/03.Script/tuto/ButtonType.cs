using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonType : MonoBehaviour
{
    public bool laver;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Keycap":
                laver = true;
                collision.transform.position = transform.position;
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Keycap":
                laver = false;
                break;
        }
    }
}
