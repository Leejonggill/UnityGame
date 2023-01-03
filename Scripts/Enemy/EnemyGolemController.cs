using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum ENMEY_STATE {  IDLE,MOVE,BACK}

public class EnemyGolemController : MonoBehaviour
{
    float disMax = 17f;
    Animator anim;
    Transform target;
    float speed = 3.0f;
    readonly int golemWalk = Animator.StringToHash("isWalk");
    ENMEY_STATE state = ENMEY_STATE.IDLE;
    [SerializeField] Transform maxMove;
    [SerializeField] GameObject[] normalgameObjects;
    [SerializeField] GameObject setBattelMaps;
    [SerializeField] Image fadeIn;
    [SerializeField] Image fadeOut;

    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.Find("PlayerCharacter").transform;
    }

    void Update()
    {
        //Debug.Log(transform.localRotation);
        //Debug.Log(transform.rotation);
        //Debug.Log(new Vector3(maxMove.position.x, maxMove.position.y, maxMove.position.z));
        //Debug.Log(new Vector3(transform.position.x, transform.position.y, transform.position.z));
        switch (state)
        {
            case ENMEY_STATE.IDLE:
                Idle();
                Debug.Log("대기");
                break;
            case ENMEY_STATE.MOVE:
                Move();
                Debug.Log("움직임");
                break;
            case ENMEY_STATE.BACK:
                Back();
                Debug.Log("백");
                break;
        }
    }


    void Idle()
    {
        anim.SetBool(golemWalk, false);
        if(GetDistance()< disMax)
        {
            state = ENMEY_STATE.MOVE;
        }
    }

    void Move()
    {
        RotateGolem();
        anim.SetBool(golemWalk, true);
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        if(GetDistance()>= disMax && GetDistanceMax()>=1)
        {
            state = ENMEY_STATE.BACK;
        }
        //else if(GetDistance()>=25)
        //{
        //    state = ENMEY_STATE.IDLE;
        //}
    }

    void Back()
    {
        BackRotateGolem();
        anim.SetBool(golemWalk, true);
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        if(GetDistanceMax()<=1f)
        {
            state = ENMEY_STATE.IDLE;
        }
        else if(GetDistance()< disMax && GetDistanceMax()<25)
        {
            state = ENMEY_STATE.MOVE;
        }
    }

    void RotateGolem()
    {
        Vector3 dis = target.position - transform.position;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(dis), Time.deltaTime * 5);
    }

    void BackRotateGolem()
    {
        Vector3 dis = new Vector3(maxMove.position.x - transform.position.x,0,
            maxMove.position.z - transform.position.z);
        transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(dis), Time.deltaTime * 5);
    }

    float GetDistance()
    {
        return (target.position - transform.position).magnitude;
    }

    float GetDistanceMax()
    {
        return (maxMove.position - transform.position).magnitude;
    }

    IEnumerator SetFade()
    {
        Time.timeScale = 0;
        Color color;
        color = fadeIn.color;
        while(color.a<1)
        {
            color.a += 0.002f;
            fadeIn.color = color;
            yield return null;
        }
    }

    IEnumerator SetFadeOut()
    {
        Time.timeScale = 1.0f;
        Color color;
        color = fadeOut.color;
        while (color.a > 0)
        {
            color.a -= 0.002f;
            fadeOut.color = color;
            yield return null;
        }
    }

    IEnumerator Battle()
    {
        yield return StartCoroutine(SetFade());
        for (int i = 0; i < normalgameObjects.Length; i++)
        {
            normalgameObjects[i].SetActive(false);
        }
        setBattelMaps.SetActive(true);
        StartCoroutine(SetFadeOut());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            SoundsManager.Instance.OnSwordSound(1);
            EnemyState.hitEnemySave = gameObject;
            StartCoroutine(Battle());
        }
    }
}
