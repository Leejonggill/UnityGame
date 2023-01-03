using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackBattle : MonoBehaviour
{
    //[SerializeField] Transform[] selectEnemy;
    [SerializeField] GameObject mainCam;
    [SerializeField] GameObject[] battleCamera;
    [SerializeField] GameObject[] selectCamera;
    [SerializeField] GameObject playerTurnObj;

    [SerializeField] GameObject player;
    [SerializeField] GameObject player2;

    [SerializeField] GameObject rotateSelect;
    [SerializeField] GameObject rotateSelect2;

    [SerializeField] GameObject[] mei_katana;
    [SerializeField] GameObject[] enemy_Hp;
    [SerializeField] GameObject BattleManager;

    [SerializeField] Image[] removeColor;
    [SerializeField] GameObject effect;

    [SerializeField] GameObject effect2;

    [SerializeField] Transform trans3;
    [SerializeField] GameObject effectAttackOra;
    [SerializeField] GameObject healOra;

    Transform[] enemyTrans;

    Transform parentTrans;
    public EnemyState[] enemyState;

    int select = 2;
    int playerTurn;

    bool isSelect = false;

    Animator playerAnim;
    Animator playerAnim2;

    readonly int isAttack = Animator.StringToHash("isAttack");
    readonly int isSkill = Animator.StringToHash("isSkill");
    readonly int isSkill2 = Animator.StringToHash("isSkill2");

    private void Start()
    {
        enemyTrans = new Transform[3];

        enemyTrans[0] = GameObject.Find("SpawnManager").transform;
        enemyTrans[1] = GameObject.Find("SpawnManager2").transform;
        enemyTrans[2] = GameObject.Find("SpawnManager3").transform;

        playerAnim = GameObject.Find("Player_Mei").GetComponent<Animator>();
        playerAnim2 = GameObject.Find("Player_Male").GetComponent<Animator>();
    }

    private void OnEnable()
    {
        parentTrans = GameObject.Find("SpawnScriptsObj").transform;
        enemyState = parentTrans.GetComponentsInChildren<EnemyState>();

        firstShowSet();

        Debug.Log(Battle.playerTurn);
        isSelect = false;
        playerTurn = Battle.playerTurn;

        if (playerTurn == 1)
        {
            rotateSelect.SetActive(true);
            battleCamera[playerTurn - 1].SetActive(false);
            playerTurnObj.SetActive(false);
            selectCamera[playerTurn - 1].SetActive(true);
            StartCoroutine(ShowEnemyHp());
        }
        else if (playerTurn == 2)
        {
            rotateSelect2.SetActive(true);
            battleCamera[playerTurn - 1].SetActive(false);
            playerTurnObj.SetActive(false);
            selectCamera[playerTurn - 1].SetActive(true);
            StartCoroutine(ShowEnemyHp());
        }
    }

    private void OnDisable()
    {
        player.transform.localRotation = Quaternion.Euler(0, -90, 0);
        player2.transform.localRotation = Quaternion.Euler(0, -90, 0);
        PlayerSkill.selecSkill = 0;

        BattleManager.SetActive(true);
    }

    void Update()
    {
        if(PlayerSkill.selecSkill==3&&!isSelect)
        {
            if (playerTurn == 1)
            {
                isSelect = true;
                Battle.playerTurn++;
                StartCoroutine(skillAttack());
                RemoveColor();
            }
            else if(playerTurn==2)
            {
                isSelect = true;
                Battle.playerTurn++;
                StartCoroutine(skillAttack2());
                RemoveColor();
            }
        }

        if (!isSelect)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                leff();
                StartCoroutine(ShowEnemyHp());
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                right();
                Debug.Log(select);
                StartCoroutine(ShowEnemyHp());
            }
            //select = Mathf.Clamp(select, 0, 2);


            if (enemyState[select].enemyHp > 0)
            {
                if (Input.GetKeyDown(KeyCode.Space) && PlayerSkill.selecSkill == 0)
                {
                    isSelect = true;
                    if (playerTurn == 1)
                        StartCoroutine(Attack());
                    else if (playerTurn == 2)
                        StartCoroutine(Attack2());
                    Battle.playerTurn++;
                    RemoveColor();
                }
                else if (Input.GetKeyDown(KeyCode.Space) && PlayerSkill.selecSkill != 0)
                {
                    isSelect = true;
                    Battle.playerTurn++;
                    if (playerTurn == 1)
                        StartCoroutine(skillAttack());
                    else if (playerTurn == 2)
                        StartCoroutine(skillAttack2());
                    RemoveColor();
                }
                SetSelectImage();
                LookAt();
            }
        }
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

    void LookAt()
    {
        if (playerTurn == 1)
        {
            Vector3 dis = enemyTrans[select].position - player.transform.position;
            player.transform.localRotation = Quaternion.Slerp(player.transform.localRotation, Quaternion.LookRotation(dis),
                Time.deltaTime * 3.0f);
        }
        else if (playerTurn == 2)
        {
            if (select != 2)
            {
                Vector3 dis = enemyTrans[select].position - player2.transform.position;
                player2.transform.localRotation = Quaternion.Slerp(player2.transform.localRotation, Quaternion.LookRotation(dis),
                    Time.deltaTime * 3.0f);
            }
            else if (select == 2)
            {
                Vector3 dis = trans3.position - player2.transform.position;
                player2.transform.localRotation = Quaternion.Slerp(player2.transform.localRotation, Quaternion.LookRotation(dis),
                    Time.deltaTime * 3.0f);
            }
        }
    }

    IEnumerator Attack()
    {
        if (player1DamageUp != null && powerUp == false)
        {
            powerUp = true;
            PlayerStateManager.Instance.player[0].str += 40;
            Destroy(player1DamageUp.transform.gameObject);
        }

        float time = 0;
        rotateSelect.SetActive(false);
        Vector3 dis = enemyTrans[select].transform.position - player.transform.position;

        mei_katana[0].SetActive(false);
        mei_katana[1].SetActive(true);
        playerAnim.SetBool(isAttack, true);
        while (GetDistance() >= 4)
        {
            time += Time.deltaTime;
            player.transform.Translate(Vector3.forward * 3.0f * time, Space.Self);
            yield return null;
        }
        playerAnim.SetBool(isAttack, false);
        yield return new WaitForSeconds(0.6f);
        SoundsManager.Instance.OnMaleAttackSound(0);
        yield return new WaitForSeconds(0.2f);
        Damage();
        yield return new WaitForSeconds(0.5f);
        mei_katana[0].SetActive(true);
        mei_katana[1].SetActive(false);
        yield return new WaitForSeconds(0.3f);
        while (GetDistance() <= 5)
        {
            player.transform.position -= dis * Time.deltaTime * 3.5f;
            yield return null;
        }
        player.transform.localPosition = new Vector3(16, 8, 60.5f);
        player2.transform.localPosition = new Vector3(16, 8, 62.7f);
        StartCoroutine(DisAble());
    }

    IEnumerator Attack2()
    {
        float time = 0;
        rotateSelect2.SetActive(false);
        Vector3 dis = enemyTrans[select].transform.position - player2.transform.position;

        playerAnim2.SetBool(isAttack, true);
        while (GetDistance2() >= 4)
        {
            time += Time.deltaTime;
            player2.transform.Translate(Vector3.forward * 3.0f * time, Space.Self);
            //if (select == 1)
            //    player2.transform.position += dis * Time.deltaTime * 3.7f;
            //else if (select == 2)
            //    player2.transform.position += dis * Time.deltaTime * 5.5f;
            //else if (select == 0)
            //    player2.transform.position += dis * Time.deltaTime * 4.4f;
            yield return null;
        }
        yield return new WaitForSeconds(2.4f);
        Damage();
        yield return new WaitForSeconds(0.3f);
        playerAnim2.SetBool(isAttack, false);
        yield return new WaitForSeconds(0.3f);
        while (GetDistance2() <= 5)
        {
            player2.transform.position -= dis * Time.deltaTime * 4.5f;
            yield return null;
        }
        player.transform.localPosition = new Vector3(16, 8, 60.5f);
        player2.transform.localPosition = new Vector3(16, 8, 62.7f);
        StartCoroutine(DisAble());
    }

    IEnumerator DisAble()
    {
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < enemy_Hp.Length; i++)
        {
            enemy_Hp[i].SetActive(false);
        }
        player.transform.localRotation = Quaternion.Euler(0, -90, 0);
        player2.transform.localRotation = Quaternion.Euler(0, -90, 0);
        mainCam.SetActive(true);
        selectCamera[playerTurn - 1].SetActive(false);
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);

        if (powerUp == true && player1DamageUp == null)
        {
            powerUp = false;
            PlayerStateManager.Instance.player[0].str -= 40;
        }
    }

    void SetSelectImage()
    {
        if (playerTurn == 1)
        {
            switch (select)
            {
                case 0:
                    rotateSelect.transform.localPosition = Vector3.Lerp(rotateSelect.transform.localPosition,
                         new Vector3(-6.1f, 164, -15.3f), Time.deltaTime * 7);
                    break;
                case 1:
                    rotateSelect.transform.localPosition = Vector3.Lerp(rotateSelect.transform.localPosition,
                        new Vector3(-46.1f, 167, -15.3f), Time.deltaTime * 7);
                    break;
                case 2:
                    rotateSelect.transform.localPosition = Vector3.Lerp(rotateSelect.transform.localPosition,
                    new Vector3(-101.4f, 106.6f, -15.3f), Time.deltaTime * 7);
                    break;
            }
        }
        else if (playerTurn == 2)
        {
            switch (select)
            {
                case 0:
                    rotateSelect2.transform.localPosition = Vector3.Lerp(rotateSelect2.transform.localPosition,
                         new Vector3(-85.8f, 144.3f, -15.3f), Time.deltaTime * 7);
                    break;
                case 1:
                    rotateSelect2.transform.localPosition = Vector3.Lerp(rotateSelect2.transform.localPosition,
                        new Vector3(-85.8f, 144.3f, -15.3f), Time.deltaTime * 7);
                    break;
                case 2:
                    rotateSelect2.transform.localPosition = Vector3.Lerp(rotateSelect2.transform.localPosition,
                    new Vector3(-34.9f, 144.3f, -15.3f), Time.deltaTime * 7);
                    break;

            }
        }
    }

    IEnumerator ShowEnemyHp()
    {
        for (int i = 0; i < enemy_Hp.Length; i++)
        {

            enemy_Hp[i].SetActive(false);
        }

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < enemy_Hp.Length; i++)
        {
            if (select == i)
            {
                enemy_Hp[i].SetActive(true);
            }
            else
            {
                enemy_Hp[i].SetActive(false);
            }
        }
    }

    void firstShowSet()
    {
        if (enemyState[2].enemyHp > 0)
        {
            select = 2;
        }
        else if (enemyState[1].enemyHp > 0)
        {
            select = 1;
        }
        else if (enemyState[0].enemyHp > 0)
        {
            select = 0;
        }
    }

    void leff()
    {
        if (enemyState[2].enemyHp > 0 && select == 1)
        {
            select++;
        }

        if (enemyState[1].enemyHp > 0 && select == 0)
        {
            select++;
        }

        if (enemyState[2].enemyHp > 0 && enemyState[1].enemyHp <= 0 && select == 0)
        {
            select = 2;
        }
    }

    void right()
    {
        if (enemyState[0].enemyHp > 0 && select == 1)
        {
            select--;
        }

        if (enemyState[1].enemyHp > 0 && select == 2)
        {
            select--;
        }

        if (enemyState[0].enemyHp > 0 && enemyState[1].enemyHp <= 0 && select == 2)
        {
            select = 0;
        }
    }

    float GetDistance()
    {
        return (enemyTrans[select].position - player.transform.position).magnitude;
    }
    float GetDistance2()
    {
        return (enemyTrans[select].position - player2.transform.position).magnitude;
    }

    GameObject player1DamageUp = null;
    bool powerUp;
    IEnumerator skillAttack()
    {
        float time = 0;
        rotateSelect.SetActive(false);
        Vector3 dis = enemyTrans[select].transform.position - player.transform.position;

        mei_katana[0].SetActive(false);
        mei_katana[1].SetActive(true);

        //if (player1DamageUp != null) // 이렇게해서 밑에 데미지 함수에 + powerUp넣어주면됨. 하지만 검기를 에너미스태이트 에다 했으니
        //{
        //    powerUp = 40;
        //    Destroy(player1DamageUp.transform.gameObject);
        //}

        if (player1DamageUp != null&&powerUp==false&& PlayerSkill.selecSkill != 2) 
        {
            powerUp = true;
            PlayerStateManager.Instance.player[0].str += 40;
            Destroy(player1DamageUp.transform.gameObject);
        }

        if (PlayerSkill.selecSkill == 1)
        {
            playerAnim.SetBool(isSkill, true);
            while (GetDistance() >= 4)
            {
                time += Time.deltaTime;
                player.transform.Translate(Vector3.forward * 3.0f * time, Space.Self);
                //player.transform.position += dis * Time.deltaTime * 5.5f;
                yield return null;
            }
            yield return new WaitForSeconds(1.7f);
            SoundsManager.Instance.OnMaleAttackSound(0);
            yield return new WaitForSeconds(0.2f);
            Damage();
            yield return new WaitForSeconds(0.5f);
            playerAnim.SetBool(isSkill, false);
            mei_katana[0].SetActive(true);
            mei_katana[1].SetActive(false);
            yield return new WaitForSeconds(0.3f);
            while (GetDistance() <= 5)
            {
                player.transform.position -= dis * Time.deltaTime * 3.5f;
                yield return null;
            }
            player.transform.localPosition = new Vector3(16, 8, 60.5f);
            player2.transform.localPosition = new Vector3(16, 8, 62.7f);
            StartCoroutine(DisAble());
        }
        else if (PlayerSkill.selecSkill == 2)
        {
            bool isPower = false;
            playerAnim.SetBool(isSkill2, true);
            if (player1DamageUp == null) // 버프를 안받았을떄
            {
                isPower = true;
                player1DamageUp = Instantiate(effectAttackOra, player.transform.position + new Vector3(0, -0.5f, 0), Quaternion.Euler(-90, 0, 0));
            }
            SoundsManager.Instance.OnSkillSound(1);
            yield return new WaitForSeconds(1.7f);
            SoundsManager.Instance.OnMaleAttackSound(0);
            SkillEffect();
            if (isPower == true)
            {
                Destroy(player1DamageUp.transform.gameObject);
            }
            else if (player1DamageUp != null && powerUp == false&&isPower == false) // 버프를 받았을떄
            {
                powerUp = true;
                PlayerStateManager.Instance.player[0].str += 40;
                Destroy(player1DamageUp.transform.gameObject);
            }
            yield return new WaitForSeconds(0.5f);
            playerAnim.SetBool(isSkill2, false);
            mei_katana[0].SetActive(true);
            mei_katana[1].SetActive(false);
            yield return new WaitForSeconds(0.9f);
            StartCoroutine(DisAble());
        }
        else if(PlayerSkill.selecSkill==3)
        {
            playerAnim.SetTrigger("isBuff");
            player1DamageUp = Instantiate(effectAttackOra, player.transform.position+new Vector3(0,-0.5f,0), Quaternion.Euler(-90, 0, 0));
            SoundsManager.Instance.OnSkillSound(1);
            yield return new WaitForSeconds(1.5f);
            SoundsManager.Instance.OnSkillSound(0);
            yield return new WaitForSeconds(1.8f);
            mei_katana[0].SetActive(true);
            mei_katana[1].SetActive(false);
            StartCoroutine(DisAble());
        }

        //if (powerUp == true&& player1DamageUp == null )
        //{
        //    powerUp = false;
        //    PlayerStateManager.Instance.player[0].str -= 40;
        //}
        Debug.Log("스킬 로직");
        //if (player1DamageUp == null && powerUp == true)
        //{
        //    powerUp = false;
        //    PlayerStateManager.Instance.player[0].str -= 40;
        //    Destroy(player1DamageUp.transform.gameObject);
        //}
    }

    IEnumerator skillAttack2()
    {
        GameObject heal;
        float time = 0;
        rotateSelect2.SetActive(false);
        Vector3 dis = enemyTrans[select].transform.position - player2.transform.position;

        if (PlayerSkill.selecSkill == 2)
        {
            playerAnim2.SetBool(isSkill, true);
            while (GetDistance2() >= 3)
            {
                time += Time.deltaTime;
                player2.transform.Translate(Vector3.forward * 4.0f * time, Space.Self);
                //player2.transform.position += dis * Time.deltaTime * 5.0f;
                yield return null;
            }
            yield return new WaitForSeconds(0.95f);
            SoundsManager.Instance.OnMaleAttackSound(1);
            yield return new WaitForSeconds(0.1f);
            Damage();
            yield return new WaitForSeconds(0.4f);
            playerAnim2.SetBool(isSkill, false);
            yield return new WaitForSeconds(0.3f);
            while (GetDistance2() <= 5)
            {
                player2.transform.position -= dis * Time.deltaTime * 4.5f;
                yield return null;
            }
            player.transform.localPosition = new Vector3(16, 8, 60.5f);
            player2.transform.localPosition = new Vector3(16, 8, 62.7f);
            StartCoroutine(DisAble());
        }
        else if (PlayerSkill.selecSkill == 1)
        {
            heal = Instantiate(healOra, player2.transform.position + new Vector3(0, 0, 0), Quaternion.Euler(-90, 0, 0));
            playerAnim2.SetBool(isSkill2, true);
            SoundsManager.Instance.OnSkillSound(1);
            yield return new WaitForSeconds(2.3f);
            SoundsManager.Instance.OnMaleAttackSound(1);
            SkillEffect2();
            Destroy(heal.transform.gameObject);
            yield return new WaitForSeconds(0.7f);
            playerAnim2.SetBool(isSkill2, false);
            yield return new WaitForSeconds(0.9f);
            StartCoroutine(DisAble());
        }
        else if (PlayerSkill.selecSkill == 3)
        {
            playerAnim2.SetTrigger("isHeal");
            heal = Instantiate(healOra, player2.transform.position + new Vector3(0, 0, 0), Quaternion.Euler(-90, 0, 0));
            SoundsManager.Instance.OnSkillSound(0);
            yield return new WaitForSeconds(2.0f);
            if (PlayerStateManager.Instance.player[0].currenthp >= 0)
            {
                PlayerStateManager.Instance.HpResult(0, 50);
            }
            if (PlayerStateManager.Instance.player[1].currenthp >= 0)
            {
                PlayerStateManager.Instance.HpResult(1, 50);
            }
            Destroy(heal.transform.gameObject);
            StartCoroutine(DisAble());
        }
    }

    void SkillEffect()
    {
        Instantiate(effect, player.transform.position + new Vector3(0, 1, 0), player.transform.localRotation);
    }

    void SkillEffect2()
    {
        Instantiate(effect2, player2.transform.position + new Vector3(0, 1, 0), player2.transform.localRotation);
    }

    void Damage()
    {
        if (PlayerSkill.selecSkill == 0)
            enemyState[select].enemyHp -= PlayerStateManager.Instance.player[playerTurn - 1].str - enemyState[select].enemyDef;
        else
        {
            if (PlayerSkill.selecSkill == 1)
            {
                if (playerTurn == 1)
                {
                    float damage = (float)PlayerStateManager.Instance.player[playerTurn - 1].str * 1.5f - enemyState[select].enemyDef;
                    enemyState[select].enemyHp -= (int)damage;
                }
            }
            else if (PlayerSkill.selecSkill == 2)
            {
                if (playerTurn == 2)
                {
                    float damage = (float)PlayerStateManager.Instance.player[playerTurn - 1].str * 1.5f - enemyState[select].enemyDef;
                    enemyState[select].enemyHp -= (int)damage;
                }
            }
            //else if (PlayerSkill.selecSkill == 3)
            //{

            //}
        }
    }

}
