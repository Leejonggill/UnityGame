using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackroundMove : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public GameObject setText;

    float offset;
    float speed = 0.1f;
    float timer = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(SetActive());
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (offset < 0.18f && timer>0.5f)
        {
            offset += Time.deltaTime * speed;
            spriteRenderer.material.mainTextureOffset = new Vector2(offset, 0);
        }
    }

    IEnumerator SetActive()
    {
        yield return new WaitForSeconds(2.5f);
        Debug.Log("¿Ï·á");
        setText.SetActive(true);
    }
}
