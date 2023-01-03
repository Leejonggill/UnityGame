using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine.UI;

public class FireBaseManager : MonoBehaviour
{
    FirebaseAuth auth; // 로그인 회원가입 사용
    FirebaseUser user; // 인증이 완료된 유저 정보

    [SerializeField] InputField email;
    [SerializeField] InputField password;
    [SerializeField] GameObject startObject;
    [SerializeField] GameObject backObject;
    [SerializeField] GameObject loginPanel;

    public static string emailTemp;
    public static string userID;
    public static string userID2;

    public class Data
    {
        public int[] level = new int[2];
        public int[] maxHp = new int[2];
        public int[] currenthp = new int[2];
        public int[] maxMp = new int[2];
        public int[] currentMp = new int[2];
        public int[] str = new int[2];
        public int[] def = new int[2];
        public int[] Evaison = new int[2];
        public int[] maxExp = new int[2];
        public int[] currentExp = new int[2];
        public int gold;
        public Sword[] sword = new Sword[2];
        public Helmet[] helmet = new Helmet[2];
        public Armor[] armor = new Armor[2];
        public Pants[] pants = new Pants[2];

        public ItemList[] itemLists = new ItemList[20];
        public int[] itemCount = new int[20];
        public bool isFirst = false;

        public void CreateCharcter(int nNum, int nLevel, int nHp, int nMp, int nStr, int nDef, int nEvaison, int nExp)
        {
            level[nNum] = nLevel;
            maxHp[nNum] = nHp;
            currenthp[nNum] = nHp;
            maxMp[nNum] = nMp;
            currentMp[nNum] = nMp;
            str[nNum] = nStr;
            def[nNum] = nDef;
            Evaison[nNum] = nEvaison;
            maxExp[nNum] = nExp;
            currentExp[nNum] = 0;
            sword[nNum] = Sword.None;
            helmet[nNum] = Helmet.None;
            armor[nNum] = Armor.None;
            pants[nNum] = Pants.None;
        }

        public void CreateGoldInven()
        {
            for (int i = 0; i < 20; i++)
            {
                itemLists[i] = ItemList.None;
                itemCount[i] = 0;
            }
            gold = 1000;
        }
    }

    DatabaseReference reference;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance; // 초기화
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void writeNewUser(string userID)
    {
        Data userData = new Data();
        userData.CreateCharcter(0, 1, 200, 100, 30, 10, 10, 200);
        userData.CreateCharcter(1, 1, 300, 100, 20, 20, 10, 200);
        userData.CreateGoldInven();
        userData.isFirst = false;
        string json = JsonUtility.ToJson(userData);
        reference.Child(userID).SetRawJsonValueAsync(json);
    }

    public void Crate()
    {
        auth.CreateUserWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task => 
        { 
            if(task.IsCanceled) // 회원가입 도중에 캔슬되면 취소
            {
                Debug.Log("회원가입 취소");
                userID2 = "회원가입 실패";
                return;
            }
            if(task.IsFaulted)
            {
                Debug.Log("회원가입 실패");
                userID2 = "회원가입 실패";
                // 회원가입 실패 == 이메일 비정상 / 비밀번호가 간단 / 이미 가입된 이메일
                return;
            }

            Debug.Log("회원가입 성공");
            FirebaseUser newUser = task.Result;
            userID2 = "ID :"+newUser.UserId;
            writeNewUser(newUser.UserId);
        });
    }

    bool isStart = false;

    public void Login()
    {
        auth.SignInWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task =>
        {
            if (task.IsCanceled) // 회원가입 도중에 캔슬되면 취소
            {
                Debug.Log("로그인실패");
                userID2 = "로그인실패";
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("로그인실패");
                userID2 = "로그인실패";
                // 회원가입 실패 == 이메일 비정상 / 비밀번호가 간단 / 이미 가입된 이메일
                return;
            }

            isStart = true;
            emailTemp = email.text;
            Debug.Log(isStart);
            Debug.Log("로그인 성공");
            userID2 = "로그인성공";
            FirebaseUser newUser = task.Result;
            user = auth.CurrentUser;
            userID = user.UserId;
        });
    }

    public void LogOut()
    {
        auth.SignOut();
        Debug.Log("로그아웃");
    }

    IEnumerator GameStartLoading()
    {
        yield return new WaitForSeconds(1.0f);
        if (isStart)
        {
            GameStart();
        }
    }

    public void GameStart2()
    {
        StartCoroutine(GameStartLoading());
    }

    public void GameStart()
    {
        Debug.Log("게임시작 로직");
        startObject.SetActive(true);
        backObject.SetActive(false);
        loginPanel.SetActive(false);
        gameObject.SetActive(false);
    }
}
