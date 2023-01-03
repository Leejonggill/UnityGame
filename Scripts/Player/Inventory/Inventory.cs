using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region test
    //private static bool isDestory = false;

    //private void Awake()
    //{
    //    if (isDestory == false)
    //    {
    //        isDestory = true;
    //        DontDestroyOnLoad(gameObject);
    //        return;
    //    }
    //    else if (isDestory == true)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    #endregion

    private static Inventory instance = null;

    public static Inventory Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //public Transform itemParentTransform;
    public int slotMax = 20;
    public InventorySlot[] slots;// static

    private void Start()
    {
        //slots = itemParentTransform.transform.GetComponentsInChildren<InventorySlot>();
        slots = transform.GetComponentsInChildren<InventorySlot>();
    }

    public void AddItemInventory(Item _item)
    {
        if (Item.ItemType.USEITEM == _item.itemType) // ���������̶�� 
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    Debug.Log("���Լ���2");
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].ItemCountPlus();
                        return;
                    }
                }
            }
        }

        for (int i = 0; i<slots.Length;i++)
        {
            if(slots[i].item==null)
            {
                Debug.Log("���Լ���");
                slots[i].AddItem(_item);
                return;
            }
        }
    }
}

    //public Transform itemParentTransform;// ������ �������Ʈ������ �̰� �ν����Ϳ� �־�����
    //public InventorySlot[] showSlots; //  itemParentTransform.transform.GetComponentsInChildren<InventorySlot>();

    //public void OnUseSlotItem(int Select) // ���콺Ŭ���� ���Ծȿ� ��ư���ٰ� ���� 
    //{
    //    if (slots[Select].item.itemType == Item.ItemType.USEITEM)  // ���ο� ��ũ��Ʈ ���� �ִ°���õ
    //    {
    //        switch (slots[Select].item.itemName)
    //        {

    //            case "HpPostion":
    //                Debug.Log("use Item");
    //                PlayerStateManager.Instance.HpResult(InventoryKey.SelectKey - 1, 50);
    //                break;
    //            case "MpPostion":
    //                PlayerStateManager.Instance.MpResult(InventoryKey.SelectKey - 1, 50);
    //                break;
    //        }
    //        slots[Select].itemCount--;
    //        showSlots[Select].itemCount--;
    //        showSlots[Select].SetText();
    //        slots[Select].SetText();
    //        if (slots[Select].itemCount <= 0)
    //        {
    //            slots[Select].RemoveSlot();
    //            showSlots[Select].RemoveSlot();
    //        }
    //    }
    //}

