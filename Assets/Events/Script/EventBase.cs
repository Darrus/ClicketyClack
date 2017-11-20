/** 
 *  @file    EventBase.cs
 *  @author  Darrus
 *  @date    17/11/2017  
 *  @brief   Contains the base event class
 */
using UnityEngine;
using UnityEngine.Events;

/** 
 *  @brief   Event base class that contains a solved variable
 */
public class EventBase : MonoBehaviour
{
    [SerializeField]
    protected bool solved;

    /** 
     *  @brief   a Getter and Setter function, upon setting the variable to true, it'll call the the OnSolved function
     *  @return  returns a boolean of the solved variable
     */
    public bool Solved
    {
        get
        {
            return solved;
        }
        set
        {
            solved = value;
            if(solved)
            {
                OnSolved();
            }
        }
    }


    /** 
     *  @brief   The OnSolved function is called upon setting the solved variable is true, this is a virtual variable so it can be inherited by child classes
     */
    protected virtual void OnSolved()
    {
    }
}
