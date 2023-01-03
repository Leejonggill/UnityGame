using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemShopEventTool : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] ItemText itemText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Item item = GetComponent<ItemShopToolTip>().item;

        if(item!=null)
        {
            itemText.gameObject.SetActive(true);
            itemText.SetUpToolTip(item.itemName);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemText.gameObject.SetActive(false);
    }
}
