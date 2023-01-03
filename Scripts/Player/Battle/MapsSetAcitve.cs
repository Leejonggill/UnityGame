using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapsSetAcitve : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    private void OnEnable()
    {
        StartCoroutine(enumerator());
    }

    IEnumerator enumerator()
    {
        yield return null;
        for(int i=0; i<objects.Length;i++)
        {
            objects[i].SetActive(true);
        }
    }
}
