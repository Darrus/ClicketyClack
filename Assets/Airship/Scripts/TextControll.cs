using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
                text.text = "Pick up the pieces coals \n and place it into the cargo.";
                break;

            case 1:
                text.text = "Lift the cargo \n and attach it into the train";
                break;
                
            case 2:
                text.text = "Bandits will attempt to \n destroy the train, \n protect it at all cause!";
                break;

            case 3:
                text.text = "The Bandits have set up \n a trap for the train, \n Clear the path of rocks";
                break;

            case 4:
                text.text = "LEVEL CLEAR";
                textNum = 4;
                break;
        }
    }


}

