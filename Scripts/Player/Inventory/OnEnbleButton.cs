using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnbleButton : MonoBehaviour
{
    [SerializeField] GameObject buttonMager;

    //private void OnEnable()
    //{
    //    buttonMager.SetActive(false);
    //}

    private void OnEnable()
    {
        buttonMager.SetActive(false);
    }
}
