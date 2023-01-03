using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    [SerializeField] Transform enemy;
    [SerializeField] Transform player;

    // Update is called once per frame
    void Update()
    {
        //Vector3 dis = enemy.transform.position - player.transform.position;
        transform.position = Vector3.Lerp(player.position, enemy.transform.position+new Vector3(0,4.5f,0), 0.8f);
        transform.Rotate(new Vector3(0, 0, 60) * Time.unscaledDeltaTime);
    }
}
