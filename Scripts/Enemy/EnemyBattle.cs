using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattle : MonoBehaviour
{
    [SerializeField] GameObject BattleManager;
    [SerializeField] Camera battleCamera;
    [SerializeField] Camera[] playerCamera;
    [SerializeField] Transform[] playerTransform;
    [SerializeField] Transform[] spawnTrans;

    Transform parentTrans;
    public EnemyState[] enemyState; // 이걸그냥 
    int frame;

    private void OnEnable()
    {
        frame = 0;
        // enmeyState = new EnemyState[3] 이렇게 쓰고
        // for문이용해서 null이면 리턴하게
        parentTrans = GameObject.Find("SpawnScriptsObj").transform;
        enemyState = parentTrans.GetComponentsInChildren<EnemyState>();
        StartCoroutine(AttackLogic());
    }

    private void OnDisable()
    {
        BattleManager.SetActive(true);
    }

    //private void Update()
    //{
    //    LookAtPlayer();
    //}

    void LookAtPlayer(int _frame)
    {
        //if (GetDistance() >= 2.6f)
        //{
            enemyState[frame].myAnim.SetBool("isWalk", true);
            Vector3 dis = playerTransform[_frame].position - enemyState[frame].myTransform.position;
            enemyState[frame].myTransform.localRotation = Quaternion.Lerp(enemyState[frame].myTransform.localRotation,
                Quaternion.LookRotation(dis), 0.2f);
            enemyState[frame].myTransform.Translate(Vector3.forward * 5 * Time.deltaTime);
        //}
        //else
        //{
        //    enemyState[0].myAnim.SetBool("isWalk", false);
        //    enemyState[0].myAnim.SetBool("isAttack", true);
        //    if(enemyState[0].myAnim.GetBool("isAttack"))
        //    {
        //        enemyState[0].myAnim.SetBool("isAttack", false);
        //    }
        //}
    }

    float GetDistance(int _frame)
    {
        return (playerTransform[_frame].position - enemyState[frame].myTransform.position).magnitude;
    }

    float GetBackDistance()
    {
        return (spawnTrans[frame].position - enemyState[frame].myTransform.position).magnitude;
    }

    float GetResetDistance()
    {
        return (enemyState[frame].myTransform.position - spawnTrans[frame].position).magnitude;
    }

    void LooAtReswpan()
    {
        enemyState[frame].myAnim.SetBool("isWalk", true);
        Vector3 dis = spawnTrans[frame].position - enemyState[frame].myTransform.position;
        enemyState[frame].myTransform.localRotation = Quaternion.Lerp(enemyState[frame].myTransform.localRotation,
            Quaternion.LookRotation(dis), 0.05f);
        enemyState[frame].myTransform.Translate(Vector3.forward * 5 * Time.deltaTime);
    }

    int PlayerHp(int ranPlayer)
    {
        int result = ranPlayer;
        //if(PlayerStateManager.Instance.player[0].currenthp <= 0 &&PlayerStateManager.Instance.player[1].currenthp<=0)
        //{
        //    return 
        //}
        if (PlayerStateManager.Instance.player[0].currenthp<=0&&ranPlayer==0)
        {
            result = 1;
        }
        else if (PlayerStateManager.Instance.player[1].currenthp <= 0 && ranPlayer == 1)
        {
            result = 0;
        }

        return result;
    }

    void HpResult(int Select)
    {
        Debug.Log(PlayerStateManager.Instance.player[Select].currenthp);
        PlayerStateManager.Instance.player[Select].currenthp -= enemyState[frame].enemyAtk - (PlayerStateManager.Instance.player[Select].def / 10);
        Debug.Log(PlayerStateManager.Instance.player[Select].currenthp);
    }

    IEnumerator AttackLogic()
    {
        //if(PlayerStateManager.Instance.player[0].currenthp<=0&&PlayerStateManager.Instance.player[1].currenthp<=0)
        //{
        //    frame = 3;
        //}
        //Debug.Log("로직실행");

        if (frame < 3)
        {
            if (enemyState[frame].enemyHp > 0)
            {
                int ranPlayer = Random.Range(0, 2);
                ranPlayer = PlayerHp(ranPlayer);

                if (PlayerStateManager.Instance.player[ranPlayer].currenthp > 0)
                {
                    while (GetDistance(ranPlayer) >= 2.6f)
                    {
                        LookAtPlayer(ranPlayer);
                        yield return null;
                    }
                    enemyState[frame].myAnim.SetBool("isWalk", false);
                    enemyState[frame].myAnim.SetBool("isAttack", true);
                    yield return new WaitForSeconds(1.2f);
                    HpResult(ranPlayer);
                    yield return new WaitForSeconds(0.3f);
                    enemyState[frame].myAnim.SetBool("isAttack", false);
                    yield return new WaitForSeconds(1.0f);
                    while (GetBackDistance() >= 0.2f)
                    {
                        LooAtReswpan();
                        yield return null;
                    }
                    enemyState[frame].myAnim.SetBool("isWalk", false);
                    yield return new WaitForSeconds(0.3f);
                    int frame2 = 0;
                    while (frame2 <= 60)
                    {
                        frame2++;
                        enemyState[frame].myTransform.localRotation = Quaternion.Slerp(enemyState[frame].myTransform.localRotation, Quaternion.LookRotation
                            (new Vector3(90, 0, 0)), 0.05f);
                        Debug.Log(GetResetDistance());
                        yield return null;
                    }
                    yield return new WaitForSeconds(1.0f);
                    frame++;
                    StartCoroutine(AttackLogic());
                }
            }
            else
            {
                frame++;
                StartCoroutine(AttackLogic());
            }
        }
        else
        {
            Battle.playerTurn = 1;
            gameObject.SetActive(false);
        }
    }
}
