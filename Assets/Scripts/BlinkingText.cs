/** 
 *  @file    BlinkingText.cs
 *  @author  Darrus
 *  @date    17/11/2017  
 *  @brief   Contains the blinking text class
 */
using UnityEngine;
using System;

/**
  *  @brief Class that handles blinking an object, Requires the Mesh Renderer component
  */
[RequireComponent(typeof(MeshRenderer))]
public class BlinkingText : MonoBehaviour {
    public float timeBetweenFlash;
    private float timer;
    MeshRenderer myRenderer;
    bool flash = false;
    Color[] colors = new Color[2];

    /**
     * @brief Sets the first color to white and the second color to yellow
     */
    void Awake()
    {
        myRenderer = GetComponent<MeshRenderer>();
        timer = timeBetweenFlash;
        colors[0] = Color.white;
        colors[1] = Color.yellow;
    }

    /**
    * @brief Calls every frame of the game
    */
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            timer = timeBetweenFlash;
            flash = !flash;
            int flashIndex = Convert.ToInt16(flash);
            myRenderer.material.SetColor("_Color", colors[flashIndex]);
        }
    }
}
