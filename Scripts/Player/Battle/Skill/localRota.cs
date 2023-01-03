using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class localRota : MonoBehaviour
{
    [SerializeField] GameObject effectRotation;
    void Start()
    {
        transform.localRotation = Quaternion.Euler(-1, -113, -0.017f);
        effectRotation.transform.localRotation = Quaternion.Euler(-1.65f, -94, 163);
        //effectRotation.transform.localRotation = Quaternion.Euler(-179, 83, -17);
        //effectRotation.transform.localRotation = Quaternion.Euler(-179, 83, -17);
        //effectRotation.transform.localRotation = Quaternion.Euler(2, 88 , -37);
        //effectRotation.transform.localRotation = Quaternion.Euler(355,-122, 179);
    }
    //-179,83.7,-17.2
}
