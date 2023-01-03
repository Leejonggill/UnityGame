using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRemoveClick : MonoBehaviour
{
    [SerializeField] GameObject playerTurn;
    [SerializeField] GameObject itemTurn;

    public void OnRemoveInven()
    {
        playerTurn.SetActive(true);
        itemTurn.SetActive(false);
        gameObject.SetActive(false);
    }
}
