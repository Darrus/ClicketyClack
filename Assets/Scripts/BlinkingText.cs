using UnityEngine;
using System;

[RequireComponent(typeof(MeshRenderer))]
public class BlinkingText : MonoBehaviour {
    public float timeBetweenFlash;
    private float timer;
    MeshRenderer myRenderer;
    bool flash = false;
    Color[] colors = new Color[2];

    void Awake()
    {
        myRenderer = GetComponent<MeshRenderer>();
        timer = timeBetweenFlash;
        colors[0] = Color.white;
        colors[1] = Color.yellow;
    }

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
