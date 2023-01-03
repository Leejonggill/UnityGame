using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alpha : MonoBehaviour
{
    [SerializeField] Image[] images = new Image[2];
    [SerializeField] Text[] texts = new Text[2];
    [SerializeField] GameObject backGround;

    Color colors;
    bool isfrist = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown&&isfrist==false)
        {
            isfrist = true;
            SoundsManager.Instance.OnStrartKeySound();
            StartCoroutine(SetColor());
            backGround.SetActive(true);
        }
    }

    IEnumerator SetColor()
    {
        colors = images[0].color;
        float timer = 0.0f;

        while (colors.a > 0)
        {
            timer += Time.timeScale / 4;
            for (int i = 0; i < images.Length; i++)
            {
                colors = images[i].color;
                colors.a -= 0.00007f * timer;
                images[i].color = colors;

                colors = texts[i].color;
                colors.a -= 0.00007f * timer;
                texts[i].color = colors;
            }
            yield return null;
        }
    }
}
