using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattelManager : MonoBehaviour
{
    [SerializeField] GameObject BattleCamera;
    [SerializeField] GameObject mei_Camera;
    [SerializeField] GameObject male_Camera;
    [SerializeField] GameObject playerTurn;
    [SerializeField] Image[] removeColor;
    [SerializeField] GameObject enemyBattleManager;

    bool isFirts = false;

    private void OnEnable()
    {
        Debug.Log("완료");
        RemoveColor();
        if (!isFirts)
        {
            UnSetCamera(BattleCamera);
            SetCamera(mei_Camera);
            SetCamera(playerTurn);
            isFirts = true;
            gameObject.SetActive(false);
        }
        else if (isFirts)
        {
            StartCoroutine(SetCam());
        }
    }

    IEnumerator SetCam()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log(Battle.playerTurn);
        if(Battle.playerTurn==1)
        {
            if (PlayerStateManager.Instance.player[0].currenthp > 0)
            {
                UnSetCamera(BattleCamera); // 이걸 함수 void (int select) 로해서 playerTurn 1이면 메이꺼 2이면 메일껄로 해도됨
                SetCamera(mei_Camera);
                SetCamera(playerTurn);
            }
            else
                Battle.playerTurn = 2;
        }
        if (Battle.playerTurn == 2)
        {
            if (PlayerStateManager.Instance.player[1].currenthp > 0)
            {
                UnSetCamera(BattleCamera);
                SetCamera(male_Camera);
                SetCamera(playerTurn);
            }
            else
                Battle.playerTurn = 3;
        }
        if(Battle.playerTurn==3)
        {
            enemyBattleManager.SetActive(true);
        }
        gameObject.SetActive(false);
    }

    void SetCamera(GameObject _SetActive)
    {
        _SetActive.SetActive(true);
    }

    void UnSetCamera(GameObject _SetActive)
    {
        _SetActive.SetActive(false);
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

    IEnumerator SetCamera()
    {
        yield return new WaitForSeconds(1.0f);
        SetCamera(BattleCamera);
        yield return new WaitForSeconds(1.0f);
        SetCamera(playerTurn);
    }
}
