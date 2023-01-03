using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine.UI;

public class FireBaseManager : MonoBehaviour
{
    FirebaseAuth auth; // �α��� ȸ������ ���
    FirebaseUser user; // ������ �Ϸ�� ���� ����

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
        auth = FirebaseAuth.DefaultInstance; // �ʱ�ȭ
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
            if(task.IsCanceled) // ȸ������ ���߿� ĵ���Ǹ� ���
            {
                Debug.Log("ȸ������ ���");
                userID2 = "ȸ������ ����";
                return;
            }
            if(task.IsFaulted)
            {
                Debug.Log("ȸ������ ����");
                userID2 = "ȸ������ ����";
                // ȸ������ ���� == �̸��� ������ / ��й�ȣ�� ���� / �̹� ���Ե� �̸���
                return;
            }

            Debug.Log("ȸ������ ����");
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
            if (task.IsCanceled) // ȸ������ ���߿� ĵ���Ǹ� ���
            {
                Debug.Log("�α��ν���");
                userID2 = "�α��ν���";
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("�α��ν���");
                userID2 = "�α��ν���";
                // ȸ������ ���� == �̸��� ������ / ��й�ȣ�� ���� / �̹� ���Ե� �̸���
                return;
            }

            isStart = true;
            emailTemp = email.text;
            Debug.Log(isStart);
            Debug.Log("�α��� ����");
            userID2 = "�α��μ���";
            FirebaseUser newUser = task.Result;
            user = auth.CurrentUser;
            userID = user.UserId;
        });
    }

    public void LogOut()
    {
        auth.SignOut();
        Debug.Log("�α׾ƿ�");
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
        Debug.Log("���ӽ��� ����");
        startObject.SetActive(true);
        backObject.SetActive(false);
        loginPanel.SetActive(false);
        gameObject.SetActive(false);
    }
}
