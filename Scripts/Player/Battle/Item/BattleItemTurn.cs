using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleItemTurn : MonoBehaviour
{
    [SerializeField] GameObject mainCam;
    [SerializeField] GameObject BattleManager;
    [SerializeField] GameObject ItemTurn;
    [SerializeField] GameObject slotObj;
    [SerializeField] GameObject playerTurn;
    [SerializeField] GameObject info;
    [SerializeField] GameObject[] selectCamreas;
    private void OnEnable()
    {
        playerTurn.SetActive(false);
        slotObj.SetActive(true);
    }

    private void OnDisable()
    {
        slotObj.SetActive(false);
    }

    public void OnUsedItem()
    {
        info.SetActive(false);
        ItemTurn.SetActive(false);
        mainCam.SetActive(true);

        selectCamreas[0].SetActive(false);
        selectCamreas[1].SetActive(false);

        BattleManager.SetActive(true);
        Battle.playerTurn++;
    }
}
