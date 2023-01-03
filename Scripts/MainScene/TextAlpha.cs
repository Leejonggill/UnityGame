using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAlpha : MonoBehaviour
{
    [SerializeField] Text startText;
    [SerializeField] Text Opstion;
    [SerializeField] Text exitText;
    [SerializeField] Text mainText;
    public GameObject KeyPressManager;

    // * 하나로 세개 쓸수잇음.
    Color startColor;
    Color exitColor;
    Color mainColor;
    Color OpstionColor;

    float time1;

    private void Start()
    {
        StartCoroutine(SetButtonPress());
    }

    void Update()
    {
        if (startColor.a < 1.0f)
        {
            time1 += Time.deltaTime;
            startColorSet();
            mainColorSet();
            OpstionSet();
        }
        if(exitColor.a<0.5f)
        {
            exitColorSet();
        }
    }

    void startColorSet()
    {
        startColor = startText.color;
        startColor.a = 0.5f * time1;
        startText.color = startColor;
    }

    void exitColorSet()
    {
        exitColor = startText.color;
        exitColor.a = 0.25f* time1;
        exitText.color = exitColor;
    }

    void OpstionSet()
    {
        OpstionColor = Opstion.color;
        OpstionColor.a = 0.25f * time1;
        Opstion.color = OpstionColor;
    }

    void mainColorSet()
    {
        mainColor = mainText.color;
        mainColor.a = 0.5f * time1;
        mainText.color = mainColor;
    }

    IEnumerator SetButtonPress()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
        KeyPressManager.SetActive(true);
    }
}
