using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMover : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    private GameObject _cube;
    private Vector3 targetPosition;
    private float moveTime;

    void Start()
    {
        _cube = this.gameObject;
        targetPosition = GetRandomPosition();
        moveTime = Random.Range(5, 10);
    }

    // Update
    void Update()
    {
        moveTime -= Time.deltaTime;

        if (moveTime > 0)
        {
            //正面に進む
            _cube.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        if (moveTime < 0)
        {
            moveTime = MoveTime();
            targetPosition = Vector3.forward;//GetRandomPosition();
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - _cube.transform.position);
            _cube.transform.rotation = Quaternion.Slerp(_cube.transform.rotation, targetRotation, Time.deltaTime / 2);
        }
    }

    public Vector3 GetRandomPosition()
    {
        float levelSize = 1f;
        return new Vector3(Random.Range(-levelSize, levelSize), 0, Random.Range(-levelSize, levelSize));
    }

    public float MoveTime()
    {
        return Random.Range(5, 10);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Bullet")
        {
            Debug.Log("HIT");
        }
    }
}
