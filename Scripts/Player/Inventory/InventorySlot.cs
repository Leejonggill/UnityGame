using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item;
    public int itemCount;
    public Image itemImage;

    [SerializeField] Text itemCountText;
    [SerializeField] GameObject buttonObject;

    bool isOpenButton = false;
    bool isUse = false;

    private void OnEnable()
    {
        if (buttonObject.activeSelf)
        {
            buttonObject.SetActive(false);
        }

    }

    public void SetText()
    {
        itemCountText.text = itemCount.ToString();
    }

    public void AddItem(Item _item, int _itemCount = 1)
    {
        if (_item == null && _itemCount == 0)
        {
            RemoveSlot();
            return;
        }

        itemImage.gameObject.SetActive(true);
        item = _item;
        itemCount = _itemCount;
        itemImage.sprite = _item.itemImage;

        if (item.itemType != Item.ItemType.EQUIPMENTITEM)
        {
            itemCountText.gameObject.SetActive(true);
            itemCountText.text = itemCount.ToString();
        }
        else
        {
            Debug.Log("아이템 추가");
            itemCountText.gameObject.SetActive(false);
        }
    }

    public void SetActiveButton()
    {
        itemImage.gameObject.SetActive(true);
    }

    public void ItemCountPlus()
    {
        itemCount++;
        itemCountText.text = itemCount.ToString();
    }
    public void RemoveSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        itemCountText.text = itemCount.ToString();
        itemCountText.gameObject.SetActive(false);
        itemImage.gameObject.SetActive(false);
    }

    //public void RemoveSlotEquiment(int _nSelect)
    //{
    //    if(_nSelect==0)
    //    AddItem(PlayerStateManager.Instance.player[0].itesms[_nSelect]);
    //}

    public void UseSlotItem() // 래이캐스트로도가능 마우스클릭시 hitinfo.itemType 이렇게 // 새로운 스크립트 만들어서 넣는걸추천
    {
        if (item.itemType == Item.ItemType.USEITEM)
        {
            int selectkey = InventoryKey.SelectKey - 1;
            switch (item.itemName)
            {
                case "HpPostion":
                    Debug.Log("use Item");
                    //if (PlayerStateManager.Instance.player[selectkey].currenthp ==
                    //    PlayerStateManager.Instance.player[selectkey].maxHp)
                    //    return;
                    SoundsManager.Instance.OnPostionSound();
                    PlayerStateManager.Instance.HpResult(InventoryKey.SelectKey - 1, 50);
                    break;
                case "MpPostion":
                    //                if (PlayerStateManager.Instance.player[selectkey].currentMp ==
                    //PlayerStateManager.Instance.player[selectkey].maxMp)
                    //                    return;
                    SoundsManager.Instance.OnPostionSound();
                    PlayerStateManager.Instance.MpResult(InventoryKey.SelectKey - 1, 50);
                    break;
            }
            itemCount--;
            itemCountText.text = itemCount.ToString();
            if (itemCount <= 0)
            {
                RemoveSlot();
            }
        }
        else if (item.itemType == Item.ItemType.EQUIPMENTITEM)
        {
            ShowEquiment showEquiment = GameObject.Find("InventoryGird").GetComponent<ShowEquiment>(); // 이렇게 사용가능
            switch (item.itemName)
            {
                case "Helmet":
                    // if문 추가시켜서 player가 아이템을 끼고있다면 실행 안되게 해야됨.
                    // 그리고 헬멧을 벗을때 if(플레이어아이템=="Helmet") def -=5; 플레이어아이템 = null; 
                    ItemEnquiment(0);
                    break;
                case "Helmet2":
                    ItemEnquiment(0);
                    break;
                case "Armor":
                    ItemEnquiment(1);
                    break;
                case "Armor2":
                    ItemEnquiment(1);
                    break;
                case "Pants":
                    ItemEnquiment(2);
                    break;
                case "Pants2":
                    ItemEnquiment(2);
                    break;
                case "Sword":
                    ItemEnquiment(3);
                    break;
                case "Sword2":
                    ItemEnquiment(3);
                    break;

            }
            if (isUse)
            {
                isUse = false;
                showEquiment.SetEquimentSlots();
                itemCount--;
                if (itemCount <= 0)
                {
                    RemoveSlot();
                }
            }
        }
    }

    public void BattleUseSlotItem() // 래이캐스트로도가능 마우스클릭시 hitinfo.itemType 이렇게 // 새로운 스크립트 만들어서 넣는걸추천
    {
        if (item.itemType == Item.ItemType.USEITEM)
        {
            int selectkey = Battle.playerTurn - 1;
            switch (item.itemName)
            {
                case "HpPostion":
                    Debug.Log("use Item");
                    SoundsManager.Instance.OnPostionSound();
                    PlayerStateManager.Instance.HpResult(selectkey, 50);
                    break;
                case "MpPostion":
                    SoundsManager.Instance.OnPostionSound();
                    PlayerStateManager.Instance.MpResult(selectkey, 50);
                    break;
            }
            itemCount--;
            itemCountText.text = itemCount.ToString();
            if (itemCount <= 0)
            {
                RemoveSlot();
            }
        }
    }

    public void OnSetButton()
    {
        if (item != null && !isOpenButton)
        {
            isOpenButton = true;
            buttonObject.SetActive(true);
        }
        else if (isOpenButton)
        {
            isOpenButton = false;
            buttonObject.SetActive(false);
        }
    }

    public void OnChangeItemSlot()
    {
        if(item==null)
        {
            Debug.Log("리턴");
            return;
        }

        switch (item.itemName)
        {
            case "Helmet": // 장비창으로해도되고 지금만든건 인벤토리창이 Helmet일떄 실행.
                ItemChange(0);
                break;
            case "Helmet2":
                ItemChange(0);
                break;
            case "Armor":
                ItemChange(1);
                break;
            case "Armor2":
                ItemChange(1);
                break;
            case "Pants":
                ItemChange(2);
                break;
            case "Pants2":
                ItemChange(2);
                break;
            case "Sword":
                ItemChange(3);
                break;
            case "Sword2":
                ItemChange(3);
                break;
        }
    }

    void ItemEnquiment(int _nNum)
    {
        if(_nNum>3)
        {
            Debug.Log("범위값 초과");
            return;
        }

        if (PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].itesms[_nNum] == null)
        {
            PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].itesms[_nNum] = item; // 아이템 추가
            PlayerStateManager.Instance.ItemSet(InventoryKey.SelectKey - 1, _nNum); // 아이템 꼇는지 안꼇는지 갱신 
            isUse = true;
        }
    }

    void ItemChange(int _nNum)
    {
        if(_nNum>3)
        {
            Debug.Log("범위값 초과");
            return;
        }

        if (PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].itesms[_nNum] != null)
        {
            ShowEquiment showEquiment = GameObject.Find("InventoryGird").GetComponent<ShowEquiment>(); // 위에다선언하고
            // showEquiment가 Null일떄 showEquiment = GameObject.Find("InventoryGird").GetComponent<ShowEquiment>(); 해도됨
            Debug.Log("교체");
            UnEquinmentItem(PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].itesms[_nNum]);
            showEquiment.slots[_nNum].AddItem(item);
            AddItem(PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].itesms[_nNum]);
            PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].itesms[_nNum] = showEquiment.slots[_nNum].item;
            PlayerStateManager.Instance.ItemSet(InventoryKey.SelectKey - 1, _nNum);
            showEquiment.SetEquimentSlots();
        }
    }

    public void UnEquinmentItem(Item _item) // 아이템장착해제시 방어력감소
    {

        switch (_item.itemName)
        {
            case "Helmet":
                PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].helmet = Helmet.None;
                PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].def -= 5;
                break;
            case "Helmet2":
                PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].helmet = Helmet.None;
                PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].def -= 7;
                break;
            case "Armor":
                PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].armor = Armor.None;
                PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].def -= 5;
                break;
            case "Armor2":
                PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].armor = Armor.None;
                PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].def -= 7;
                break;
            case "Pants":
                PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].pants = Pants.None;
                PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].def -= 5;
                break;
            case "Pants2":
                PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].pants = Pants.None;
                PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].def -= 7;
                break;
            case "Sword":
                PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].sword = Sword.None;
                PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].str -= 5;
                break;
            case "Sword2":
                PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].sword = Sword.None;
                PlayerStateManager.Instance.player[InventoryKey.SelectKey - 1].str -= 7;
                break;
        }
    }
}
