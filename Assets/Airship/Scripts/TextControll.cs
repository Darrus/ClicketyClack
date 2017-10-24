using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
地面に３DText 生成
チュートリアルシーン作成
電車とオブジェクト配置
一連の流れを試す
調整
UI移行
*/

public class TextControll : MonoBehaviour
{
    public static int textNum;
    private TextMesh text;

    private void Start()
    {
        text = GetComponent<TextMesh>();
        textNum = 0;
    }

    // Update
    void Update()
    {

        switch (textNum)
        {
            case 0:
                text.text = "Place\n" +
                            "Cargo on Train\n";
                break;

            case 1:
                text.text = "Defend\n" +
                            "the\n" +
                            "Train";
                break;


            case 2:
                text.text = "Keep up\n" +
                            "with the Train\n";
                break;

            case 3:
                textNum = 0;
                break;
        }
    }
}

