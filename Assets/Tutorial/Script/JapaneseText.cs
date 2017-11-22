/**
 * @file : JapaneseText.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief : 日本語版テキスト
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief : 日本語のテキストを制御するクラス
 */
public class JapaneseText : MonoBehaviour
{
    [Multiline]
    public string[] japanese;           ///< 文字を確保するための配列
    private TextMesh text;            ///< テキストメッシュのテキスト

    private void Start()
    {
        // テキストメッシュの取得
        text = GetComponent<TextMesh>();

        // 初期テキスト
        TextControll.textNum = 0;
    }

/**
*   @brief   毎フレーム呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    private void Update()
    {
        // 番号ごとに文字が変わる
        switch (TextControll.textNum)
        {
            case 0:
                text.text = japanese[0];
                break;

            case 1:
                text.text = japanese[1];
                break;

            case 2:
                text.text = japanese[2];
                break;

            case 3:
                text.text = japanese[3];
                break;

            case 4:
                text.text = japanese[4];
                TextControll.textNum = 4;
                break;
        }
    }
}