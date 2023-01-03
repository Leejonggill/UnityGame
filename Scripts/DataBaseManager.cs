using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Unity;

public class DataBaseManager : MonoBehaviour
{
    public class Data
    {
        //public string email;

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

        public Data(PlayerStateManager player1)
        {
            for (int i = 0; i < 2; i++)
            {
                level[i] = player1.player[i].level;
                maxHp[i] = player1.player[i].maxHp;
                currenthp[i] = player1.player[i].currenthp;
                maxMp[i] = player1.player[i].maxMp;
                currentMp[i] = player1.player[i].currentMp;
                str[i] = player1.player[i].str;
                def[i] = player1.player[i].def;
                Evaison[i] = player1.player[i].Evaison;
                maxExp[i] = player1.player[i].maxExp;
                currentExp[i] = player1.player[i].currentExp;
                sword[i] = player1.player[i].sword;
                helmet[i] = player1.player[i].helmet;
                armor[i] = player1.player[i].armor;
                pants[i] = player1.player[i].pants;
            }

            player1.SaveItem();

            //for(int i=0; i<20; i++)
            //{
            //    itemLists[i] = ItemList.None;
            //    itemCount[i] = 0;
            //}

            for (int i = 0; i < 20; i++)
            {
                itemLists[i] = player1.playerSaveSlot[i].itemList;
                itemCount[i] = player1.playerSaveSlot[i].count;
            }
            gold = player1.Gold;
        }

        //public int level;
        //public int[] maxHp = new int[2];
        //public int currenthp;
        //public int maxMp;
        //public int currentMp;
        //public int str;
        //public int def;
        //public int Evaison;
        //public int maxExp;
        //public int currentExp;
        //public int gold;

        //public Data(PlayerStateManager player1)
        //{
        //    level = player1.player[0].level;
        //    maxHp[0] = player1.player[0].maxHp;
        //    maxHp[1] = player1.player[1].maxHp;
        //    currenthp = player1.player[0].currenthp;
        //    maxMp = player1.player[0].maxMp;
        //    currentMp = player1.player[0].currentMp;
        //    str = player1.player[0].str;
        //    def = player1.player[0].def;
        //    Evaison = player1.player[0].Evaison;
        //    maxExp = player1.player[0].maxExp;
        //    currentExp = player1.player[0].currentExp;
        //    gold = player1.Gold;
        //}
    }

