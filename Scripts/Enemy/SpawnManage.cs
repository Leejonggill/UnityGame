using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManage : MonoBehaviour
{
    [SerializeField] Transform[] spawnTrans;

    public GameObject golem;
    public GameObject golem2;
    GameObject[] golemObj;

    void OnEnable()
    {
        golemObj = new GameObject[3];
        for (int i = 0; i < spawnTrans.Length; i++)
        {
            int ran = Random.Range(0, 2);
            if (ran == 1)
            {
                golemObj[i] = Instantiate(golem, spawnTrans[i].position-new Vector3(0,0.1f,0), spawnTrans[i].rotation);
                golemObj[i].transform.parent = transform;
            }
            else
            {
                golemObj[i] = Instantiate(golem2, spawnTrans[i].position - new Vector3(0, 0.1f, 0), spawnTrans[i].rotation);
                golemObj[i].transform.parent = transform;
            }
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < golemObj.Length; i++)
        {
            Destroy(golemObj[i].transform.gameObject);
        }
    }
}
