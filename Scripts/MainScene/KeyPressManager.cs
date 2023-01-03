using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPressManager : MonoBehaviour
{
    [SerializeField] GameObject opstion;
    [SerializeField] Image FadeIn;
    public Text[] menuText = new Text[3];

    Color fadeColor;

    int KeyNumber;
    bool isfrist = false;

    float timer = 0.0f;

    void Start()
    {
        KeyNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isfrist == false)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                SoundsManager.Instance.OnStrartKeySound();
                KeyNumber++;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                SoundsManager.Instance.OnStrartKeySound();
                KeyNumber--;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (KeyNumber != 1)
                {
                    SoundsManager.Instance.OnStrartKeySound();
                    StartCoroutine(_FadeIn());
                    isfrist = true;
                }
                else
                {
                    SelectScene();
                    gameObject.SetActive(false);
                }
            }
        }
        SetAlpha();
    }

    IEnumerator _FadeIn()
    {
        yield return StartCoroutine(FadeAlpha());
        SelectScene();
    }

    IEnumerator FadeAlpha()
    {
        while (fadeColor.a < 1)
        {
            timer += Time.timeScale;
            fadeColor.a = 0.0007f * timer;
            FadeIn.color = fadeColor;
            yield return null;
        }
    }

    void SetAlpha()
    {
        KeyNumber = Mathf.Clamp(KeyNumber, 0, 2);
        SelectMenu();
    }


    void SelectMenu()
    {
        KeyNumber = Mathf.Clamp(KeyNumber, 0, 2);
        Color selectColor;

        for (int i = 0; i < menuText.Length; i++)
        {
            selectColor = menuText[i].color;
            if (i == KeyNumber)
            {
                selectColor.a = 1.0f;
                menuText[i].color = selectColor;
            }
            else if (i != KeyNumber)
            {
                selectColor.a = 0.5f;
                menuText[i].color = selectColor;
            }
        }
    }

    void SelectScene()
    {
        switch (KeyNumber)
        {
            case 0:
                LoadingScene.LoadScene("StartVilage");
                break;
            case 1:
                opstion.SetActive(true);
                break;
            case 2:
                Application.Quit();
                break;
        }
    }
}
