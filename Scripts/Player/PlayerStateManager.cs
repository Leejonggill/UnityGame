using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sword
{
    None,
    Sword1,
    Sword2
}

public enum Armor
{
    None,
    Armor1,
    Armor2
}

public enum Helmet
{
    None,
    Helmet1,
    Helmet2
}

public enum Pants
{
    None,
    Pants1,
    Pants2
}

public enum ItemList
{
    None,
    HpPostion,
    MpPostion,
    Helmet,
    Helmet2,
    Armor,
    Armor2,
    Pants,
    Pants2,
    Sword,
    Sword2
}

public class PlayerStateManager : MonoBehaviour
{
    #region test
    //private static bool isDestory = false;

    //private void Awake()
    //{
    //    if(isDestory==false)
    //    {
    //        isDestory = true;
    //        DontDestroyOnLoad(gameObject);
    //        return;
    //    }
    //    else if(isDestory==true)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    #endregion

    #region SingleTon
    private static PlayerStateManager instance = null;

    public static PlayerStateManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Variable
    public struct playerStaete
    {
        public int level;
        public int maxHp;
        public int currenthp;
        public int maxMp;
        public int currentMp;
        public int str;
        public int def;
        public int Evaison;
        public int maxExp;
        public int currentExp;
        public Item[] itesms;
        public Sword sword;
        public Helmet helmet;
        public Pants pants;
        public Armor armor;
    };

    public struct PlayerSaveSlot
    {
        public ItemList itemList;
        public int count;
    }
    public PlayerSaveSlot[] playerSaveSlot;


    public static int playerSelect = 1;
    public int Gold = 1000;
    public playerStaete[] player = new playerStaete[2];

    #endregion
    private void Start() // »ý¼ºÀÚ
    {
        playerSaveSlot = new PlayerSaveSlot[20];
        InvenSet();

        player[0].itesms = new Item[4];
        player[1].itesms = new Item[4];
        CreateCharcter(0, 1, 200, 100, 30, 10, 10, 200);
        CreateCharcter(1, 1, 300, 100, 20, 20, 10, 200);
        ItemSet(0, Sword.None, Armor.None, Pants.None, Helmet.None);
        ItemSet(1, Sword.None, Armor.None, Pants.None, Helmet.None);
    }

    public void ItemSet(int Select, Sword sword, Armor armor, Pants pants, Helmet helmet)
    {
        player[Select].sword = sword;
        player[Select].armor = armor;
        player[Select].pants = pants;
        player[Select].helmet = helmet;
    }

    public void InvenSet()
    {
        for (int i = 0; i < 20; i++)
        {
            playerSaveSlot[i].count = 0;
            playerSaveSlot[i].itemList = ItemList.None;
        }
    }

    public void SaveItem()
    {
        Inventory inventory = Inventory.Instance;
        //int z = 0;
        //for(int i=0; i<playerSaveSlot.Length;i++)
        //{
        //    for(int j=0; j+z<inventory.slotMax;j++)
        //    {
        //        if(inventory.slots[j]!=null)
        //        {
        //            z++;
        //            inventory.slots[j].item = playerSaveSlot[i].item;
        //            inventory.slots[j].itemCount = playerSaveSlot[i].count;
        //            break;
        //        }
        //    }
        //}

        //for (int i = 0; i < 20; i++)
        //{
        //    playerSaveSlot[i].itemList = ItemList.None;
        //    playerSaveSlot[i].count = 0;
        //}

        int j = 0;
        for (int i = 0; i < inventory.slotMax; i++)
        {
            if (inventory.slots[i].item != null)
            {
                playerSaveSlot[j].count = inventory.slots[i].itemCount;
                switch (inventory.slots[i].item.itemName)
                {
                    case "HpPostion":
                        playerSaveSlot[j].itemList = ItemList.HpPostion;
                        break;
                    case "MpPostion":
                        playerSaveSlot[j].itemList = ItemList.MpPostion;
                        break;
                    case "Helmet":
                        playerSaveSlot[j].itemList = ItemList.Helmet;
                        break;
                    case "Helmet2":
                        playerSaveSlot[j].itemList = ItemList.Helmet2;
                        break;
                    case "Armor":
                        playerSaveSlot[j].itemList = ItemList.Armor;
                        break;
                    case "Armor2":
                        playerSaveSlot[j].itemList = ItemList.Armor2;
                        break;
                    case "Pants":
                        playerSaveSlot[j].itemList = ItemList.Pants;
                        break;
                    case "Pants2":
                        playerSaveSlot[j].itemList = ItemList.Pants2;
                        break;
                    case "Sword":
                        playerSaveSlot[j].itemList = ItemList.Sword;
                        break;
                    case "Sword2":
                        playerSaveSlot[j].itemList = ItemList.Sword2;
                        break;
                }
                j++;
            }
            else
            {
                playerSaveSlot[j].itemList = ItemList.None;
                playerSaveSlot[j].count = 0;
                j++;
            }
        }
    }

