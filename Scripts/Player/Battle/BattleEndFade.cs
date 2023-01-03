using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleEndFade : MonoBehaviour
{
    [SerializeField] Image fadeIn;
    [SerializeField] Image fadeIn2;
    [SerializeField] Image backRotate;
    [SerializeField] GameObject[] Objects;
    [SerializeField] GameObject BattelMaps;
    Color color;
    float timer;

    private void OnEnable()
    {
        timer = 0;
        color = fadeIn.color;
        color.a = 0;
        fadeIn.color = color;
        fadeIn2.color = color;
        StartCoroutine(SetFadeIn());
    }

    private void OnDisable()
    {
        Color color = fadeIn.color;
        color.a = 0;
        fadeIn.color = color;

        color = fadeIn2.color;
        color.a = 0;
        fadeIn2.color = color;

        color = backRotate.color;
        color.a = 0;
        backRotate.color = color;


        //BattelMaps.SetActive(false);
        //for (int i=0; i< Objects.Length;i++)
        //{
        //    Objects[i].SetActive(true);
        //}
    }

    IEnumerator SetFadeIn()
    {
        yield return new WaitForSeconds(0.5f);
        while (color.a <= 0.99f)
        {
            timer += Time.deltaTime;
            color = fadeIn.color;
            color.a = 0.4f * timer;
            fadeIn.color = color;

            color = backRotate.color;
            color.a = 2.0f * timer;
            backRotate.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        color.a = 0;
        timer = 0;
        while (color.a <= 0.99f)
        {
            Debug.Log("FadeIn2");
            timer += Time.deltaTime;
            color = fadeIn2.color;
            color.a = 0.7f * timer;
            fadeIn2.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
        Objects[0].SetActive(true);
        BattelMaps.SetActive(false);
        gameObject.SetActive(false);
    }
}
