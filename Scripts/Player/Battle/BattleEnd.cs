using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleEnd : MonoBehaviour
{
    [SerializeField] Camera BattleCamera;
    [SerializeField] Camera[] playerCameras;
    [SerializeField] GameObject enemyTurn;
    [SerializeField] GameObject playerTurn;
    [SerializeField] GameObject battleManager;
    [SerializeField] Image[] removeColor;
    [SerializeField] GameObject battleEndImages;

    Transform parentTrans;
    public EnemyState[] enemyState;
    bool isUpate = false;

    private void OnEnable()
    {
        BattleCamera.gameObject.SetActive(true);
        StartCoroutine(setEnemyState());
    }

    private void OnDisable()
    {
        RemoveColor();
        Battle.playerTurn = 1;
        //BattleCamera.gameObject.SetActive(true);
        BattleCamera.gameObject.SetActive(false);
        battleManager.gameObject.SetActive(false);
        enemyTurn.SetActive(false);
        playerCameras[0].gameObject.SetActive(false);
        playerCameras[1].gameObject.SetActive(false);
        playerTurn.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isUpate) // BattleManager¿¡´ÙÇÏ¸é ¾÷µ¥ÀÌÆ® ¾È½áµµµÊ
        {
            if (enemyState[0].enemyHp <= 0 && enemyState[1].enemyHp <= 1 && enemyState[2].enemyHp <= 0)
            {
                int Exp = 0;
                //int Gold = 0;
                for(int i=0; i<enemyState.Length;i++)
                {
                    //Gold += enemyState[i].enemyGold;
                    Exp += enemyState[i].enemyExp;
                }

                for (int i = 0; i < 2; i++)
                {
                    PlayerStateManager.Instance.player[i].currentExp += Exp;
                    PlayerStateManager.Instance.LevelUp(i);
                }

                ResultItem();
                Destroy(EnemyState.hitEnemySave);
                battleEndImages.SetActive(true);
                isUpate = false;
                Debug.Log("ÀüÅõ ½Â¸®");
            }
            if (PlayerStateManager.Instance.player[0].currenthp <= 0 && PlayerStateManager.Instance.player[1].currenthp <= 0)
            {
                battleEndImages.SetActive(true);
                isUpate = false;
                Debug.Log("Àü¸ê");
            }
        }
    }

    void RemoveColor()
    {
        for (int i = 0; i < removeColor.Length; i++)
        {
            Color color;
            color = removeColor[i].color;
            color = new Color(0, 0, 0);
            removeColor[i].color = color;
        }
    }

    IEnumerator setEnemyState()
    {
        yield return new WaitForSeconds(1.0f);
        isUpate = true;
        parentTrans = GameObject.Find("SpawnScriptsObj").transform;
        enemyState = parentTrans.GetComponentsInChildren<EnemyState>();
    }

    [SerializeField] Text resultText;
    [SerializeField] GameObject battleGetItem;

    void ResultItem()
    {
        Inventory inventory = Inventory.Instance;
        int ran = Random.Range(0, 101);
        resultText.text = null;

        if (ran <= 50 && ran >= 0)
        {
            Item item;
            item = Resources.Load<Item>("HpPostion");
            inventory.AddItemInventory(item);
            resultText.text += "È¸º¹Æ÷¼Ç È¹µæ\n";
        }

        if(ran>=50 && ran<=101)
        {
            Item item;
            item = Resources.Load<Item>("MpPostion");
            inventory.AddItemInventory(item);
            resultText.text += "¸¶³ªÆ÷¼Ç È¹µæ\n";
        }

        int Gold = 0;
        for (int i = 0; i < enemyState.Length; i++)
        {
            Gold += enemyState[i].enemyGold;
        }
        resultText.text += "°ñµå " + Gold.ToString() + " È¹µæ\n";
        PlayerStateManager.Instance.Gold += Gold;

        battleGetItem.SetActive(true);
    }
}
