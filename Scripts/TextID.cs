using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextID : MonoBehaviour
{
    [SerializeField] Text text;



    public void OnClick()
    {
        StartCoroutine(Logic());
    }


    IEnumerator Logic()
    {
        yield return new WaitForSeconds(1.5f);
        text.text = FireBaseManager.userID2;
    }
}
