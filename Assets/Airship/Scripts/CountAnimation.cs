/**
 * @file : CountAnimation.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief : カウントUI 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * @brief : カウントのアニメーションを管理するクラス
 */
public class CountAnimation : CreateCount
{
    private float alpha = 1.0f;             ///< テキストのアルファ

/**
*   @brief   デバッグ時に１フレームだけ呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    private void Start()
    {
        // 正面をカメラの方向へ
        transform.LookAt(Camera.main.transform.position);
    }

/**
*   @brief   毎フレーム呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    void Update()
    {
        // テキストの変更
        ChangeText();

        // アニメーション
        MoveAnimation();

        // フェードアウト
        FadeOutText();
    }

/**
*   @brief   テキストの変更をする関数
*   @param  nothing
*   @return nothing 
*/
    void ChangeText()
    {
        // テキストメッシュの取得
        TextMesh text = GetComponent<TextMesh>();

        // 表示
        text.text = nowCnt + " / " + MaxCnt;
    }

/**
*   @brief   テキストの移動アニメーション関数
*   @param  nothing
*   @return nothing 
*/
    void MoveAnimation()
    {
        // カメラのほうへ向く
        transform.rotation = Camera.main.transform.rotation;

        // 上昇
        transform.position += new Vector3(0, 0.1f, 0) * Time.deltaTime;
    }

/**
*   @brief   フェードアウトをする関数
*   @param  nothing
*   @return nothing 
*/
    void FadeOutText()
    {

        // テキストのアルファ値を減算
        alpha -= 1f * Time.deltaTime;
       GetComponent<TextMesh>().color = new Color(40, 219, 64, alpha);
        
        // 透明になったら
        if (alpha < 0)
        {
            // 3DTextを削除
            Destroy(this.gameObject);
        }
    }
}
