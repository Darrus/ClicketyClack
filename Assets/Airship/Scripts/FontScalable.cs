/**
 * @file : FontScalable.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief : フォントスケールの調整
 */

using UnityEngine;
using System.Collections;

/**
 * @brief : フォントスケールの調整をするクラス
 */
[ExecuteInEditMode]     ///< 編集中も処理を実行可能
public class FontScalable : MonoBehaviour
{
    public float fontScale = 0.5f;          ///<  フォントサイズ
    private TextMesh textMesh;          ///< テキストメッシュ

/**
*   @brief   デバッグ時に１フレームだけ呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    void Start()
    {
        // テキストメッシュ
        textMesh = GetComponent<TextMesh>();
    }

/**
*   @brief   毎フレーム呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    void Update()
    {
        // フォントスケール分を乗算
        Vector3 defaultScale = new Vector3(1, 1, 1) * fontScale;

        // テキストメッシュのフォントサイズを取得
        int fontSize = textMesh.fontSize;

        // 初期値が０だった場合１２を代入、指定されていればそれを代入
        fontSize = (fontSize == 0) ? 12 : fontSize;

        // 128のスケールを固定しメモリ消費を抑える
        float scale = 0.1f * 128 / fontSize;

        // 拡大
        transform.localScale = defaultScale * scale;
    }
}