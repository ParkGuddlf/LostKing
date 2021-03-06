using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    [SerializeField] Texture2D cursor;
    void Start()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
