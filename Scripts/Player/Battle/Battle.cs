using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Battle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] selectText;
    //[SerializeField] Text[] selectText;
    [SerializeField] Image[] playerSelectImage;
    [SerializeField] Image selectImage;
    [SerializeField] GameObject[] gameObjectsParent;

    bool isSelect = false;
    int select = 4;
    //int playerNum;

    public static int playerTurn = 1;

    private void OnEnable()
    {
        select = 4;
        //if (isSelect)
        //{
        //    if (playerNum == 2)
        //    {
        //        playerNum = 1;
        //    }
        //    else if (playerNum == 1)
        //    {
        //        playerNum = 2;
        //    }
        //}
        if (playerTurn>=3)
        {
            isSelect = true;
        }

        if (!isSelect)
        {
            playerTurn = 1;
            //playerTurn = PlayerStateManager.playerSelect;

            //playerNum = PlayerStateManager.playerSelect;
            isSelect = true;
        }
                
        //playerNum = PlayerStateManager.playerSelect;

        StartCoroutine(StartColor());
    }

    private void OnDisable()
    {
        RemoveText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            select = 0;
            SelectText();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            select = 1;
            SelectText();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            select = 2;
            SelectText();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            select = 3;
            SelectText();
        }

        if (Input.GetKeyDown(KeyCode.Space) && select != 4)
        {
            Select(select);
        }
    }

    void Select(int Select)
    {
        SoundsManager.Instance.OnSelectKeySound();
        switch (Select)
        {
            case 0:
                gameObjectsParent[Select].SetActive(true);
                break;
            case 1:
                gameObjectsParent[Select].SetActive(true);
                break;
            case 2:
                gameObjectsParent[Select].SetActive(true);
                break;
            case 3:
                gameObjectsParent[Select].SetActive(true);
                break;
        }
    }

    IEnumerator StartColor()
    {
        //yield return new WaitForSeconds(0.0f);
        //SetColor();
        float timer = 0;
        if (playerTurn == 1)
        {
            //Color color = new Color(186, 0, 0);
            timer += 0.00005f;
            int frame = 0;
            while (frame < 150/*playerSelectImage[0].color == color*/)
            {
                frame++;
                Color color = new Color(186, 0, 0);
                playerSelectImage[0].color = Color.Lerp(playerSelectImage[0].color, color, timer);
                selectImage.color = Color.Lerp(playerSelectImage[0].color, color, timer);
                yield return null;
            }
           
        }
        else if (playerTurn == 2)
        {
            timer += 0.00005f;
            int frame = 0;
            while (frame < 150/*playerSelectImage[0].color == color*/)
            {
                frame++;
                Color color = new Color(255, 220, 30);
                playerSelectImage[1].color = Color.Lerp(playerSelectImage[1].color, color, timer);
                selectImage.color = Color.Lerp(playerSelectImage[1].color, color, timer);
                yield return null;
            }
        }

        Debug.Log("¿Ï");
        yield return null;
    }

    void SelectText()
    {
        SoundsManager.Instance.OnSelectKeySound();
        Color tempColor;
        for (int i = 0; i < selectText.Length; i++)
        {
            tempColor = selectText[i].color;
            if (select == i)
            {
                tempColor.a = 1.0f;
                selectText[i].color = tempColor;
            }
            else
            {
                tempColor.a = 0.5f;
                selectText[i].color = tempColor;
            }
        }
    }

    void SetColor()
    {
        Color color;
        switch (playerTurn)
        {
            case 1:
                color = new Color(186, 0, 0);
                playerSelectImage[0].color = color;
                selectImage.color = color;
                break;
            case 2:
                color = new Color(255, 220, 30);
                playerSelectImage[1].color = color;
                selectImage.color = color;
                break;
        }
    }

    void RemoveColor()
    {
        Color color;
        color = selectImage.color;
        color = new Color(0, 0, 0);
        selectImage.color = color;

        for (int i = 0; i < playerSelectImage.Length; i++)
        {
            playerSelectImage[i].color = color;
        }
    }

    void RemoveText()
    {
        for (int i = 0; i < selectText.Length; i++)
        {
            Color color;
            color = selectText[i].color;
            color.a = 0.5f;
            selectText[i].color = color;
        }
    }
}
