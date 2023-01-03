using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleHit : MonoBehaviour
{
    [SerializeField] TextMeshPro textMesh;
    [SerializeField] GameObject hitImpact;
    public Transform player1;
    public Transform player2;
    Animator hitPlayer;
    Animator hitPlayer2;
    PlayerStateManager playerState;
    int tempHp1;
    int tempHp2;
    bool isPlayer1;
    bool isPlayer2;

    void Start()
    {
        player1 = GameObject.Find("Player_Mei").GetComponent<Transform>();
        player2 = GameObject.Find("Player_Male").GetComponent<Transform>();
        hitPlayer  = GameObject.Find("Player_Mei").GetComponent<Animator>();
        hitPlayer2 = GameObject.Find("Player_Male").GetComponent<Animator>();
        playerState = PlayerStateManager.Instance;
        tempHp1 = playerState.player[0].currenthp;
        tempHp2 = playerState.player[1].currenthp;
    }

    private void OnDisable()
    {
        isPlayer1 = false;
        isPlayer2 = false;
        hitPlayer.SetBool("isDead",false);
        hitPlayer2.SetBool("isDead", false);
    }

    void Update()
    {
        if(tempHp1>playerState.player[0].currenthp)
        {
            SoundsManager.Instance.OnHitEnemy();
            textMesh.color = new Color(255, 0, 0);
            textMesh.text = (tempHp1 - playerState.player[0].currenthp).ToString();
            Instantiate(textMesh, player1.position + new Vector3(0, 1.3f,1), Quaternion.Euler(0, -90, 0));
            Instantiate(hitImpact, player1.position + new Vector3(0, 1.0f, 0), Quaternion.Euler(0, 90, 0));
            //Instantiate(textMesh, hitPlayer.transform.position)
            tempHp1 = playerState.player[0].currenthp;
            if (tempHp1 <= 0 && !isPlayer1)
            {
                tempHp1 = 0;
                playerState.player[0].currenthp = 0;
                isPlayer1 = true;
                hitPlayer.SetBool("isDead", true);
            }
            else
            {
                hitPlayer.SetTrigger("isHit");
            }
        }
        if (tempHp2 > playerState.player[1].currenthp)
        {
            SoundsManager.Instance.OnHitEnemy();
            textMesh.color = new Color(255, 0, 0);
            textMesh.text = (tempHp2 - playerState.player[1].currenthp).ToString();
            Instantiate(textMesh, player2.position + new Vector3(0, 1.5f, 1), Quaternion.Euler(0, -90, 0));
            Instantiate(hitImpact, player2.position + new Vector3(0, 1.2f, 0), Quaternion.Euler(0, 90, 0));

            tempHp2 = playerState.player[1].currenthp;
            if (tempHp2 <= 0 && !isPlayer2)
            {
                tempHp2 = 0;
                playerState.player[1].currenthp = 0;
                isPlayer2 = true;
                hitPlayer2.SetBool("isDead", true);
            }
            else
            {
                hitPlayer2.SetTrigger("isHit");
            }
        }

        if(tempHp1!=playerState.player[0].currenthp)
            tempHp1 = playerState.player[0].currenthp;
        if (tempHp2 != playerState.player[1].currenthp)
            tempHp2 = playerState.player[1].currenthp;
    }
}
