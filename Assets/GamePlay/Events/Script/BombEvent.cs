/** 
 *  @file    BombEvent.cs
 *  @author  Darrus
 *  @date    17/11/2017  
 *  @brief   Bomb event manager
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 *  @brief   A class to make sure to ignore collision with the player
 */
public class BombEvent : EventBase {
    [TagSelector]
    public string playerTag;
    public Collider bombCollider;

    /** 
     *  @brief Upon awake, find player through tag and ignore all collisions with player
     */
    private void Awake()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag(playerTag);
        foreach(GameObject player in playerObjects)
        {
            Physics.IgnoreCollision(player.GetComponent<Collider>(), bombCollider);
        }
    }
}
