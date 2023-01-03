using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossPlayerCameraCon : MonoBehaviour
{
    #region value
    //[SerializeField] GameObject[] setCharacter = new GameObject[2];
    //[SerializeField] GameObject[] effect = new GameObject[2];
    //[SerializeField] GameObject[] imageObject = new GameObject[2];
    //[SerializeField] Image escBakcGround;
    //[SerializeField] Text[] menuText = new Text[3];
    int Select = 0;

    [Header("Camera")]
    public Transform camController;
    public Transform cam;
    public float camSpeed = 0.5f;
    float mouseX;
    float mouseY = 1;
    float mouseWheel = -3f;

    bool ischange = false;
    bool isEsc = false;
    #endregion

    Vector3 mytransform;
    Transform rayTransform;
    float direct;
    bool istrigger;

    Vector3 dicCam;
    float Camdirect;
    [SerializeField] Image bossRotate;

    private void Start()
    {
        mytransform = this.transform.position;
        rayTransform = GameObject.Find("RayObject").transform;
        cam = Camera.main.transform;
        camController = GameObject.Find("CameraParent").transform;
    }

    public Transform mei_trans;
    public Transform bossTrans;
    public static bool isLook = false;

    void CameraLookAt()
    {
        if (bossTrans != null)
        {
            mei_trans.localRotation = Quaternion.identity;
            Vector3 dis = bossTrans.position - camController.position;
            camController.localRotation = Quaternion.Slerp(camController.localRotation, Quaternion.LookRotation(dis), Time.deltaTime * 5);
            Vector3 dis2 = bossTrans.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dis2), Time.deltaTime * 5);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (isLook == false)
            {
                bossRotate.gameObject.SetActive(true);
                isLook = true;
            }
            else if (isLook == true)
            {
                bossRotate.gameObject.SetActive(false);
                isLook = false;
            }
        }

        if (isLook == true)
            CameraLookAt();
        else
        {
            CamMove();
        }
        Zoom();
        //if (camController.gameObject.activeSelf) // 평상시일떄
        //{
        //    if (!isEsc)
        //    {
        //        CamMove();
        //        if (!istrigger)
        //        {
        //            Zoom();
        //        }
        //        if (Input.GetKeyDown(KeyCode.LeftControl) && !ischange)
        //        {
        //            if (PlayerStateManager.playerSelect == 1)
        //            {
        //                PlayerStateManager.playerSelect = 2;
        //            }
        //            else if (PlayerStateManager.playerSelect == 2)
        //            {
        //                PlayerStateManager.playerSelect = 1;
        //            }
        //            StartCoroutine(EffectSet());
        //            SetCharacter();
        //        }
        //    }
        //    else if (isEsc)
        //    {
        //        if (Input.GetKeyDown(KeyCode.UpArrow))
        //        {
        //            Select--;
        //            SelectMenu();
        //        }
        //        if (Input.GetKeyDown(KeyCode.DownArrow))
        //        {
        //            Select++;
        //            SelectMenu();
        //        }
        //    }
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        if (!escBakcGround.gameObject.activeSelf)
        //        {
        //            isEsc = true;
        //            Time.timeScale = 0.0f;
        //            escBakcGround.gameObject.SetActive(true);
        //        }
        //        else if (escBakcGround.gameObject.activeSelf)
        //        {
        //            isEsc = false;
        //            Time.timeScale = 1.0f;
        //            escBakcGround.gameObject.SetActive(false);
        //        }
        //    }
        //    if (Input.GetKeyDown(KeyCode.Space) && isEsc)
        //    {
        //        NumKeyPress();
        //    }
        //}
    }

    [SerializeField] GameObject Opstion;
    //void NumKeyPress()
    //{
    //    switch (Select)
    //    {
    //        case 0:
    //            isEsc = false;
    //            Time.timeScale = 1.0f;
    //            escBakcGround.gameObject.SetActive(false);
    //            break;
    //        case 1:
    //            Opstion.SetActive(true);
    //            break;
    //        case 2:
    //            Time.timeScale = 1.0f;
    //            break;
    //    }
    //}

    //void SelectMenu()
    //{
    //    Select = Mathf.Clamp(Select, 0, 2);
    //    Color selectColor;

    //    for (int i = 0; i < menuText.Length; i++)
    //    {
    //        selectColor = menuText[i].color;
    //        if (i == Select)
    //        {
    //            selectColor.a = 1.0f;
    //            menuText[i].color = selectColor;
    //        }
    //        else if (i != Select)
    //        {
    //            selectColor.a = 0.5f;
    //            menuText[i].color = selectColor;
    //        }
    //    }
    //}

    //void SetCharacter()
    //{
    //    SoundsManager.Instance.OnCtrlKeySound();
    //    if (PlayerStateManager.playerSelect == 1)
    //    {
    //        imageObject[1].transform.localPosition = new Vector3(-2306.6f, -634, 0);
    //        imageObject[0].transform.localPosition = new Vector3(-2306.6f, -456, 0);
    //        setCharacter[0].transform.position = setCharacter[1].transform.position;
    //        setCharacter[0].transform.rotation = setCharacter[1].transform.rotation;
    //        setCharacter[0].SetActive(true);
    //        setCharacter[1].SetActive(false);
    //    }
    //    else if (PlayerStateManager.playerSelect == 2)
    //    {
    //        imageObject[1].transform.localPosition = new Vector3(-2306.6f, -456, 0);
    //        imageObject[0].transform.localPosition = new Vector3(-2306.6f, -634, 0);
    //        setCharacter[1].transform.position = setCharacter[0].transform.position;
    //        setCharacter[1].transform.rotation = setCharacter[0].transform.rotation;
    //        setCharacter[0].SetActive(false);
    //        setCharacter[1].SetActive(true);
    //    }
    //}

    void CamMove()
    {
        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, 0, 12);

        camController.rotation = Quaternion.Euler(
            new Vector3(camController.rotation.x + mouseY, camController.rotation.y + mouseX, 0) * 3.0f);
    }

    void Zoom()
    {
        mouseWheel += Input.GetAxis("Mouse ScrollWheel") * 10;
        mouseWheel = Mathf.Clamp(mouseWheel, -6, -2);
        Vector3 temp = new Vector3(0, 1, mouseWheel);
        cam.localPosition = Vector3.Lerp(cam.localPosition, temp, 0.1f);
        rayTransform.localPosition = temp;
    }

    //IEnumerator EffectSet()
    //{
    //    ischange = true;
    //    if (PlayerStateManager.playerSelect == 1)
    //    {
    //        effect[0].SetActive(true);
    //    }
    //    else if (PlayerStateManager.playerSelect == 2)
    //    {
    //        effect[1].SetActive(true);
    //    }
    //    yield return new WaitForSeconds(1.0f);
    //    //yield return new WaitForSecondsRealtime(1.0f); // 인벤토리창 눌러도 이펙트사라지게하고싶을떄
    //    if (effect[0].activeSelf)
    //    {
    //        effect[0].SetActive(false);
    //    }
    //    if (effect[1].activeSelf)
    //    {
    //        effect[1].SetActive(false);
    //    }
    //    ischange = false;
    //}
}
