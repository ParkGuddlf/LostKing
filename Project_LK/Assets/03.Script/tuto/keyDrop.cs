using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class keyDrop : MonoBehaviour, IDropHandler
{
    public GameObject[] key;
    //block istriiger true
    public void OnDrop(PointerEventData eventData)
    {
        KeyToMouse.draggingItem.transform.SetParent(this.transform);
    }
}
