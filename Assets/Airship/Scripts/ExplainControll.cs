using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplainControll : MonoBehaviour
{
    [Multiline]
    public string[] Explane;
    private TextMesh text;

    private void Awake()
    {
        text = GetComponent<TextMesh>();
    }

    private void Update()
    {
        switch (PageControll.Num)
        {
            case 0:
                text.text = Explane[0];
                break;

            case 1:
                text.text = Explane[1];
                break;

            case 2:
                text.text = Explane[2];
                break;
        }
    }
}
