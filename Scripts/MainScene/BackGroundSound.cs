using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundSound : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;
    [SerializeField] Slider slider;
    [SerializeField] GameObject opstion;
    [SerializeField] GameObject keyManager;

    private void Start()
    {
        StartCoroutine(enumerator());
    }

    IEnumerator enumerator()
    {
        yield return new WaitForSeconds(0.1f);
        SoundsManager.Instance.slider = slider;
        SoundsManager.Instance.SetBackSound(audioClip);
        slider.value = SoundsManager.Instance.voluem;
        slider.onValueChanged.AddListener(delegate { SoundsManager.Instance.OnSoundVolume(); });
    }

    public void OnRemoveButton()
    {
        if (keyManager != null)
        {
            keyManager.SetActive(true);
        }
        opstion.SetActive(false);
    }
}
