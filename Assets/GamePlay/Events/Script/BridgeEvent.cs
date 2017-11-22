/** 
 *  @file    BridgeEvent.cs
 *  @author  Darrus
 *  @date    17/11/2017  
 *  @brief   Contains the Bridge event class
 */
using UnityEngine;

/** 
 *  @brief   Kills the player when the bridge event is not solved.
 */
[RequireComponent(typeof(Collider))]
public class BridgeEvent : EventBase {
    [TagSelector]
    public string playerTag;


    /** 
     *  @brief   If the bridge event is not solved, it kills a part of the train
     */
    private void OnTriggerEnter(Collider other)
    {
        if (Solved)
            return;

        if(other.CompareTag(playerTag))
        {
            Rigidbody rigid = other.transform.GetComponent<Rigidbody>();
            rigid.useGravity = true;
            rigid.isKinematic = false;

            if (other.gameObject.name == "head")
            {
                GameBoard.Instance.TheTrainLife.killHead();
            }
            if (other.gameObject.name == "carriage")
            {
                GameBoard.Instance.TheTrainLife.KillCarriage();
            }
            if (other.gameObject.name == "cargo")
            {
                GameBoard.Instance.TheTrainLife.KillCargo();
            }

        }
    }
}
