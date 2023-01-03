using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInventory : MonoBehaviour
{
    public InventorySlot[] slots;
    public Inventory inventory;

    private void Awake()
    {
        slots = transform.GetComponentsInChildren<InventorySlot>();
        inventory = Inventory.Instance;
    }

    void OnEnable()
    {
        //PlayerStateManager.Instance.LoadItem();
        for (int i = 0; i < 20; i++)
        {
            slots[i].AddItem(inventory.slots[i].item, inventory.slots[i].itemCount);
        }

        StartCoroutine(UpdateCorutine());
    }

    IEnumerator UpdateCorutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        for (int i = 0; i < 20; i++)
        {
            inventory.slots[i].AddItem(slots[i].item, slots[i].itemCount);
        }
        yield return null;
        StartCoroutine(UpdateCorutine());
    }

    void OnDisable()
    {
        for (int i = 0; i < 20; i++)
        {
            inventory.slots[i].AddItem(slots[i].item, slots[i].itemCount);
        }
    }

    //void UnEquinmentItem(Item _item) // ���������������� ���°���
    //{
    //    Debug.Log("����");
    //    switch(_item.itemName)
    //    {
    //        case "Helmet":
    //            Debug.Log("���");
    //            PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].def -= 5;
    //            break;
    //        case "Helmet2":
    //            Debug.Log("���");
    //            PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].def -= ;
    //            break;
    //        case "Armor":
    //            PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].def -= 5;
    //            break;
    //        case "Pants":
    //            PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].def -= 5;
    //            break;
    //        case "Sword":
    //            PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].str -= 5;
    //            break;
    //    }
    //}

    public void AddItemInventory2(int _nSelect) // ������ �߰�
    {
        switch (_nSelect)
        {
            case 0:
                Debug.Log("������");
                Add(_nSelect);
                break;
            case 1:
                Add(_nSelect);
                break;
            case 2:
                Add(_nSelect);
                break;
            case 3:
                Add(_nSelect);
                break;
        }
    }

    void Add(int _nSelect) // �κ��丮�� ���������������� �߰�
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].itesms[_nSelect]); // ���������ϸ� �κ��丮�� �߰�
                slots[i].UnEquinmentItem(PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].itesms[_nSelect]);
                PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].itesms[_nSelect] = null; // �÷��̾� ������ ����.
                return;
            }
        }
    }
    //private void Update()
    //{
    //    SetInven();
    //}

    //void SetInven()
    //{
    //    for (int i = 0; i < 20; i++)
    //    {
    //        if (slots[i].item != inventory.slots[i].item ||
    //            slots[i].itemCount!=inventory.slots[i].itemCount)
    //        {
    //            Debug.Log("����");
    //            //slots[i].item = inventory.slots[i].item;
    //            //slots[i].itemCount = inventory.slots[i].itemCount;
    //            //slots[i].itemImage.sprite = inventory.slots[i].itemImage.sprite;

    //            //inventory.slots[i].item = slots[i].item;
    //            ////            inventory.slots[i].itemCount = slots[i].itemCount;
    //            ////            inventory.slots[i].itemImage.sprite = slots[i].itemImage.sprite;
    //        }
    //    }
    //}
}
