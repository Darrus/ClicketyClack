/** 
 *  @file    TriggerTrainDeath.cs
 *  @author  Darrus
 *  @date    17/11/2017  
 *  @brief   Contains trigger train death class
 */
using UnityEngine;

/** 
 *  @brief   Triggers the train death upon collider with the trigger tag
 */
[RequireComponent(typeof(Collider))]
public class TriggerTrainDeath : MonoBehaviour {
    [TagSelector]
    public string triggerTag;

    /** 
     *  @brief   Upon collision enter, trigger trains death according to which part was collided with.
     *  @param   collision, the collision class that this object has collided with
     */
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(triggerTag))
        {

            if (collision.gameObject.name == "head")
            {
                GameBoard.Instance.TheTrainLife.killHead();
            }
            if (collision.gameObject.name == "carriage")
            {
                GameBoard.Instance.TheTrainLife.KillCarriage();
            }
            if (collision.gameObject.name == "cargo")
            {
                GameBoard.Instance.TheTrainLife.KillCargo();
            }

            Rigidbody rigid = collision.transform.GetComponent<Rigidbody>();
            if (!rigid)
                return;

            rigid.useGravity = true;
            rigid.isKinematic = false;
        }
    }
}
