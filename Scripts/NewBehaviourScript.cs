using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Horizontal"))
        {
            float h = Input.GetAxisRaw("Horizontal");
            transform.Translate(new Vector3(h, 0, 0) * 3 * Time.deltaTime);
        }
    }

    //Animator anim;
    //public GameObject attck;
    //bool isAttack = false;

    //void Start()
    //{
    //    anim = GetComponent<Animator>();
    //}

    //void Update()
    //{
    //    if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
    //    {
    //        // * �ִϸ��̼� ��ȯ
    //        anim.SetInteger("speed", 3);

    //        // * �̵�
    //        float h = Input.GetAxisRaw("Horizontal");
    //        float v = Input.GetAxisRaw("Vertical");
    //        Vector3 movement = new Vector3(h, 0, v);

    //        transform.Translate(Vector3.forward * 3 * Time.deltaTime);
    //        // ȸ�� ȸ���� �������ϱ� �����θ� �̵��ϰ�.
    //        transform.rotation = Quaternion.LookRotation(movement);
    //        // Quaternion.LookRotation : ���⺤�͸� ���ʹϾ�(ȸ��) ������ ��ȯ.
    //    }
    //    else
    //    {
    //        anim.SetInteger("speed", 0);
    //    }


    //    if (Input.GetMouseButtonDown(0) && isAttack == false)
    //    {
    //        StartCoroutine(Attack());
    //        anim.SetTrigger("isAttack");
    //    }

    //    if (Input.GetKeyDown(KeyCode.Return))
    //    {
    //        anim.SetTrigger("isHit");
    //    }
    //}


    //IEnumerator Attack()
    //{
    //    isAttack = true; // ��Ӱ��ݸ���� �����ϰ�.
    //    yield return new WaitForSeconds(0.7f);
    //    attck.SetActive(true);
    //    yield return new WaitForSeconds(0.5f);
    //    attck.SetActive(false);
    //    isAttack = false;
    //}
}
