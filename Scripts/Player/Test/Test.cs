using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float camSpeed = 0.5f;
    float mouseX;
    float mouseY = 1;
    float mouseWheel = -3f;

    Vector3 ray_target;
    Quaternion mytransform;
    RaycastHit hitinfo;
    Transform Player;
    Transform MainCamera;
    private float camera_dist = 0f;
    //bool istrigger;
    //Vector3 dir;

    private void Start()
    {
        mytransform = this.transform.rotation;
        Player = GameObject.Find("PlayerCharacter").transform;
        MainCamera = GameObject.Find("MainCamera").transform;
        //dir = (MainCamera.transform.position - Player.transform.position);
    }

    void Update()
    {
        Debug.Log(mytransform);
        Debug.Log(transform.rotation);
        CamMove();
        Zoom();

        ray_target = (MainCamera.transform.position - Player.transform.position).normalized;
        camera_dist = (MainCamera.transform.position - Player.transform.position).magnitude;
        //Physics.Raycast(transform.position, ray_target, out hitinfo, camera_dist);

        Debug.Log(camera_dist);

        if (Physics.Raycast(transform.position, ray_target, out hitinfo, camera_dist))
        {
            if (hitinfo.transform.CompareTag("Ground"))
            {
                Debug.Log("Ãæµ¹");
                //istrigger = true;
                MainCamera.transform.position = new Vector3(hitinfo.point.x, MainCamera.transform.position.y, hitinfo.point.z);
                //tempY += 0.05f * Time.deltaTime;
            }
        }
    }

    void CamMove()
    {
        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, 0, 12);
        transform.rotation = Quaternion.Euler(
            new Vector3(transform.rotation.x + mouseY, transform.rotation.y + mouseX, 0) * 3.0f);
    }

    void Zoom()
    {
        mouseWheel += Input.GetAxis("Mouse ScrollWheel") * 10;
        mouseWheel = Mathf.Clamp(mouseWheel, -6, -2);
        Vector3 temp = new Vector3(MainCamera.transform.localPosition.x, 1, mouseWheel);
        MainCamera.transform.localPosition = Vector3.Lerp(MainCamera.transform.localPosition, temp, 0.1f);
        //rayTransform.localPosition = temp;
    }

}