using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour
{
    RaycastHit hitinfo;
    Vector3 dic;
    float direct;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dic = Camera.main.transform.position - transform.position;
        direct = (Camera.main.transform.position - transform.position).magnitude; // float형으로 변환
        if (Physics.Raycast(transform.position, dic,out hitinfo ,direct))
        {
            if(hitinfo.collider.CompareTag("House"))
            {
                Debug.Log(transform.position);
                Debug.Log(hitinfo.transform.position);
                Debug.Log(Camera.main.transform.position);
                Camera.main.transform.position = hitinfo.transform.position;
                Debug.Log(Camera.main.transform.position);
            }
        }
    }
}
