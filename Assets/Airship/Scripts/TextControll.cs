using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextControll : MonoBehaviour
{
    public static int textNum;
    public GameObject HoloLensCamera;
    private TextMesh text;
    private float scale;

    private void Start()
    {
        text = GetComponent<TextMesh>();
        scale = GetComponent<FontScalable>().fontScale;    
        textNum = 0;
    }

    // Update
    void Update()
    {
        switch (textNum)
        {
            case 0:
                scale = 6.0f;


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
        }
    }


}

