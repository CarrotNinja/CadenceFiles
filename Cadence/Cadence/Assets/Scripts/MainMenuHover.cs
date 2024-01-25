using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;// Required when using Event data.
public class MainMenuHover : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    //Do this when the cursor enters the rect area of this selectable UI object.
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("The cursor entered the selectable UI element.");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("The Cursor has left the selectable UI elemennt.");
    }
}