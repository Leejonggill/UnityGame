using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossPlayerCon : MonoBehaviour
{
    //public static int playerSelect = 1;

    [Header("Camera")]
    public Transform camController;
    public Transform cam;

    [Header("Player")]
    public Transform playerController;
    public Transform playerTransform;
    float playerSpeed = 6.0f;

    Vector3 movement;
    Animator playerAnim;
    bool isRoll2 = false;
    bool isAttacked = false;
    bool isEvasion = false;
    bool isCombo = false;
    int comboCount = 0;

    readonly int isRun = Animator.StringToHash("isRun");
    readonly int isRoll = Animator.StringToHash("isRoll");
    readonly int isAttack = Animator.StringToHash("isAttack");
    readonly int isAttack2 = Animator.StringToHash("isAttack2");
    readonly int isAttack3 = Animator.StringToHash("isAttack3");
    readonly int isLeft = Animator.StringToHash("isLeft");
    readonly int isRight = Animator.StringToHash("isRight");
    readonly int isBack = Animator.StringToHash("isBack");
    readonly int isIdle = Animator.StringToHash("isIdle");

    [SerializeField] GameObject effect;

    int hpPostion = 3;

    private void OnEnable()
    {
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

    [SerializeField] Text text;
    float time = 0;
    private void Update()
    {
        time += Time.deltaTime;
        if (transform.localPosition.y < -0.1)
        {
            transform.localPosition = new Vector3(0, 0, 0);
        }
        else if (transform.localPosition.y > 0.1)
        {
            transform.localPosition = new Vector3(0, 0, 0);
        }

        if(hpPostion>0&&Input.GetKeyDown(KeyCode.B))
        {
            hpPostion--;
            PlayerStateManager.Instance.HpResult(0, 50);
            text.text = hpPostion.ToString();
            SoundsManager.Instance.OnPostionSound();
        }

        Move();
        Roll();
        Attack();

        if (PlayerStateManager.Instance.player[0].currentMp < 100&& time>1)
        {
            time = 0;
            PlayerStateManager.Instance.MpResult(0, 5);
        }
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        movement = new Vector3(h, 0, v);

        if (movement != Vector3.zero)
        {

            Quaternion cameraRotation = camController.rotation;
            Quaternion playerRotation = playerController.rotation;
            cameraRotation.x = 0; // y축이 앞뒤좌우 이기때문에 y축만 바뀌게 하기
            cameraRotation.z = 0; // 안넣어도 상관없는데 가끔식 camContorller.roation x z 가
            playerRotation.x = 0; // PlayerController x z 값이 다를때가있음.
            playerRotation.z = 0;
            if (BossPlayerCameraCon.isLook == false)
            {
                playerController.localRotation = Quaternion.Slerp(playerRotation, cameraRotation, 10.0f * Time.deltaTime);
            }

            if (isAttacked == false)
            {
                playerController.Translate(movement * Time.deltaTime * playerSpeed);
            }
            playerController.position = playerTransform.position;
            playerTransform.localPosition = new Vector3(0, 0, 0);

            playerTransform.localRotation = Quaternion.Slerp(playerTransform.localRotation, Quaternion.LookRotation(movement), 10.0f * Time.deltaTime);

            if (isAttacked == false&& BossPlayerCameraCon.isLook==false)
            {
                playerAnim.SetBool(isRun, true);
                playerAnim.SetBool(isLeft, false);
                playerAnim.SetBool(isRight, false);
                playerAnim.SetBool(isBack, false);
            }
        }
        else
        {
            playerAnim.SetBool(isRun, false);
            playerAnim.SetBool(isLeft, false);
            playerAnim.SetBool(isRight, false);
            playerAnim.SetBool(isBack, false);
        }


        if (BossPlayerCameraCon.isLook == true)
        {
            playerAnim.SetBool(isRun, false);
            playerTransform.localRotation = Quaternion.Euler(0, 0, 0);
            if (h >= 0.1f)
            {
                playerAnim.SetBool(isLeft, false);
                playerAnim.SetBool(isRight, true);
                Debug.Log("오른");
            }
            if (h <= -0.1f)
            { 
                playerAnim.SetBool(isLeft, true);
                playerAnim.SetBool(isRight, false);
                Debug.Log("왼");
            }
            if(v != 0)
            {
                playerAnim.SetBool(isBack, true);
                playerAnim.SetBool(isLeft, false);
                playerAnim.SetBool(isRight, false);
            }

            //if(h == 0 && v ==0)
            //{
            //    playerAnim.SetBool(isIdle, true);
            //    playerAnim.SetBool(isLeft, false);
            //    playerAnim.SetBool(isRight, false);
            //    playerAnim.SetBool(isBack, false);
            //}
            //else
            //{
            //    playerAnim.SetBool(isIdle, false);
            //}
        }

        camController.position = playerTransform.position + new Vector3(0, 0.5f, 0);
    }

    void Roll()
    {
        if (PlayerStateManager.Instance.player[0].currentMp > 10)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isRoll2)
            {
                PlayerStateManager.Instance.player[0].currentMp -= 10;
                isAttacked = false;
                playerAnim.SetTrigger(isRoll);
                isEvasion = true;
                isRoll2 = true;
            }
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0)&& PlayerStateManager.Instance.player[0].currentMp > 10)
        {
            if (isAttacked == false)
            {
                playerAnim.SetBool(isAttack, true);
                if(comboCount==0)
                {
                    playerAnim.SetBool(isRun, false);
                    comboCount++;
                    isAttacked = true;
                }
            }
            else
            {
                isCombo = true;
            }
        }
    }


    [SerializeField] GameObject SwordAttack;

    void SetAcitveFalseAttack()
    {
        SwordAttack.SetActive(false);
    }

    void SetAcitveTrueAttack()
    {
        SwordAttack.SetActive(true);
        StartCoroutine(ActiveFalse());
    }

    void AttackSta()
    {
        PlayerStateManager.Instance.player[0].currentMp -= 10;
    }

    IEnumerator ActiveFalse()
    {
        yield return new WaitForSeconds(0.2f);
        Debug.Log("공격코루틴");
        SwordAttack.SetActive(false);
    }

    void AttackComBo()
    {
        if (comboCount==1&&isCombo && PlayerStateManager.Instance.player[0].currentMp > 10)
        {
            //playerAnim.SetBool(isAttack, false);
            playerAnim.SetBool(isAttack2, true);
            isCombo = false;
            comboCount++;
        }
        else if(comboCount == 1 && isCombo==false)
        {
            playerAnim.SetBool(isAttack, false);
        }
    }

    void AttackComBo2()
    {
        if (isCombo&& PlayerStateManager.Instance.player[0].currentMp > 10)
        {
            Debug.Log("콤보 마지막진입");
            //playerAnim.SetBool(isAttack, false);
            playerAnim.SetBool(isAttack3, true);
            isCombo = false;
        }
        else if (isCombo == false)
        {
            playerAnim.SetBool(isAttack2, false);
        }
    }

    void AttackComboReset()
    {
        Debug.Log("콤보 리셋");
        isAttacked = false;
        playerAnim.SetBool(isAttack, false);
        playerAnim.SetBool(isAttack2, false);
        playerAnim.SetBool(isAttack3, false);
        isCombo = false;
          comboCount = 0;
    }

    void AnimEndRoll()
    {
        isEvasion = false;
        isRoll2 = false;
    }

    void RollEvasion()
    {
        isEvasion = true;
    }

    void RollEvasionFasle()
    {
        isEvasion = false;
    }

    void SoundAttack()
    {
        SoundsManager.Instance.OnMaleAttackSound(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BossAttack"))
        {
            if (isEvasion == false)
            {
                SoundsManager.Instance.OnHitPlayer();
                Instantiate(effect, transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity);
                other.gameObject.SetActive(false);
                PlayerStateManager.Instance.player[0].currenthp -= 20;
            }
        }
    }
}
