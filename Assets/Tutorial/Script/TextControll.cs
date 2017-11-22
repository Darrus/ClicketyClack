/**
 * @file : TextControll.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief :チュートリアルの看板テキストの制御
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/**
 * @brief : 看板テキストを制御するクラス
 */
public class TextControll : MonoBehaviour
{
    [Multiline]
    public string[] English;                          ///< 文字を確保するための配列
    public static int textNum;                    ///< テキストを管理する番号
    private TextMesh text;                       ///< テキストメッシュのテキスト

/**
 *   @brief   デバッグ時に１フレームだけ呼び出される関数
 *   @param  nothing
 *   @return nothing 
*/
    private void Start()
    {
        // テキストメッシュの取得
        text = GetComponent<TextMesh>();

        // テキストの初期化
        textNum = 0;
    }

/**
*   @brief   毎フレーム呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    void Update()
    {
        // テキストの変更
        switch (textNum)
        {
            case 0:
                text.text = English[0];
                break;

            case 1:
                text.text = English[1];
                break;
                
            case 2:
                text.text = English[2];
                break;

            case 3:
                text.text = English[3];
                break;

            case 4:
                text.text = English[4];
                textNum = 4;
                break;
        }
    }


}

