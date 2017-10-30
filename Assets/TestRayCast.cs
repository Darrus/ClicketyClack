using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRayCast : MonoBehaviour
{
    public LayerMask mask;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            AvoidMove();
        }
    }
    void AvoidMove()
    {
        // Rayの作成
        //Ray ray = new Ray(transform.position, transform.forward);
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Rayが衝突したコライダーの情報を得る
        RaycastHit hit;

        // Rayが衝突したかどうか
        if (Physics.Raycast(mouseRay, out hit, 100.0f, mask))
        {
            Debug.Log("Hit");

            //// Examples
            //// 衝突したオブジェクトの色を赤に変える
            //hit.collider.GetComponent<MeshRenderer>().material.color = Color.red;
            //// 衝突したオブジェクトを消す
            //Destroy(hit.collider.gameObject);
            //// Rayの衝突地点に、このスクリプトがアタッチされているオブジェクトを移動させる
            //this.transform.position = hit.point;
            //// Rayの原点から衝突地点までの距離を得る
            //float dis = hit.distance;
            //// 衝突したオブジェクトのコライダーを非アクティブにする
            //hit.collider.enabled = false;
        }

        // Rayの可視化
        Debug.DrawRay(mouseRay.origin, mouseRay.direction * 10f, Color.red, 3.0f);
    }
}

