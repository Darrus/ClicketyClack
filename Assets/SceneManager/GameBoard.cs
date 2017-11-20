using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [System.Serializable]
    public struct Train
    {
        public bool head;
        public bool Carriage;
        public bool Cargo;
        public int Life;

        public Train(bool T)
        {
            head = T;
            Carriage = T;
            Cargo = T;
            Life = 3;
        }

        public void killHead()
        {
            head = false;
            Carriage = false;
            Cargo = false;
            Life = 0;
            GameBoard.Instance.Check_Light_Bulbs();
        }

        public void KillCarriage()
        {
            Carriage = false;
            Cargo = false;
            Life = 1;
            GameBoard.Instance.Check_Light_Bulbs();
        }
        public void KillCargo()
        {
            Cargo = false;
            Life = 2;
            GameBoard.Instance.Check_Light_Bulbs();
        }
    };

    [HideInInspector]
    public Train TheTrainLife = new Train(true);

    public Material Lighten_Bulb_material;
    public Material darken_Bulb_material;

    public List<Renderer> Light_Bulbs;

    public GameObject Victory_Screen;
    public GameObject Lose_Screen;

    public TrainMovement Train_Head_Movement;
    public TextMesh BoardText;

    public static GameBoard Singleton = null;

    public static GameBoard Instance
    {
        get { return Singleton; }
    }

    void Awake()
    {
        Debug.Log("GameBoard: Starting.");

        if (Singleton != null)
        {
            Debug.LogError("Multiple GameBoard Singletons exist!");
            return;
        }
        Singleton = this;

        Victory_Screen.SetActive(false);
        Lose_Screen.SetActive(false);
    }
    // Use this for initialization
    void Start()
    {
        BoardText.text = "At Station";
    }

    // Update is called once per frame
    void Update()
    {
        Check_Text();

        Check_Win_Lose_Condition();
    }

    void Check_Text()
    {
        if (LevelManager.Instance.MoveOut && LevelManager.Instance.Play && !LevelManager.Instance.ReachStation && TheTrainLife.Life != 0)
        {
            Check_Progress();
            BoardText.text = "Progress: " + Check_Progress() + "%";
        }

    }

    string Check_Progress()
    {
        int percentage = (int)(Train_Head_Movement.distanceTravel / Train_Head_Movement.Manager.TotalTrackDistance * 100);

        return percentage.ToString();
    }

    void Check_Win_Lose_Condition()
    {
        if (TheTrainLife.Life == 0)
        {
            if (!Lose_Screen.activeSelf)
            {
                BoardText.text = "Derailed!";
                Lose_Screen.SetActive(true);

                if (Victory_Screen.activeSelf)
                {
                    Victory_Screen.SetActive(false);
                }
            }
        }

        if (LevelManager.Instance.ReachStation && LevelManager.Instance.MoveOut && TheTrainLife.Life != 0)
        {
            if (!Victory_Screen.activeSelf)
            {

                BoardText.text = "Right On Track!";

                Victory_Screen.SetActive(true);
            }

            if (LevelManager.Instance.Tutorial)
                TextControll.textNum = 4;
        }

    }

    public void Button_Menu()
    {
        AppManager.Instance.RenderingTrack = false;
        AppManager.Instance.curScene = AppManager.GameScene.mainmenu;
        AppManager.Instance.LoadScene();
    }

    public void Button_Retry()
    {
        AppManager.Instance.ReStartLevel = true;
        AppManager.Instance.LoadScene();
    }

    public void Button_NextLevel()
    {
        AppManager.Instance.NextLevel();
    }


    public void Check_Light_Bulbs()
    {
        if (TheTrainLife.head)
            Light_Bulbs[0].material = Lighten_Bulb_material;
        else
            Light_Bulbs[0].material = darken_Bulb_material;

        if (TheTrainLife.Carriage)
            Light_Bulbs[1].material = Lighten_Bulb_material;
        else
            Light_Bulbs[1].material = darken_Bulb_material;

        if (TheTrainLife.Cargo)
            Light_Bulbs[2].material = Lighten_Bulb_material;
        else
            Light_Bulbs[2].material = darken_Bulb_material;
    }

    public void SelfDestory()
    {
        Victory_Screen.SetActive(false);
        Lose_Screen.SetActive(false);
        GameBoard.Singleton.enabled = false;
        GameBoard.Singleton = null;
    }
}
