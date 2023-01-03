using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] GameObject[] skillSelectImages;
    [SerializeField] GameObject[] skillimages;
    [SerializeField] Image[] seleckBackGround;
    [SerializeField] Image[] seleckBackGround2;
    [SerializeField] GameObject playerTurn; // 스킬취소할떄
    [SerializeField] GameObject Attack;
    bool isSelectSkill = false;

    [SerializeField] Text[] Skill_text;
    [SerializeField] Text mpText;

    public static int selecSkill = 0;
    int selectCha;

    int[] player1MpCost;
    int[] player2MpCost;

    private void Start()
    {
        player1MpCost = new int[3];
        player1MpCost[0] = 15;
        player1MpCost[1] = 30;
        player1MpCost[2] = 10;

        player2MpCost = new int[3];
        player2MpCost[0] = 30;
        player2MpCost[1] = 15;
        player2MpCost[2] = 10;
    }

    private void OnEnable()
    {
        mpText.text = "";
        isSelectSkill = false;
        playerTurn.SetActive(false);
        selecSkill = 1;
        selectCha = Battle.playerTurn;
        skillimages[selectCha-1].SetActive(true);
    }

    private void OnDisable()
    {
        Skill_text[0].text = "Mp 30을 소모하여 공격력*2로 공격";
        Skill_text[1].text = "Mp 15을 소모하여 공격력*1.5로 공격";
        RemoveColor();
        skillSelectImages[selectCha - 1].transform.localPosition = new Vector3(763, 361, 0);
        skillimages[selectCha - 1].SetActive(false);
        if (!isSelectSkill)
        {
            selecSkill = 0;
            playerTurn.SetActive(true);
        }
        else
        {
            Attack.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SoundsManager.Instance.OnSelectKeySound();
            selecSkill--;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SoundsManager.Instance.OnSelectKeySound();
            selecSkill++;
        }
        SetColor();
        SetPostion();
        SetTextSkill();

        Debug.Log(skillSelectImages[selectCha - 1].transform.localPosition);
        if (Input.GetKeyDown(KeyCode.Space)&&PlayerStateManager.Instance.player[selectCha-1].currentMp >= player1MpCost[selecSkill-1]&&selectCha==1)
        {
            PlayerStateManager.Instance.player[selectCha - 1].currentMp -= player1MpCost[selecSkill - 1];
            isSelectSkill = true;
            gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && PlayerStateManager.Instance.player[selectCha - 1].currentMp >= player2MpCost[selecSkill - 1] && selectCha == 2)
        {
            PlayerStateManager.Instance.player[selectCha - 1].currentMp -= player2MpCost[selecSkill - 1];
            isSelectSkill = true;
            gameObject.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.Space) && PlayerStateManager.Instance.player[selectCha - 1].currentMp <= player1MpCost[selecSkill - 1] && selectCha == 1)
        {
            mpText.text = "마나가 부족합니다";
            StartCoroutine(textReset());
        }
        else if(Input.GetKeyDown(KeyCode.Space) && PlayerStateManager.Instance.player[selectCha - 1].currentMp <= player2MpCost[selecSkill - 1] && selectCha == 2)
        {
            mpText.text = "마나가 부족합니다";
            StartCoroutine(textReset());
        }


        if (Input.GetKeyDown(KeyCode.X))
        {
            isSelectSkill = false;
            gameObject.SetActive(false);
        }
    }

    IEnumerator textReset()
    {
        yield return new WaitForSeconds(1.0f);
        mpText.text = "";
    }

    void SetTextSkill()
    {
        switch (selecSkill)
        {
            case 1:
                if(selectCha - 1 == 0)
                {
                    Skill_text[0].text = "Mp 15을 소모하여 공격력*1.5로 공격";
                }   
                else if(selectCha - 1 == 1)
                {
                    Skill_text[1].text = "Mp 30을 소모하여 공격력*2로 공격";
                }
                break;
            case 2:
                if (selectCha - 1 == 0)
                {
                    Skill_text[0].text = "Mp 30을 소모하여 공격력*2로 공격";
                }
                else if (selectCha - 1 == 1)
                {
                    Skill_text[1].text = "Mp 15을 소모하여 공격력*1.5로 공격";
                }
                break;
            case 3:
                if (selectCha - 1 == 0)
                {
                    Skill_text[0].text = "Mp 10을 소모하여 모든공격 공격력*2.5증가";
                }
                else if (selectCha - 1 == 1)
                {
                    Skill_text[1].text = "Mp 10을 소모하여 전체 회복+50";
                }
                break;
        }
    }

    void SetPostion()
    {
        switch (selecSkill)
        {
            case 1:
                skillSelectImages[selectCha - 1].transform.localPosition = Vector3.Lerp(skillSelectImages[selectCha - 1].transform.localPosition,
                    new Vector3(763, 361, 0), Time.deltaTime * 9);
                break;
            case 2:
                skillSelectImages[selectCha - 1].transform.localPosition = Vector3.Lerp(skillSelectImages[selectCha - 1].transform.localPosition,
               new Vector3(763, 200, 0), Time.deltaTime * 9);
                break;
            case 3:
                skillSelectImages[selectCha - 1].transform.localPosition = Vector3.Lerp(skillSelectImages[selectCha - 1].transform.localPosition,
               new Vector3(763, 25, 0), Time.deltaTime * 9);
                break;
        }
    }

    void SetColor()
    {
        selecSkill = Mathf.Clamp(selecSkill, 1, 3);
        Debug.Log(selecSkill);
        if (selectCha - 1 == 0)
        {
            for (int i = 0; i < seleckBackGround.Length; i++)
            {
                if (selecSkill - 1 == i)
                {
                    seleckBackGround[i].color = Color.Lerp(seleckBackGround[i].color, new Color(203, 0, 0), 0.000025f);
                }
                else if (selecSkill - 1 != i)
                {
                    seleckBackGround[i].color = Color.Lerp(seleckBackGround[i].color, new Color(0, 0, 0), 0.025f);
                }
            }
        }
        else
        {
            for (int i = 0; i < seleckBackGround2.Length; i++)
            {
                if (selecSkill - 1 == i)
                {
                    seleckBackGround2[i].color = Color.Lerp(seleckBackGround2[i].color, new Color(161, 161, 161), 0.000025f);
                }
                else if (selecSkill - 1 != i)
                {
                    seleckBackGround2[i].color = Color.Lerp(seleckBackGround2[i].color, new Color(0, 0, 0), 0.025f);
                }
            }
        }
    }

    void RemoveColor()
    {
        for (int i = 0; i < seleckBackGround.Length; i++)
        {
            seleckBackGround[i].color = new Color(0, 0, 0);
                seleckBackGround2[i].color = new Color(0, 0, 0);
        }
    }
}
