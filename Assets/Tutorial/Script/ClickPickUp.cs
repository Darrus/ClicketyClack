/**
 * @file : ClickPickUp.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief : オブジェクトをクリックで動かす
 */

using UnityEngine;
using HoloToolkit.Unity.InputModule;

/**
 * @brief : hololensの入力を管理するクラス
 */
public class ClickPickUp : MonoBehaviour, IInputClickHandler
{
    [SerializeField]
    private float speed = 1.0f;             ///< デフォルト座標に戻るときのスピード

    [SerializeField]                                  
    private float ObjectDistance;       ///< カメラとオブジェクトの距離

    private float step;                         ///< デフォルト座標に戻るときのスピード

    bool pickUp = false;                       ///< オブジェクトが持てるかどうか

/**
*   @brief   毎フレーム呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    private void Update()
    {
        //  クリックされたら
        if (pickUp)
        {
            // デフォルトアングルに戻す
            step = speed * Time.deltaTime;
            this.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), step);

            // カメラの目の前に
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * ObjectDistance;
        }
    }

/**
*   @brief   クリックされた時に呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    public void OnInputClicked(InputClickedEventData eventData)
    {
        // Rigidbodyの重力をオフに
        GetComponent<Rigidbody>().useGravity = !pickUp;
        pickUp = !pickUp;
    }
}


