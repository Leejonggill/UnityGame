using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject ShopCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            ShopCanvas.SetActive(true);
            Debug.Log("1");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            ShopCanvas.SetActive(false);
            Debug.Log("2");
        }
    }

    public void OnFalseButton()
    {
        ShopCanvas.SetActive(false);
    }
}
