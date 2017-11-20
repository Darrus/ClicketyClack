/**
 * @file : PageControll.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief : 看板のページ制御
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * @brief : 看板のページを管理するクラス
 */
public class PageControll : MonoBehaviour
{
    static public int Num;              ///< ページ数
    public GameObject[] Page;      ///< ページGIF

/**
 *   @brief   デバッグ時に１フレームだけ呼び出される関数
 *   @param  nothing
 *   @return nothing 
*/
    void Start ()
    {
        // 初期化
        Num = 0;

        // ページを表示している間はほかのページは非表示
        for (int i = 1; i < Page.Length; i++)
        {
            Page[i].SetActive(false);
        }
        Page[Num].SetActive(true);
    }

/**
 *   @brief   右矢印がクリックされたら呼ばれる関数
 *   @param  nothing
 *   @return nothing 
*/
    public void RightClick()
    {
        // Next Page
        Num++;
        Page[Num - 1].SetActive(false);
        if (Num >= Page.Length)
            Num = 0;
        Page[Num].SetActive(true);
    }

/**
 *   @brief   左矢印がクリックされたら呼ばれる関数
 *   @param  nothing
 *   @return nothing 
*/
    public void LeftClick()
    {
        // Back Page
        Num--;
        Page[Num + 1].SetActive(false);
        if (Num <= -1)
            Num = 2;
        Page[Num].SetActive(true);
    }
}
