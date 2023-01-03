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
        cam = Camera.main.transform; // 같은곳바라볼떄
        camController = GameObject.Find("CameraParent").transform;
        //cam.position = new Vector3(0, 1, -3);

        // 플레이어 설정
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
    //    // 카메라 설정
    //    //cam = Camera.main.transform;
    //    //camController = GameObject.Find("CameraParent").transform;
    //    ////cam.position = new Vector3(0, 1, -3);

    //    //// 플레이어 설정
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
            cameraRotation.x = 0; // y축이 앞뒤좌우 이기때문에 y축만 바뀌게 하기
            cameraRotation.z = 0; // 안넣어도 상관없는데 가끔식 camContorller.roation x z 가
            playerRotation.x = 0; // PlayerController x z 값이 다를때가있음.
            playerRotation.z = 0;
            playerController.localRotation = Quaternion.Slerp(playerRotation, cameraRotation, 10.0f * Time.deltaTime);
            // Slerp 나 Lerp를 써도 상관이없음

            // movemnt로 되는이유
            // 회전축은 0.0 0.0 0.0 0.0 으로 구성되는데
            // 앞을본다면 h = 1 뒤를본다면 h = -1
            // 왼쪽을본다면 v = -1 오른쪽을본다면 v = 1이되기때문에

            // 유니티 선생님 코드 playerController.Translate(movement * Time.deltaTime * playerSpeed);
            // - 부모(PlayerCharcter)한테 중력,콜리더 추가할때. 
            // - PlayerCharcter한테 물리를추가안하면 물리현상이 일어났을때. PlayerController가 혼자쭉감.

            // * 자식(Player_Mei)에게 중력 콜리더(물리)를 추가할때.
            playerController.Translate(movement * Time.deltaTime * playerSpeed);
            playerController.position = playerTransform.position;
            playerTransform.localPosition = new Vector3(0, 0, 0);

            playerTransform.localRotation = Quaternion.Slerp(playerTransform.localRotation, Quaternion.LookRotation(movement), 10.0f * Time.deltaTime);
            // 로컬로해야되는이유 (자신의 기준)
            // 상속을받으면 로컬포지션은 0.0.0 이라고나오지만 월드로하면 부모의 포지션으로나옴
            // 로컬포지션 X를 =1하면 월드좌표기준으론 1만큼움직이지만 월드포지션으로 x =1 을하면 월드포지션으로 x=1로 움직임 
            // 따라서 로컬로테이션으로해야 자식의 각도(회전X ,Y ,Z)만 변경됨.(자신을기준으로)
            // playerTransform.rotaiton = Quaternion.Slerp(playerTransform.rotaiton, Quaternion.LookRotation(movement), 10.0f * Time.deltaTime);
            // 근데 이걸로해도 상관은없음

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
