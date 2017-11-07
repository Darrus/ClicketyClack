using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextControll : MonoBehaviour
{
    [Multiline]
    public string[] English;
    public static int textNum;

    private TextMesh text;

    private void Awake()
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
                text.text = English[0];
                break;

            case 1:
                text.text = English[1];
                break;

            case 2:
                text.text = English[2];
                break;

            case 3:
                text.text = English[3];
                break;

            case 4:
                text.text = English[4];
                textNum = 4;
                break;
        }
    }


}
/*
 *             case 0:
                text.text = "Put Coal\n" +
                            "in\n" +
                            "Cargo\n";
                break;

            case 1:
                text.text = "Place\n" +
                            "Cargo on Train\n";
                break;
                
            case 2:
                text.text = "Protect the Train\n" +
                            "Watch out for\n" +
                            "Bandit";
                break;

            case 3:
                text.text = "Remove the Rocks\n" +
                            "from the\n" +
                            "Cave";
                break;

            case 4:
                text.text = "CLAER";
                textNum = 4;
                break;
 */
