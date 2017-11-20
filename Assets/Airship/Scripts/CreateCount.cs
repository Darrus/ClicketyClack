/**
 * @file : CreateCount.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief : カウントUIの生成
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief : カウントUIを管理するクラス
 */
public class CreateCount : MonoBehaviour
{
    public static bool _isTri = false;            ///< カウント生成を管理

    public int nowCnt = 0;                            ///< 現在のカウント
    public int MaxCnt;                                 ///< 最大値のカウント
    public GameObject CountPrefab;          ///< カウントのプレハブ
    public GameObject CntObjPos;             ///< 出現のポジション

/**
*   @brief   毎フレーム呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    void Update ()
    {
        // 生成命令
        if (_isTri)
        {
            // 現在のカウントを＋１
            nowCnt++;

            // プレハブから3DTextを生成
            var obj = GameObject.Instantiate(CountPrefab, CntObjPos.transform.position, Quaternion.identity);

            // 最大値と現在値を更新
            obj.GetComponent<CreateCount>().MaxCnt = MaxCnt;
            obj.GetComponent<CreateCount>().nowCnt = nowCnt;

            // 生成処理終了
            _isTri = false;
        }
        
        // 最大値までカウントしたら
        if (nowCnt == MaxCnt)
        {
            // 削除
            Destroy(this.gameObject);
        }
    } 
}
    