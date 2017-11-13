using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JapaneseText : FontScalable
{
    [Multiline]
    public string[] japanese;

    private TextMesh text;

    private void Awake()
    {
        text = GetComponent<TextMesh>();
        TextControll.textNum = 0;
    }

    private void Update()
    {

        switch (TextControll.textNum)
        {
            case 0:
                text.text = japanese[0];
                break;

            case 1:
                text.text = japanese[1];
                break;

            case 2:
                text.text = japanese[2];
                break;

            case 3:
                text.text = japanese[3];
                break;

            case 4:
                text.text = japanese[4];
                TextControll.textNum = 4;
                break;
        }
    }
}
/*
 石炭を貨物に
積んでみよう


    貨物を列車に
連結してみよう

    駅に着くまでに
障害物や盗賊から
列車を防衛しよう


    最後に
トンネルの岩を
どけてみよう
     */
