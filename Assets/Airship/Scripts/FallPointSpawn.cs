using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPointSpawn : MonoBehaviour
{

    public GameObject fallpointPrefab;

    private GameObject activeFallPoint = null;
    private Vector3 FallPos;
    private float createTime;
    private int rate;

    private void Start()
    {
        createTime = 3.0f;
        rate = Random.Range(1, 5);
    }

    void Update()
    {
        if (AirShipPatrol.CanShot && LevelManager.Singleton != null && GameBoard.Singleton != null)
        {
            if(LevelManager.Instance.Play && LevelManager.Instance.MoveOut && !LevelManager.Instance.ReachStation && GameBoard.Instance.TheTrainLife.Life > 0)
                CreateFallpoint();
        }
    }

    private void CreateFallpoint()
    {
        createTime -= Time.deltaTime;

        if (createTime < 0)
        {
            rate = Random.Range(1, 5);
            Debug.Log(rate);

            // 電車が次に目指すレール上に落下地点をスポーン
            // 20% HIT

            if (rate > 1)
            {
                FallPos = transform.position + RandomPosition();
            }
            else
            {
                FallPos = transform.position + transform.forward / 2f;
            }

            FallPos.y += 0.01f;
            activeFallPoint = Instantiate((GameObject)fallpointPrefab, FallPos, Quaternion.identity);

            createTime = CreateTime();
        }
    }

    public Vector3 RandomPosition()
    {
        float levelSize = 0.5f;
        return new Vector3(Random.Range(-levelSize, levelSize), transform.position.y + 0.5f, Random.Range(-levelSize, levelSize));
    }

    private float CreateTime()
    {
        return Random.Range(5.0f, 10.0f);
    }
}
