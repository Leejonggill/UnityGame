using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetItemResult : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Text text2;
    [SerializeField] Image backImage;

    bool isFirst = false;

    void Start()
    {
        if (!isFirst)
            isFirst = true;
    }

    private void OnEnable()
    {
        SoundsManager.Instance.OnSwordSound(0);
        Color color;
        color = text.color;
        color.a = 1;
        text.color = color;

        color = text2.color;
        color.a = 1;
        text2.color = color;

        color = backImage.color;
        color.a = 1;
        backImage.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        Color color;
        color = text.color;
        color.a -= Time.deltaTime * 0.3f;
        text.color = color;

        color = text2.color;
        color.a -= Time.deltaTime * 0.3f;
        text2.color = color;

        color = backImage.color;
        color.a -= Time.deltaTime * 0.3f;
        backImage.color = color;

        if(color.a<0.03f)
        {
            gameObject.SetActive(false);
        }
    }
}