    public bool isLoad = false;
    public void LoadItem()
    {
        if (isLoad == false)
        {
            isLoad = true;
            Inventory inventory = Inventory.Instance;

            int j = 0;
            for (int i = 0; i < 20; i++)
            {
                if (inventory.slots[j].item == null)
                {
                    switch (playerSaveSlot[i].itemList)
                    {
                        case ItemList.HpPostion:
                            Item item;
                            item = Resources.Load<Item>("HpPostion");
                            inventory.slots[j].AddItem(item, playerSaveSlot[i].count);
                            j++;
                            break;
                        case ItemList.MpPostion:
                            item = Resources.Load<Item>("MpPostion");
                            inventory.slots[j].AddItem(item, playerSaveSlot[i].count);
                            j++;
                            break;
                        case ItemList.Helmet:
                            item = Resources.Load<Item>("Helmet");
                            inventory.slots[j].AddItem(item, playerSaveSlot[i].count);
                            j++;
                            break;
                        case ItemList.Helmet2:
                            item = Resources.Load<Item>("Helmet2");
                            inventory.slots[j].AddItem(item, playerSaveSlot[i].count);
                            j++;
                            break;
                        case ItemList.Armor:
                            item = Resources.Load<Item>("Armor");
                            inventory.slots[j].AddItem(item, playerSaveSlot[i].count);
                            j++;
                            break;
                        case ItemList.Armor2:
                            item = Resources.Load<Item>("Armor2");
                            inventory.slots[j].AddItem(item, playerSaveSlot[i].count);
                            j++;
                            break;
                        case ItemList.Pants:
                            item = Resources.Load<Item>("Pants");
                            inventory.slots[j].AddItem(item, playerSaveSlot[i].count);
                            j++;
                            break;
                        case ItemList.Pants2:
                            item = Resources.Load<Item>("Pants2");
                            inventory.slots[j].AddItem(item, playerSaveSlot[i].count);
                            j++;
                            break;
                        case ItemList.Sword:
                            item = Resources.Load<Item>("Sword");
                            inventory.slots[j].AddItem(item, playerSaveSlot[i].count);
                            j++;
                            break;
                        case ItemList.Sword2:
                            item = Resources.Load<Item>("Sword2");
                            inventory.slots[j].AddItem(item, playerSaveSlot[i].count);
                            j++;
                            break;
                        default:
                            Debug.Log("defaul");
                            break;
                    }
                    Debug.Log(j);
                }
            }
        }
    }

    public void LoadEquimentItem(int Select)
    {
        switch (player[Select].sword)
        {
            case Sword.None:
                break;
            case Sword.Sword1:
                Item item;
                item = Resources.Load<Item>("Sword");
                player[Select].itesms[3] = item;
                break;
            case Sword.Sword2:
                item = Resources.Load<Item>("Sword2");
                player[Select].itesms[3] = item;
                break;
            default:
                break;
        }

        switch (player[Select].helmet)
        {
            case Helmet.None:
                break;
            case Helmet.Helmet1:
                Item item;
                item = Resources.Load<Item>("Helmet");
                player[Select].itesms[0] = item;
                break;
            case Helmet.Helmet2:
                item = Resources.Load<Item>("Helmet2");
                player[Select].itesms[0] = item;
                break;
            default:
                break;
        }

        switch (player[Select].armor)
        {
            case Armor.None:
                break;
            case Armor.Armor1:
                Item item;
                item = Resources.Load<Item>("Armor");
                player[Select].itesms[1] = item;
                break;
            case Armor.Armor2:
                item = Resources.Load<Item>("Armor2");
                player[Select].itesms[1] = item;
                break;
            default:
                break;
        }

        switch (player[Select].pants)
        {
            case Pants.None:
                break;
            case Pants.Pants1:
                Item item;
                item = Resources.Load<Item>("Pants");
                player[Select].itesms[2] = item;
                break;
            case Pants.Pants2:
                item = Resources.Load<Item>("Pants2");
                player[Select].itesms[2] = item;
                break;
            default:
                break;
        }
    }

