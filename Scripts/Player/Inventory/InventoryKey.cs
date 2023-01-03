using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryKey : MonoBehaviour
{
    [SerializeField] GameObject[] playerObj = new GameObject[2];
    [SerializeField] GameObject inventoryObj;
    [SerializeField] GameObject[] showPlayer;

    bool isObj = false;
    public static int SelectKey;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isObj)
            {
                SelectKey = PlayerStateManager.playerSelect;
                playerObj[0].SetActive(false);
                playerObj[1].SetActive(false);
                inventoryObj.SetActive(true);
                isObj = true;
                SetCharacter();
                Time.timeScale = 0.0f;
            }
            else if (isObj)
            {
                Time.timeScale = 1.0f;
                playerObj[0].SetActive(true);
                playerObj[1].SetActive(true);
                inventoryObj.SetActive(false);
                isObj = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftControl)&&isObj)
        {
            if (SelectKey == 1)
                SelectKey = 2;
            else if (SelectKey == 2)
                SelectKey = 1;
            SetCharacter();
        }
    }

    void SetCharacter()
    {
        if (SelectKey == 1)
        {
            showPlayer[0].SetActive(true);
            showPlayer[1].SetActive(false);
        }
        else if (SelectKey == 2)
        {
            showPlayer[1].SetActive(true);
            showPlayer[0].SetActive(false);
        }
    }
}