    DatabaseReference reference;

    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        if (isLoad == false)
        {
            StartCoroutine(load1());
        }
    }

    private void Update()
    {
        if (isLoad == true && isSave == false)
        {
            StartCoroutine(Save1());
        }
    }

    public void OnSave()
    {
        string temp = FireBaseManager.emailTemp;
        Debug.Log(temp);
        writeNewUser("User", FireBaseManager.userID);
    }
    
    public void LoadBtn()
    {
        readUser(FireBaseManager.userID);
    }

    public void writeNewUser(string userEmail,string userID)
    {
        Data userData = new Data(PlayerStateManager.Instance);
        userData.isFirst = false;
        //userData.email = FireBaseManager.emailTemp;
        string json = JsonUtility.ToJson(userData);
        reference.Child(userID).SetRawJsonValueAsync(json);
    }

    public static bool isLoad = false;
    public bool isSave = false;
    IEnumerator load1()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        readUser(FireBaseManager.userID);
        yield return new WaitForSecondsRealtime(1.0f);
        isLoad = true;
        PlayerStateManager.Instance.LoadItem();
    }

    IEnumerator Save1()
    {
        isSave = true;
        yield return new WaitForSecondsRealtime(1.0f);
        string temp = FireBaseManager.emailTemp;
        Debug.Log("Save");
        writeNewUser("User", FireBaseManager.userID);
        yield return new WaitForSecondsRealtime(1.0f);
        isSave = false;
    }

    public void readUser(string userID)
    {
        reference.Child(userID).GetValueAsync().ContinueWith(task =>
        {
            if(task.IsFaulted)
            {
                Debug.Log("실패");
            }
            else if(task.IsCompleted)
            {
                var snapshot = task.Result;

                //Data json = JsonUtility.FromJson<Data>(userID);
                //Debug.Log(json.gold);
                //Debug.Log(snapshot.Child("Evaison"));
                //Debug.Log(snapshot.Child("Evaison[0]"));

                Debug.Log(snapshot.ChildrenCount);
                //string dataString = "";
                //int value = 0;

                //foreach (var data in snapshot.Children)
                //{

                //    //dataString += data.Key + " " + data.Value + "\n";
                //    //Debug.Log(dataString);
                //    //Debug.Log(snapshot.ChildrenCount);
                //    Debug.Log("child"+snapshot.Child("gold").Value);
                //    Debug.Log("level" + snapshot.Child("level").Value);

                //    //IDictionary dictionary = (IDictionary)data.Value;
                //    //PlayerStateManager.Instance.Gold = (int)dictionary["gold"];
                //    //Debug.Log("gold" + dictionary["gold"]);
                //}

                // 되는것들
                //Debug.Log("gold " + snapshot.Child("gold").Value);
                //Debug.Log("level" + snapshot.Child("level").Value);
                //Debug.Log("maxHp" + snapshot.Child("maxHp").Child("0").Value);
                //Debug.Log("maxHp" + snapshot.Child("maxHp").Child("1").Value);

                //dataString += snapshot.Child("gold").Value;
                //value = int.Parse(dataString);
                //Debug.Log("PlayerGold" + dataString);
                //Debug.Log("PlayerGold2 " + value);
                //PlayerStateManager.Instance.Gold = value;
                //Debug.Log("PlayerGold3 " + PlayerStateManager.Instance.Gold);

                SetPlayerState(snapshot);
            }
        });
    }

    void SetPlayerState(DataSnapshot data)
    {
        PlayerStateManager playerState = PlayerStateManager.Instance;

        string dataString="";
        dataString += data.Child("gold").Value;
        playerState.Gold = int.Parse(dataString);

        dataString = "";
        dataString += data.Child("maxHp").Child("0").Value;
        playerState.player[0].maxHp = int.Parse(dataString);

        dataString = "";
        dataString += data.Child("maxHp").Child("1").Value;
        playerState.player[1].maxHp = int.Parse(dataString);

        dataString = "";
        dataString += data.Child("currenthp").Child("0").Value;
        playerState.player[0].currenthp = int.Parse(dataString);

        dataString = "";
        dataString += data.Child("currenthp").Child("1").Value;
        playerState.player[1].currenthp = int.Parse(dataString);

        dataString = "";
        dataString += data.Child("maxMp").Child("0").Value;
        playerState.player[0].maxMp = int.Parse(dataString);

        dataString = "";
        dataString += data.Child("maxMp").Child("1").Value;
        playerState.player[1].maxMp = int.Parse(dataString);

        dataString = "";
        dataString += data.Child("currentMp").Child("0").Value;
        playerState.player[0].currentMp = int.Parse(dataString);

        dataString = "";
        dataString += data.Child("currentMp").Child("1").Value;
        playerState.player[1].currentMp = int.Parse(dataString);

        dataString = "";
        dataString += data.Child("maxExp").Child("0").Value;
        playerState.player[0].maxExp = int.Parse(dataString);

        dataString = "";
        dataString += data.Child("maxExp").Child("1").Value;
        playerState.player[1].maxExp = int.Parse(dataString);

        dataString = "";
        dataString += data.Child("currentExp").Child("0").Value;
        playerState.player[0].currentExp = int.Parse(dataString);

        dataString = "";
        dataString += data.Child("currentExp").Child("1").Value;
        playerState.player[1].currentExp = int.Parse(dataString);

        dataString = "";
        dataString += data.Child("Evaison").Child("0").Value;
        playerState.player[0].Evaison = int.Parse(dataString);

        dataString = "";
        dataString += data.Child("Evaison").Child("1").Value;
        playerState.player[1].Evaison = int.Parse(dataString);

        dataString = "";
        dataString += data.Child("str").Child("0").Value;
        playerState.player[0].str = int.Parse(dataString);

        dataString = "";
        dataString += data.Child("str").Child("1").Value;
        playerState.player[1].str = int.Parse(dataString);

        dataString = "";
        dataString += data.Child("def").Child("0").Value;
        playerState.player[0].def = int.Parse(dataString);

        dataString = "";
        dataString += data.Child("def").Child("1").Value;
        playerState.player[1].def = int.Parse(dataString);

        dataString = "";
        dataString += data.Child("level").Child("0").Value;
        playerState.player[0].level = int.Parse(dataString);

        dataString = "";
        dataString += data.Child("level").Child("1").Value;
        playerState.player[1].level = int.Parse(dataString);

        //playerState.LoadEquimentItem(0);
        //playerState.LoadEquimentItem(1);
        //Debug.Log(playerState.player[0].sword);
        //SetItem(data);

        //dataString = "";
        //dataString += data.Child("sword").Child("0").Value;
        //int enum1 = int.Parse(dataString);
        //playerState.player[0].sword = (Sword)enum1;

        SetItem(data);
    }

    void SetItem(DataSnapshot data)
    {
        PlayerStateManager playerState = PlayerStateManager.Instance;

        string dataString = "";
        dataString += data.Child("sword").Child("0").Value;
        int enum1 = int.Parse(dataString);
        playerState.player[0].sword = (Sword)enum1;

        dataString = "";
        dataString += data.Child("sword").Child("1").Value;
        enum1 = int.Parse(dataString);
        playerState.player[1].sword = (Sword)enum1;

        dataString = "";
        dataString += data.Child("pants").Child("0").Value;
        enum1 = int.Parse(dataString);
        playerState.player[0].pants = (Pants)enum1;

        dataString = "";
        dataString += data.Child("pants").Child("1").Value;
        enum1 = int.Parse(dataString);
        playerState.player[1].pants = (Pants)enum1;

        dataString = "";
        dataString += data.Child("armor").Child("0").Value;
        enum1 = int.Parse(dataString);
        playerState.player[0].armor = (Armor)enum1;

        dataString = "";
        dataString += data.Child("armor").Child("1").Value;
        enum1 = int.Parse(dataString);
        playerState.player[1].armor = (Armor)enum1;

        dataString = "";
        dataString += data.Child("helmet").Child("0").Value;
        enum1 = int.Parse(dataString);
        playerState.player[0].helmet = (Helmet)enum1;

        dataString = "";
        dataString += data.Child("helmet").Child("1").Value;
        enum1 = int.Parse(dataString);
        playerState.player[1].helmet = (Helmet)enum1;

        Debug.Log(playerState.player[0].sword);
        LoadInventory(data);
    }

    void LoadInventory(DataSnapshot data)
    {
        Debug.Log("인벤토리 로드진행");
        PlayerStateManager playerState = PlayerStateManager.Instance;

        for (int i = 0; i < 20; i++)
        {
            string dataString = "";
            dataString += data.Child("itemLists").Child(i.ToString()).Value;
            int enum1 = int.Parse(dataString);
            playerState.playerSaveSlot[i].itemList = (ItemList)enum1;
            Debug.Log(playerState.playerSaveSlot[i].itemList);
        }

        for (int i = 0; i < 20; i++)
        {
            string dataString = "";
            dataString += data.Child("itemCount").Child(i.ToString()).Value;
            playerState.playerSaveSlot[i].count = int.Parse(dataString);
        }
    }
}