    public void ItemSet(int _nSelect, int _nNum)
    {
        //for (int i = 0; i<player.Length; i++)
        //{
        if (_nSelect > 2 || _nNum > 3)
        {
            Debug.Log("Á¦ÇÑÀ» ¹þ¾î³².");
            return;
        }

        switch (_nNum)
        {
            case 0:
                if (player[_nSelect].itesms[0] != null)
                {
                    switch (player[_nSelect].itesms[0].itemName) // Çï¸ä
                    {
                        case "Helmet":
                            player[_nSelect].helmet = Helmet.Helmet1;
                            player[_nSelect].def += 5;
                            break;
                        case "Helmet2":
                            player[_nSelect].helmet = Helmet.Helmet2;
                            player[_nSelect].def += 7;
                            break;
                    }
                }
                break;
            case 1:
                if (player[_nSelect].itesms[1] != null)
                {
                    switch (player[_nSelect].itesms[1].itemName) // Çï¸ä
                    {
                        case "Armor":
                            player[_nSelect].armor = Armor.Armor1;
                            player[_nSelect].def += 5;
                            break;
                        case "Armor2":
                            player[_nSelect].armor = Armor.Armor2;
                            player[_nSelect].def += 7;
                            break;
                    }
                }
                break;
            case 2:
                if (player[_nSelect].itesms[2] != null)
                {
                    switch (player[_nSelect].itesms[2].itemName) // Çï¸ä
                    {
                        case "Pants":
                            player[_nSelect].pants = Pants.Pants1;
                            player[_nSelect].def += 5;
                            break;
                        case "Pants2":
                            player[_nSelect].pants = Pants.Pants2;
                            player[_nSelect].def += 7;
                            break;
                    }
                }
                break;
            case 3:
                if (player[_nSelect].itesms[3] != null)
                {
                    switch (player[_nSelect].itesms[3].itemName) // Çï¸ä
                    {
                        case "Sword":
                            player[_nSelect].sword = Sword.Sword1;
                            player[_nSelect].str += 5;
                            break;
                        case "Sword2":
                            player[_nSelect].sword = Sword.Sword2;
                            player[_nSelect].str += 7;
                            break;
                    }
                }
                break;
        }
        //}
    }

    public void HpResult(int nSelect, int nResultHp)
    {
        player[nSelect].currenthp += nResultHp;
        if (player[nSelect].currenthp >= player[nSelect].maxHp)
        {
            player[nSelect].currenthp = player[nSelect].maxHp;
        }

        if (player[nSelect].currenthp <= 0)
        {
            player[nSelect].currenthp = 0;
        }
    }

    public void MpResult(int nSelect, int nResultMp)
    {
        player[nSelect].currentMp += nResultMp;
        if (player[nSelect].currentMp >= player[nSelect].maxMp)
        {
            player[nSelect].currentMp = player[nSelect].maxMp;
        }
    }

    public void LevelUp(int nNum)
    {
        if (player[nNum].maxExp <= player[nNum].currentExp && nNum == 0)
        {
            player[nNum].level++;
            player[nNum].maxHp += 14 * (player[nNum].level - 1);
            player[nNum].currenthp += 14 * (player[nNum].level - 1);
            player[nNum].maxMp += 10 * (player[nNum].level - 1);
            player[nNum].currentMp += 10 * (player[nNum].level - 1);
            player[nNum].str += 3 * (player[nNum].level - 1);
            player[nNum].def += 1 * (player[nNum].level - 1);
            player[nNum].Evaison += 1 * (player[nNum].level - 1);
            player[nNum].currentExp = player[nNum].currentExp - player[nNum].maxExp;
            player[nNum].maxExp = player[nNum].maxExp * 2;
        }
        if (player[nNum].maxExp <= player[nNum].currentExp && nNum == 1)
        {
            player[nNum].level++;
            player[nNum].maxHp += 20 * (player[nNum].level - 1);
            player[nNum].currenthp += 20 * (player[nNum].level - 1);
            player[nNum].maxMp += 10 * (player[nNum].level - 1);
            player[nNum].currentMp += 10 * (player[nNum].level - 1);
            player[nNum].str += 2 * (player[nNum].level - 1);
            player[nNum].def += 2 * (player[nNum].level - 1);
            player[nNum].Evaison += 1 * (player[nNum].level - 1);
            player[nNum].currentExp = player[nNum].currentExp - player[nNum].maxExp;
            player[nNum].maxExp = player[nNum].maxExp * 2;
        }
    }

    void CreateCharcter(int nNum, int nLevel, int nHp, int nMp, int nStr, int nDef, int nEvaison, int nExp)
    {
        player[nNum].level = nLevel;
        player[nNum].maxHp = nHp;
        player[nNum].currenthp = nHp;
        player[nNum].maxMp = nMp;
        player[nNum].currentMp = nMp;
        player[nNum].str = nStr;
        player[nNum].def = nDef;
        player[nNum].Evaison = nEvaison;
        player[nNum].maxExp = nExp;
        player[nNum].currentExp = 0;
    }
}
