using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharcter : MonoBehaviour
{
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;

    void Awake()
    {
        player1 = GameObject.Find("Player_Mei");
        player2 = GameObject.Find("Player_Male");
        if(PlayerStateManager.playerSelect==1)
        {
            player1.SetActive(true);
            if(player2.activeSelf)
            player2.SetActive(false);
        }
        else if(PlayerStateManager.playerSelect == 2)
        {
            if (player2.activeSelf)
                player1.SetActive(false);

            player2.SetActive(true);
        }
    }

}
