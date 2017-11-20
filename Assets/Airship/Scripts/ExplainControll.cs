/**
 * @file : ExplainControll.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief :説明文
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief : 説明文を管理するクラス
 */
public class ExplainControll : MonoBehaviour
{
    [Multiline]
    public string[] Explane;            ///< 説明文の配列
    private TextMesh text;           ///< テキストメッシュ

/**
 *   @brief   デバッグ時に１フレームだけ呼び出される関数
 *   @param  nothing
 *   @return nothing 
*/
    private void Start()
    {
        // テキストメッシュの取得
        text = GetComponent<TextMesh>();
    }

/**
*   @brief   毎フレーム呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    private void Update()
    {
        // Stringの配列をテキストに代入
        switch (PageControll.Num)
        {
            case 0:
                text.text = Explane[0];
                break;

            case 1:
                text.text = Explane[1];
                break;

            case 2:
                text.text = Explane[2];
                break;
        }
    }
}
