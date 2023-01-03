using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Image[] mei_bar;
    [SerializeField] Text[] levelText;
    [SerializeField] Image[] male_bar;
    PlayerStateManager playerState;


    void Start()
    {
        playerState = PlayerStateManager.Instance;
    }

    void Update()
    {
        SetUI();
    }

    void SetUI()
    {
        levelText[0].text = "Lv "+playerState.player[0].level;
        mei_bar[0].fillAmount = (float)playerState.player[0].currenthp / playerState.player[0].maxHp;
        mei_bar[1].fillAmount = (float)playerState.player[0].currentMp / playerState.player[0].maxMp;
        mei_bar[2].fillAmount = (float)playerState.player[0].currentExp/ playerState.player[0].maxExp;

        levelText[1].text = "Lv " + playerState.player[1].level;
        male_bar[0].fillAmount = (float)playerState.player[1].currenthp / playerState.player[1].maxHp;
        male_bar[1].fillAmount = (float)playerState.player[1].currentMp / playerState.player[1].maxMp;
        male_bar[2].fillAmount = (float)playerState.player[1].currentExp / playerState.player[1].maxExp;
    }
}
