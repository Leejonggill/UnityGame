using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemText : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text infoText;

    public void SetUpToolTip(string name)
    {
        nameText.text = name;

        switch(name)
        {
            case "HpPostion":
                infoText.text = "ü������ Hp+50";
                break;
            case "MpPostion":
                infoText.text = "�������� Hp+50";
                break;
            case "Helmet":
                infoText.text = "���� �Ӹ��� Def :5";
                break;
            case "Helmet2":
                infoText.text = "����� �Ӹ��� Def+7";
                break;
            case "Armor":
                infoText.text = "���� ������ Def+5";
                break;
            case "Armor2":
                infoText.text = "����� ������  Def+7";
                break;
            case "Pants":
                infoText.text = "���� �ٸ��� Def+5";
                break;
            case "Pants2":
                infoText.text = "����� ������ Def+7";
                break;
            case "Sword":
                infoText.text = "���� �� Str+5";
                break;
            case "Sword2":
                infoText.text = "����� �� Str+7";
                break;
        }
    }
}
