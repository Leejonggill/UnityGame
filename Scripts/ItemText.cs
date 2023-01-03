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
                infoText.text = "체력포션 Hp+50";
                break;
            case "MpPostion":
                infoText.text = "마나포션 Hp+50";
                break;
            case "Helmet":
                infoText.text = "낡은 머리방어구 Def :5";
                break;
            case "Helmet2":
                infoText.text = "평범한 머리방어구 Def+7";
                break;
            case "Armor":
                infoText.text = "낡은 가슴방어구 Def+5";
                break;
            case "Armor2":
                infoText.text = "평범한 가슴방어구  Def+7";
                break;
            case "Pants":
                infoText.text = "낡은 다리방어구 Def+5";
                break;
            case "Pants2":
                infoText.text = "평범한 가슴방어구 Def+7";
                break;
            case "Sword":
                infoText.text = "낡은 검 Str+5";
                break;
            case "Sword2":
                infoText.text = "평범한 검 Str+7";
                break;
        }
    }
}
