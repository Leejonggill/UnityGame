using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ENEMY_STATE1 { IDLE, ATTACK, RUN, SKILLATTACK ,JUMPATTACK};

public class BossScripts : MonoBehaviour
{
    Transform target;
    Animator anim;
    //Rigidbody rigidbody1;
    float disMax = 2f;
    float speed = 3.0f;
    bool isAttackLast = false;
    bool isSkillCool = false;
    int ran;

    ENEMY_STATE1 state = ENEMY_STATE1.RUN;
    readonly int bossRun = Animator.StringToHash("isRun");
    readonly int bossAttack = Animator.StringToHash("isAttack");
    readonly int bossAttack2 = Animator.StringToHash("isAttack2");
    readonly int bossAttack3 = Animator.StringToHash("isAttack3");
    readonly int bossJump = Animator.StringToHash("isJump");
    readonly int bossSkill = Animator.StringToHash("isSkill");

    [SerializeField] GameObject bossSkillRoar;

    void Start()
    {
        //rigidbody1 = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        target = GameObject.Find("PlayerCharacter").transform;
        anim.SetBool(bossRun, true);
    }

    bool isAttackEnd = false;

    void Update()
    {
        if(isAttackEnd == true)
        {
            RotateGolem();
        }
        //rigidbody1.velocity = Vector3.zero;
        //rigidbody1.angularVelocity = Vector3.zero;
        switch (state)
        {
            case ENEMY_STATE1.IDLE:
                Idle();
                Debug.Log("대기");
                break;
            case ENEMY_STATE1.RUN:
                Move();
                Debug.Log("움직임");
                break;
            case ENEMY_STATE1.ATTACK:
                if (!isAttackLast)
                {
                    ran = Random.Range(1, 5);
                    Debug.Log(ran);
                    if (ran == 1)
                    {
                        Attack();
                    }
                    else if (ran == 2)
                    {
                        Attack2();
                    }
                    else if (ran == 3)
                    {
                        Attack3();
                    }
                    else if( ran==4)
                    {
                       state = ENEMY_STATE1.SKILLATTACK;
                    }
                }
                break;
            case ENEMY_STATE1.JUMPATTACK:
                JumpTrans();
                if (!isAttackLast)
                {
                    JumpAttack();
                }
                break;
            case ENEMY_STATE1.SKILLATTACK:
                if (!isAttackLast)
                {
                    isRoar();
                }
                break;
        }
    }

    void AnimSkill1()
    {
        Instantiate(bossSkillRoar, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
    }

    void SetBoolFalse()
    {
        isAttackEnd = false;
    }

    void SetBoolTrue()
    {
        isAttackEnd = true;
    }

    void Idle()
    {
        RotateGolem();
        anim.SetBool(bossRun, false);
        if (GetDistance() > disMax)
        {
            state = ENEMY_STATE1.RUN;
        }
    }

    void Move()
    {
        RotateGolem();
        anim.SetBool(bossRun, true);
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        if (GetDistance() >= 15)
        {
            //state = ENEMY_STATE1.IDLE;
            state = ENEMY_STATE1.JUMPATTACK;
            return;
        }

        if (GetDistance() <= disMax)
        {
            //state = ENEMY_STATE1.IDLE;
            state = ENEMY_STATE1.ATTACK;
        }
    }

    void Attack()
    {
        Debug.Log("Attack1");
        RotateGolem();
        if (GetDistance() > disMax)
        {
            state = ENEMY_STATE1.RUN;
            anim.SetBool(bossAttack, false);
            return;
        }
        anim.SetBool(bossAttack, true);
        isAttackLast = true;
    }

    void Attack2()
    {
        Debug.Log("Attack2");
        RotateGolem();
        if (GetDistance() > disMax)
        {
            state = ENEMY_STATE1.RUN;
            anim.SetBool(bossAttack2, false);
            return;
        }
        anim.SetBool(bossAttack2, true);
        isAttackLast = true;
    }

    void Attack3()
    {
        Debug.Log("Attack3");
        RotateGolem();
        if (GetDistance() > disMax)
        {
            state = ENEMY_STATE1.RUN;
            anim.SetBool(bossAttack3, false);
            return;
        }
        anim.SetBool(bossAttack3, true);
        isAttackLast = true;
    }

    void JumpAttack()
    {
        RotateGolem();
        anim.SetBool(bossJump, true);
        isAttackLast = true;
    }

    void isRoar()
    {
        RotateGolem();
        anim.SetBool(bossSkill, true);
        isAttackLast = true;
    }

    void AnimEndJumpTrigger()
    {
        isMove = false;
        isAttackLast = false;
        anim.SetBool(bossJump, false);
        state = ENEMY_STATE1.RUN;
    }

    void AnimEndSkill()
    {
        isAttackLast = false;
        anim.SetBool(bossSkill, false);
        state = ENEMY_STATE1.RUN;
    }

    void AnimAttackSetBool()
    {
        isAttackLast = false;
        anim.SetBool(bossAttack, false);
        anim.SetBool(bossAttack2, false);
        anim.SetBool(bossAttack3, false);
    }

    bool isMove = false;

    [SerializeField] GameObject right;
    [SerializeField] GameObject left;
    [SerializeField] GameObject jumpObject;

    void SetAcitveTrueAttackJump()
    {
        jumpObject.SetActive(jumpObject);
        StartCoroutine(ActiveFalse());
    }

    void SetAcitveTrueAttackRight()
    {
        right.SetActive(true);
        StartCoroutine(ActiveFalse());
    }

    void SetAcitveTrueAttackLeft()
    {
        left.SetActive(true);
        StartCoroutine(ActiveFalse());
    }

    IEnumerator ActiveFalse()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("적공격");
        right.SetActive(false);
        left.SetActive(false);
        jumpObject.SetActive(false);
    }

    void JumpTrans()
    {
        if (isMove == false)
        {
            RotateGolem();
            Vector3 dis = target.position - transform.position;
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 0.7f);
            if(GetDistance()<=5)
            {
                isMove = true;
            }
        }
    }

    void RotateGolem()
    {
        Vector3 dis = target.position - transform.position;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(dis), Time.deltaTime * 5);
    }

    float GetDistance()
    {
        return (target.position - transform.position).magnitude;
    }

}
