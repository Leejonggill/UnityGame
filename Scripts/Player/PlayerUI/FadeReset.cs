using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeReset : MonoBehaviour
{
    [SerializeField] Image resetFade;

    private void OnEnable()
    {
        Color color = resetFade.color;
        color.a = 0;
        resetFade.color = color;         
    }
}
