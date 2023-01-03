using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ItemText ItemText;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Item item = GetComponent<InventorySlot>().item;
        
        if(item!=null)
        {
            ItemText.gameObject.SetActive(true);
            ItemText.SetUpToolTip(item.itemName);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemText.gameObject.SetActive(false);
    }
}
