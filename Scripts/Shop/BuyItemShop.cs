using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItemShop : BuyShop
{
    public override void OnSelectShopButton(int Select)
    {
        switch(Select)
        {
            case 0:
                if (playerGold2.Gold >= hpPostionPrice)
                {
                    playerGold2.Gold -= 50;
                    Debug.Log(PlayerStateManager.Instance.Gold);
                    playerInventory.AddItemInventory(postion[Select]);
                    goldText.text = "¼ÒÁö°ñµå :" + playerGold2.Gold;
                }
                break;
            case 1:
                if (playerGold2.Gold >= hpPostionPrice)
                {
                    playerGold2.Gold -= 50;
                    Debug.Log(PlayerStateManager.Instance.Gold);
                    playerInventory.AddItemInventory(postion[Select]);
                    goldText.text = "¼ÒÁö°ñµå :" + playerGold2.Gold;
                }
                break;
            case 2:
                if (playerGold2.Gold >= hpPostionPrice)
                {
                    playerGold2.Gold -= 50;
                    Debug.Log(PlayerStateManager.Instance.Gold);
                    playerInventory.AddItemInventory(postion[Select]);
                    goldText.text = "¼ÒÁö°ñµå :" + playerGold2.Gold;
                }
                break;
            case 3:
                if (playerGold2.Gold >= hpPostionPrice)
                {
                    playerGold2.Gold -= 50;
                    Debug.Log(PlayerStateManager.Instance.Gold);
                    playerInventory.AddItemInventory(postion[Select]);
                    goldText.text = "¼ÒÁö°ñµå :" + playerGold2.Gold;
                }
                break;
            case 4:
                if (playerGold2.Gold >= hpPostionPrice)
                {
                    playerGold2.Gold -= 75;
                    Debug.Log(PlayerStateManager.Instance.Gold);
                    playerInventory.AddItemInventory(postion[Select]);
                    goldText.text = "¼ÒÁö°ñµå :" + playerGold2.Gold;
                }
                break;
            case 5:
                if (playerGold2.Gold >= hpPostionPrice)
                {
                    playerGold2.Gold -= 75;
                    Debug.Log(PlayerStateManager.Instance.Gold);
                    playerInventory.AddItemInventory(postion[Select]);
                    goldText.text = "¼ÒÁö°ñµå :" + playerGold2.Gold;
                }
                break;
            case 6:
                if (playerGold2.Gold >= hpPostionPrice)
                {
                    playerGold2.Gold -= 75;
                    Debug.Log(PlayerStateManager.Instance.Gold);
                    playerInventory.AddItemInventory(postion[Select]);
                    goldText.text = "¼ÒÁö°ñµå :" + playerGold2.Gold;
                }
                break;
            case 7:
                if (playerGold2.Gold >= hpPostionPrice)
                {
                    playerGold2.Gold -= 75;
                    Debug.Log(PlayerStateManager.Instance.Gold);
                    playerInventory.AddItemInventory(postion[Select]);
                    goldText.text = "¼ÒÁö°ñµå :" + playerGold2.Gold;
                }
                break;
        }
    }
}
