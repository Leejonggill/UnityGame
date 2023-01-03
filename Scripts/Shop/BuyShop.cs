using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyShop : MonoBehaviour
{
    [SerializeField] protected Text goldText;
    public Item[] postion;
    protected int hpPostionPrice = 50;
    protected int mpPostionPrice = 50;

    protected PlayerStateManager playerGold2;
    protected Inventory playerInventory;

    private void Start()
    {
        playerGold2 = PlayerStateManager.Instance;
        playerInventory = Inventory.Instance;
    }

    private void OnEnable()
    {
        StartCoroutine(setGoldText());
    }

    public virtual void OnSelectShopButton(int Select)
    {
        switch (Select)
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
                if (playerGold2.Gold >= mpPostionPrice)
                {
                    playerGold2.Gold -= 50;
                    playerInventory.AddItemInventory(postion[Select]);
                    goldText.text = "¼ÒÁö°ñµå :" + playerGold2.Gold;
                }
                break;
        }
    }

    IEnumerator setGoldText()
    {
        yield return new WaitForSeconds(0.1f);
        goldText.text = "¼ÒÁö°ñµå :" + playerGold2.Gold;
    }
}
