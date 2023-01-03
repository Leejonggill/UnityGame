using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cPlayerController : MonoBehaviour
{
    //public static int playerSelect = 1;

    [Header("Camera")]
    public Transform camController;
    public Transform cam;
    //public float camSpeed = 0.5f;
    //float mouseX;
    //float mouseY = 1;
    //float mouseWheel = -3f;

    [Header("Player")]
    public Transform playerController;
    public Transform playerTransform;
    float playerSpeed = 10.0f;

    Vector3 movement;
    Transform rayTransform;
    Animator playerAnim;

    readonly int isRun = Animator.StringToHash("isRun");

    private void OnEnable()
    {
        rayTransform = GameObject.Find("RayObject").transform;
        cam = Camera.main.transform; // �������ٶ󺼋�
        camController = GameObject.Find("CameraParent").transform;
        //cam.position = new Vector3(0, 1, -3);

        // �÷��̾� ����
        playerController = GameObject.Find("PlayerCharacter").transform;
        if (PlayerStateManager.playerSelect == 1)
        {
            playerTransform = GameObject.Find("Player_Mei").transform;
            playerAnim = playerTransform.GetComponent<Animator>();
        }
        else if (PlayerStateManager.playerSelect == 2)
        {
            playerTransform = GameObject.Find("Player_Male").transform;
            playerAnim = playerTransform.GetComponent<Animator>();
        }
    }

    //void Start()
    //{
    //    // ī�޶� ����
    //    //cam = Camera.main.transform;
    //    //camController = GameObject.Find("CameraParent").transform;
    //    ////cam.position = new Vector3(0, 1, -3);

    //    //// �÷��̾� ����
    //    //playerController = GameObject.Find("PlayerCharacter").transform;
    //    //playerTransform = GameObject.Find("Player_Mei").transform;
    //    //playerAnim = playerTransform.GetComponent<Animator>();
    //}

    private void Update()
    {
        //CamMove();
        //Zoom();
        Move();
    }

    //void CamMove()
    //{
    //    mouseX += Input.GetAxis("Mouse X");
    //    mouseY -= Input.GetAxis("Mouse Y");
    //    mouseY = Mathf.Clamp(mouseY, 0, 12);

    //    camController.rotation = Quaternion.Euler(
    //        new Vector3(camController.rotation.x + mouseY, camController.rotation.y + mouseX, 0) * 3.0f);
    //}

    //void Zoom()
    //{
    //    mouseWheel += Input.GetAxis("Mouse ScrollWheel") * 10;
    //    mouseWheel = Mathf.Clamp(mouseWheel, -6, -2);
    //    cam.localPosition = new Vector3(0, 1, mouseWheel);
    //}

    void Move()
    {
        //camController.position = playerTransform.position;
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        movement = new Vector3(h, 0, v);

        if (movement != Vector3.zero)
        {
            //playerTransform.Translate(movement * Time.deltaTime * playerSpeed);
            //playerController.position = playerTransform.position;
            //playerTransform.localPosition = new Vector3(0, 0, 0);

            Quaternion cameraRotation = camController.rotation;
            Quaternion playerRotation = playerController.rotation;
            cameraRotation.x = 0; // y���� �յ��¿� �̱⶧���� y�ุ �ٲ�� �ϱ�
            cameraRotation.z = 0; // �ȳ־ ������µ� ������ camContorller.roation x z ��
            playerRotation.x = 0; // PlayerController x z ���� �ٸ���������.
            playerRotation.z = 0;
            playerController.localRotation = Quaternion.Slerp(playerRotation, cameraRotation, 10.0f * Time.deltaTime);
            // Slerp �� Lerp�� �ᵵ ����̾���

            // movemnt�� �Ǵ�����
            // ȸ������ 0.0 0.0 0.0 0.0 ���� �����Ǵµ�
            // �������ٸ� h = 1 �ڸ����ٸ� h = -1
            // ���������ٸ� v = -1 �����������ٸ� v = 1�̵Ǳ⶧����

            // ����Ƽ ������ �ڵ� playerController.Translate(movement * Time.deltaTime * playerSpeed);
            // - �θ�(PlayerCharcter)���� �߷�,�ݸ��� �߰��Ҷ�. 
            // - PlayerCharcter���� �������߰����ϸ� ���������� �Ͼ����. PlayerController�� ȥ���߰�.

            // * �ڽ�(Player_Mei)���� �߷� �ݸ���(����)�� �߰��Ҷ�.
            playerController.Translate(movement * Time.deltaTime * playerSpeed);
            playerController.position = playerTransform.position;
            playerTransform.localPosition = new Vector3(0, 0, 0);

            playerTransform.localRotation = Quaternion.Slerp(playerTransform.localRotation, Quaternion.LookRotation(movement), 10.0f * Time.deltaTime);
            // ���÷��ؾߵǴ����� (�ڽ��� ����)
            // ����������� ������������ 0.0.0 �̶�������� ������ϸ� �θ��� ���������γ���
            // ���������� X�� =1�ϸ� ������ǥ�������� 1��ŭ���������� �������������� x =1 ���ϸ� �������������� x=1�� ������ 
            // ���� ���÷����̼������ؾ� �ڽ��� ����(ȸ��X ,Y ,Z)�� �����.(�ڽ�����������)
            // playerTransform.rotaiton = Quaternion.Slerp(playerTransform.rotaiton, Quaternion.LookRotation(movement), 10.0f * Time.deltaTime);
            // �ٵ� �̰ɷ��ص� ���������

            playerAnim.SetBool(isRun, true);
        }
        else
        {
            playerAnim.SetBool(isRun, false);
        }

        camController.position = playerTransform.position;
        //rayTransform.position = camController.position;
    }
}
