using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField] Image fadeIn;
    Color fadeInColor;
    float timer;
    bool isfirst;


    IEnumerator SetFadeIn()
    {
        while (fadeInColor.a <= 0.99f)
        {
            Debug.Log(1);
            timer += Time.deltaTime;
            fadeInColor = fadeIn.color;
            fadeInColor.a = 0.7f * timer;
            fadeIn.color = fadeInColor;
            yield return null; 
        }
        LoadingScene.LoadScene("Stage1");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            fadeIn.gameObject.SetActive(true);
            if (!isfirst)
            {
                isfirst = true;
                StartCoroutine(SetFadeIn());
            }
        }
    }
}
