using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField] Image[] mei_bar;
    [SerializeField] Image[] male_bar;
    PlayerStateManager playerState;


    void Start()
    {
        playerState = PlayerStateManager.Instance;
    }

    void Update()
    {
        SetUI();
        //Debug.Log(playerState.player[0].currentMp);
    }

    void SetUI()
    {
        mei_bar[0].fillAmount = (float)playerState.player[0].currenthp / playerState.player[0].maxHp;
        mei_bar[1].fillAmount = (float)playerState.player[0].currentMp / playerState.player[0].maxMp;

        male_bar[0].fillAmount = (float)playerState.player[1].currenthp / playerState.player[1].maxHp;
        male_bar[1].fillAmount = (float)playerState.player[1].currentMp / playerState.player[1].maxMp;
    }
}
