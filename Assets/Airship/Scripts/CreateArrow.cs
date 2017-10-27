using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateArrow : MonoBehaviour
{
    public GameObject ArrowPrefab;
    public static bool _isSleep = true;
    public GameObject target;
    public float Height = 0.1f; 

    private Rigidbody rigid;
    private int child;


    // Initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update
    void Update()
    {
        child = transform.childCount;

        // 出現
        if (_isSleep)
        {
            if (child == 0)
            {
                StartCoroutine("createArrow");

                _isSleep = false;
            }
        }
        if (ConnectCargo._isConnect)
        {
            Debug.Log("Success Connect");

            this.gameObject.GetComponent<CreateArrow>().enabled = false;
        }

    }

    private IEnumerator createArrow()
    {
        Vector3 ArrowPos = new Vector3(target.transform.position.x, target.transform.position.y + Height, target.transform.position.z);
        Instantiate<GameObject>(ArrowPrefab, ArrowPos, Quaternion.identity);
        yield break;
    }

}
