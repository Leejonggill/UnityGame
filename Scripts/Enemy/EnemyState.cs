using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyState : MonoBehaviour
{
    public int enemyMaxHp;
    public int enemyHp;
    public int enemyAtk;
    public int enemyExp;
    public int enemyDef;
    public int enemyGold;
    protected int enemyEvasion;
    public Transform myTransform { get; set; }
    public Animator myAnim { get; set; }
    public static GameObject hitEnemySave;

    //bool isDead = false;
    int tempHp;

    [SerializeField] TextMeshPro textMesh;
    [SerializeField] GameObject hitImpact;

    public void Start()
    {
        myAnim = GetComponent<Animator>();
        myTransform = transform;
        enemyMaxHp = Random.Range(100, 151);
        enemyHp = enemyMaxHp;
        enemyAtk = Random.Range(18, 23);
        enemyDef = Random.Range(5, 8);
        enemyExp = Random.Range(70, 90);
        enemyGold = Random.Range(20, 40);
        enemyEvasion = 10;
        tempHp = enemyHp;
    }

    private void Update()
    {
        if (tempHp > enemyHp)
        {
            SoundsManager.Instance.OnHitPlayer();
            //z 0.5+ 755.6 755.4
            textMesh.color = new Color(255, 255, 255);
            textMesh.text = (tempHp - enemyHp).ToString();
            Instantiate(textMesh, transform.position + new Vector3(1.0f, 1.5f, 1.2f), Quaternion.Euler(0, -90, 0));
            Instantiate(hitImpact, transform.position + new Vector3(1.0f, 1.5f, 0), Quaternion.identity);
            tempHp = enemyHp;
            if (tempHp <= 0)
            {
                tempHp = 0;
                enemyHp = 0;
                myAnim.SetBool("isDead", true);
            }
            else
            {
                myAnim.SetTrigger("isHit");
            }
        }

        //if (enemyHp<=0&& !isDead)
        //{
        //    myAnim.SetBool("isDead", true);
        //    isDead = true;
        //}
        //else if(enemyHp!=tempHp)
        //{
        //    tempHp = enemyHp;
        //    myAnim.SetTrigger("isHit");
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Skill1"))
        {
            //enemyHp -= PlayerStateManager.Instance.player[Battle.playerTurn-1].str * 2;
            //Destroy(other);
            enemyHp -= PlayerStateManager.Instance.player[0].str * 2 - enemyDef;
            Destroy(other.transform.gameObject);
        }
        if (other.CompareTag("Skill2"))
        {
            //enemyHp -= PlayerStateManager.Instance.player[Battle.playerTurn-1].str * 2;
            //Destroy(other);
            enemyHp -= PlayerStateManager.Instance.player[1].str * 2 - enemyDef;
            Destroy(other.transform.gameObject);
        }
    }
}
