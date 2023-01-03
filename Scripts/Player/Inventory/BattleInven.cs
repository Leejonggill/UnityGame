using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInven : MonoBehaviour
{
    public InventorySlot[] slots;
    public Inventory inventory;
    int j;

    private void Awake()
    {
        slots = transform.GetComponentsInChildren<InventorySlot>();
        inventory = Inventory.Instance;
    }

    void OnEnable()
    {
        for (int i = 0; i < slots.Length; i++) // 이걸해줘야 아이템을 다사용하고 이미지를 지워줌.
        {
            slots[i].RemoveSlot();
        }
        j = 0;

        for (int i = 0; i < 20; i++)
        {
            if (inventory.slots[i].item != null)
            {
                if (inventory.slots[i].item.itemType == Item.ItemType.USEITEM)
                {             
                    slots[j].AddItem(inventory.slots[i].item, inventory.slots[i].itemCount);
                    j++;
                }
            }
        }
        //j = 0;
        //for (int i = 0; i < 20; i++)
        //{
        //    if (inventory.slots[i].item.itemType == Item.ItemType.USEITEM)
        //    {
        //        j++;
        //        slots[j].AddItem(inventory.slots[i].item, inventory.slots[i].itemCount);
        //    }
        //}
    }

    void OnDisable()
    {
        j = 0;
        for (int i = 0; i < 20; i++)
        {
            if (inventory.slots[i].item != null)
            {
                if (inventory.slots[i].item.itemType == Item.ItemType.USEITEM)
                {
                    inventory.slots[i].AddItem(slots[j].item, slots[j].itemCount);
                    j++;
                }
            }
        }
    }

}
