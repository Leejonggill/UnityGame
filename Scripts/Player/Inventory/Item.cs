using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Item",menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        EQUIPMENTITEM,
        USEITEM,
    }

    public ItemType itemType;
    public Sprite itemImage;
    public string itemName;
}
