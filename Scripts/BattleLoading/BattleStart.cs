using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStart : MonoBehaviour
{
    bool isSet = false;
    bool isStart = false;
    [SerializeField] Image[] images;
    [SerializeField] Text text;
    [SerializeField] GameObject battleManager;

    private void OnEnable()
    {
        isSet = false;
        isStart = false;
        text.transform.localPosition = new Vector3(164, 63, 0);
        Color color;

        for (int i = 0; i < images.Length; i++)
        {
            color = images[i].color;
            color.a = 1.0f;
            images[i].color = color;
        }

        color = text.color;
        color.a = 1.0f;
        text.color = color;

        StartCoroutine(SetText());
        SoundsManager.Instance.OnSwordSound(0);
        StartCoroutine(ActiveTrueManager());
    }

    //private void Start()
    //{
    //    StartCoroutine(SetText());
    //    StartCoroutine(ActiveTrueManager());
    //}

    IEnumerator ActiveTrueManager()
    {
        yield return new WaitForSeconds(2.5f);
        battleManager.SetActive(true);
    }

    IEnumerator FadeOut()
    {
        Color color;
        float timer = 0;
        if (images[1].color.a > 0)
        {
            timer += Time.deltaTime;
            for (int i = 0; i < images.Length; i++)
            {
                color = images[i].color;
                color.a -= timer * 1.5f;
                images[i].color = color;
            }
            color = text.color;
            color.a -= timer * 1.5f;
            text.color = color;
        }

        yield return null;
    }

    IEnumerator SetText()
    {
        if (transform.localPosition.x >= 5)
        {
            yield return null;
            transform.localPosition += new Vector3(-0.8f, 0, 0);
            StartCoroutine(SetText());
            isSet = true;
        }
        else if (isSet)
        {
            transform.localPosition += new Vector3(-0.1f, 0, 0);
            yield return new WaitForSeconds(0.25f);
            isSet = false;
            isStart = true;
        }
        if (isStart && transform.localPosition.x >= -280)
        {
            yield return null;
            transform.localPosition += new Vector3(-0.8f, 0, 0);
            StartCoroutine(FadeOut());
            StartCoroutine(SetText());
        }
    }


}
