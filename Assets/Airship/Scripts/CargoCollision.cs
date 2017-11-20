/**
 * @file : CargoCollision.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief : 貨物のあたり判定
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

/**
 * @brief : 貨物のあたり判定を管理するクラス
 */
public class CargoCollision : MonoBehaviour
{
    public static bool _isArrow = false;            ///< 矢印の制御
    public GameObject obj_coal_pile;              ///< 石炭

    private  bool _isCoal = false;                      ///< 石炭が入れたかどうかの確認
    private int coalCount;                                 ///< 入れた石炭の個数

/**
*   @brief   デバッグ時に１フレームだけ呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    private void Start()
    {
        // 石炭の数値を初期化
        coalCount = 0;

        // 貨物を運べないように
        GetComponent<HandDraggable>().enabled = false;
    }

/**
*   @brief   毎フレーム呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    private void Update()
    {
        // 石炭を入れ終わったら
        if (_isCoal)
        {
            // テキストの変更
            TextControll.textNum = 1;

            // 矢印を次の座標へ
            ArrowControll.coalComp = true;
            
            _isCoal = false;
        }
    }

/**
*   @brief   衝突したとき呼び出される関数
*   @param  衝突したオブジェクト
*   @return nothing 
*/
    private void OnCollisionEnter(Collision collision)
    {
        // 石炭と当たったら
        if (collision.gameObject.tag == "Coal")
        {
            // カウントUIの表示
            CreateCount._isTri = true;

            // 石炭を削除
            Destroy(collision.gameObject);


            CoalInCargo();
        }

        // 電車の停止
        if (collision.gameObject.tag == "DragPoint")
        {
            // オブジェクトの削除
            Destroy(collision.gameObject);
            
            // テキスト文の制御
            TextControll.textNum = 3;
            
            // 矢印の制御
            _isArrow = true;

            // 止まった
            RockAreaManager._isStop = true;
            LevelManager.Instance.Play = false;
        }
    }

    // 石炭のカウント
    private void CoalInCargo()
    {
        // 石炭のカウント
        coalCount++;

        // 貨物の中の石炭が４つ以下だったら
        if (coalCount <= 4)
        {
            // 石炭の山を少し上に
            obj_coal_pile.transform.position += new Vector3(0, 0.005f, 0);
        }
        else
        {
            // 貨物が動かせるトリガー
            _isCoal = true;

            // アタッチされているスクリプトを起動
            GetComponent<HandDraggable>().enabled = true;
        }
    }
}


