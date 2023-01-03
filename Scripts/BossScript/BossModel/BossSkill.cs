using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    [SerializeField] GameObject skillMove;
    [SerializeField] Transform bossTrans;

    Vector3 dis;
    bool isUpdate = false;
    // Update is called once per frame
    private void Start()
    {
        bossTrans = GameObject.Find("PlayerCharacter").transform;
        Destroy(gameObject, 1.5f);
        StartCoroutine(BoolSet());
    }

    void Update()
    {
        if (isUpdate == false)
        {
            transform.Translate(Vector3.forward * Time.deltaTime*9);
            dis = bossTrans.transform.position - transform.position;
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(dis), Time.deltaTime * 7);
            skillMove.transform.Translate(Vector3.forward * Time.deltaTime * 8);
        }
    }

    IEnumerator BoolSet()
    {
        yield return new WaitForSeconds(1.0f);
        isUpdate = true;
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.CompareTag("Player"))
    //    {
    //        Debug.Log("보스스킬적중");
    //        PlayerStateManager.Instance.player[0].currenthp -= 20;
    //    }
    //}
}
