using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossState : MonoBehaviour
{
    public int enemyMaxHp;
    public int enemyHp;
    public int enemyAtk;
    public int enemyExp;
    public int enemyDef;
    public int enemyGold;

    [SerializeField] Image image;
    [SerializeField] GameObject effect;

    public void Start()
    {
        enemyMaxHp = Random.Range(550, 600);
        enemyHp = enemyMaxHp;
        enemyAtk = Random.Range(18, 23);
        enemyDef = Random.Range(5, 8);
        enemyExp = Random.Range(500, 600);
        enemyGold = Random.Range(200, 300);
    }

    private void Update()
    {
        image.fillAmount = (float)enemyHp / enemyMaxHp;
        if(enemyHp<=0)
        {
            PlayerStateManager.Instance.player[0].currentExp += enemyExp;
            PlayerStateManager.Instance.LevelUp(0);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerAttack"))
        {
            Instantiate(effect, transform.position + new Vector3(0, 2.8f, 0), Quaternion.identity);
            SoundsManager.Instance.OnSwordSound(0);
            other.gameObject.SetActive(false);
            enemyHp -= PlayerStateManager.Instance.player[0].str;
            Debug.Log("콜리전실행");
        }
    }
}
