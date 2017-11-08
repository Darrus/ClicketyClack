using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountAnimation : CreateCount
{
    private float alpha = 1.0f;

    private void Start()
    {
        transform.LookAt(Camera.main.transform.position);
    }

    void Update()
    {
        ChangeText();
        MoveAnimation();
        FadeOutText();
    }

    void ChangeText()
    {
        TextMesh text = GetComponent<TextMesh>();
        text.text = nowCnt + " / " + MaxCnt;
    }

    void MoveAnimation()
    {
        transform.rotation = Camera.main.transform.rotation;
        transform.position += new Vector3(0, 0.1f, 0) * Time.deltaTime;
    }

    void FadeOutText()
    {
        alpha -= 1f * Time.deltaTime;
       GetComponent<TextMesh>().color = new Color(40, 219, 64, alpha);
        
        if (alpha < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
