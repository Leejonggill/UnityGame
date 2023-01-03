using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    void Start()
    {
        SoundsManager.Instance.OnSkillSound(0);
        Destroy(gameObject, 4.0f);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 3);
    }
}
