using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenUI : MonoBehaviour
{
    PlayerStateManager playerState;
    [SerializeField] Text GoldText;
    [SerializeField] Transform mei;
    [SerializeField] Transform male;


    Text[] stateText_mei;
    Text[] stateText_male;

    private void Awake()
    {
        playerState = PlayerStateManager.Instance;
        stateText_mei = mei.GetComponentsInChildren<Text>();
        stateText_male = male.GetComponentsInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        SetInvenUi();
    }

    private void SetInvenUi()
    {
        stateText_mei[0].text = "ü�� :" + playerState.player[0].currenthp + "/" + playerState.player[0].maxHp;
        stateText_mei[1].text = "���� :" + playerState.player[0].currentMp + "/" + playerState.player[0].maxMp;
        stateText_mei[2].text = "���ݷ� :" + playerState.player[0].str;
        stateText_mei[3].text = "���� :" + playerState.player[0].def;
        stateText_mei[4].text = "ȸ���� :" + playerState.player[0].Evaison;
        stateText_mei[5].text = "����ġ :" + playerState.player[0].currentExp + "/" + playerState.player[0].maxExp;

        stateText_male[0].text = "ü�� :" + playerState.player[1].currenthp + "/" + playerState.player[1].maxHp;
        stateText_male[1].text = "���� :" + playerState.player[1].currentMp + "/" + playerState.player[1].maxMp;
        stateText_male[2].text = "���ݷ� :" + playerState.player[1].str;
        stateText_male[3].text = "���� :" + playerState.player[1].def;
        stateText_male[4].text = "ȸ���� :" + playerState.player[1].Evaison;
        stateText_male[5].text = "����ġ :" + playerState.player[1].currentExp + "/" + playerState.player[1].maxExp;

        GoldText.text = "���� ��� : " + playerState.Gold;
    }
}
