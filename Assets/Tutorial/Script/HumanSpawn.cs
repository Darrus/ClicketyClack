/**
 * @file : HumanSpawn.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief : 盗賊の生成
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;

/**
 * @brief : Hololenzで盗賊を生成クラス
 */
public class HumanSpawn : MonoBehaviour, IInputClickHandler
{
    public GameObject[] human;             ///< 盗賊のプレハブを収納する配列

    private void Start()
    {
        // フォーカス
        InputManager.Instance.PushFallbackInputHandler(gameObject);
    }

/**
 *   @brief   デバッグ時に１フレームだけ呼び出される関数
 *   @param  InputClickedEventData
 *   @return nothing 
*/
    public void OnInputClicked(InputClickedEventData eventData)
    {
        // 盗賊の種類をランダムに
        int rand = Random.Range(1, 3);

        // カメラの正面
        Vector3 CameraForward = Camera.main.transform.position + Camera.main.transform.forward;

        // 生成
        Instantiate(human[rand], CameraForward, new Quaternion());
    }
}