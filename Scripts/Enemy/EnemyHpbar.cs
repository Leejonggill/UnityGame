using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpbar : MonoBehaviour
{
    //[SerializeField] GameObject hp_bar;
    //[SerializeField] Camera cam;

    //// Update is called once per frame
    //void Update()
    //{
    //    hp_bar.transform.position = cam.WorldToScreenPoint(transform.position + new Vector3(0, 0.8f, 0));
    //}

    [SerializeField] Image[] hp_bar;
    [SerializeField] Text[] enemy_StateText;
    public Transform parentTransform;
    public EnemyState[] enemyState;

    private void OnEnable()
    {
        StartCoroutine(setStart());
    }

    private void Update()
    {
        setHpResult();
    }

    IEnumerator setStart()
    {
        yield return new WaitForSeconds(1.0f);
        parentTransform = GameObject.Find("SpawnScriptsObj").transform;
        enemyState = parentTransform.GetComponentsInChildren<EnemyState>();
    }

    void setHpResult()
    {
        for(int i=0; i<hp_bar.Length;i++)
        {
            if (hp_bar[i].gameObject.activeSelf)
            {
                hp_bar[i].fillAmount = (float)enemyState[i].enemyHp / enemyState[i].enemyMaxHp;
                enemy_StateText[i].text = "체력 :" + enemyState[i].enemyHp+"\n"+"공격력 :" + enemyState[i].enemyAtk + "\n"+
                    "방어력 :" + enemyState[i].enemyDef;
            }
        }
    }
}
