/**
 * @file : ManipulationController.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief : Hololensの入力
 */

using UnityEngine;
using HoloToolkit.Unity.InputModule;

/**
 * @brief : オブジェクトを運ぶクラス
 */
public class ManipulationController : MonoBehaviour, IManipulationHandler
{
    [SerializeField]
    private float speed = 100.0f;               ///< デフォルトアングルに戻すときのスピード

    [SerializeField]
    private float ObjectDistance;            ///< プレイヤーがつかんだ時のオブジェクトとの距離
    
    private float step;                             ///< スピード



/**
 *   @brief   対象をつまみ外したときに呼ばれる関数
 *   @param  ManipulationEventData
 *   @return nothing 
*/
    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        // 重力をオフに
        GetComponent<Rigidbody>().useGravity = true;
        InputManager.Instance.PopModalInputHandler();
    }

/**
 *   @brief   途中トラッキングから外したときに呼ばれる関数
 *   @param  ManipulationEventData
 *   @return nothing 
*/
    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        // 重力をオフに
        GetComponent<Rigidbody>().useGravity = true;
        InputManager.Instance.PopModalInputHandler();
    }

/**
 *   @brief   対象をつまみだしたときに呼ばれる関数
 *   @param  ManipulationEventData
 *   @return nothing 
*/
    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        // 回転時間
        step = speed * Time.deltaTime;

        // 重力をオフにする
        GetComponent<Rigidbody>().useGravity = false;
        InputManager.Instance.PushModalInputHandler(this.gameObject);
    }

/**
 *   @brief   対象をつまんでいるときに呼ばれる関数
 *   @param  ManipulationEventData
 *   @return nothing 
*/
    public void OnManipulationUpdated(ManipulationEventData eventData)
    {       
        // 重力をオフにする
        GetComponent<Rigidbody>().useGravity = true;

        // 動かないように
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        // 設定した速度に合わせてQuaternionを０にする(デフォルトアングルに戻す)
        this.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), step);   

        // クロスヘアーに固定して移動
       gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * ObjectDistance;
    }
}


