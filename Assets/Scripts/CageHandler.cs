using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CageHandler : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Something has been placed in the cage");

        if (eventData.pointerDrag.transform.CompareTag("Animal"))
        {
            Debug.Log("Cage is full");
        }
        
    }

}
