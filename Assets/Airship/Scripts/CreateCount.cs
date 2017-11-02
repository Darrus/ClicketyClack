using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCount : MonoBehaviour
{
    public int nowCnt = 0;
    public int MaxCnt;

    public GameObject CountPrefab;
    public GameObject CntObjPos;
    public static bool _isTri = false;

    void Update ()
    {
        if (_isTri)
        {
            nowCnt++;
            var obj = GameObject.Instantiate(CountPrefab, CntObjPos.transform.position, Quaternion.identity);
            obj.GetComponent<CreateCount>().MaxCnt = MaxCnt;
            obj.GetComponent<CreateCount>().nowCnt = nowCnt;

            _isTri = false;
        }
        
        if (nowCnt == MaxCnt)
        {
            Destroy(this.gameObject);
        }
    } 
}
    