/** 
*  @file    GameBoard.cs
*  @author  Yin Shuyu (150713R) 
*  @date    21/11/2017
*  @brief Contain Singleton class GameBoard
*  
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*  @brief Singleton Class contain Trian Life, and game progress State
*/
public class GameBoard : MonoBehaviour
{
    /**
    *  @brief Struct for Train Life and functions
    */
    [System.Serializable]
    public struct Train
    {
        public bool head; ///< bool of whether head of the train is alive
        public bool Carriage; ///< bool of whether Carriage of the train is alive
        public bool Cargo; ///< bool of whether Cargo of the train is alive
        public int Life; ///< number of parts of train is still alive

        /**
        *   @brief struct constructor setting Default value, struct cannot be paramless
        *  
        *   @param bool T, just a random param
        *  
        *   @return null
        */
        public Train(bool T)
        {
            head = true;
            Carriage = true;
            Cargo = true;
            Life = 3;
        }

        /**
        *   @brief function to call the kill the head of the train, as well as parts behind the head
        *  
        *   @return null
        */
        public void killHead()
        {
            head = false;
            Carriage = false;
            Cargo = false;
            Life = 0;
            GameBoard.Instance.Check_Light_Bulbs();
        }

        /**
        *   @brief function to call the kill the Carriage of the train, as well as parts behind the Carriage
        *  
        *   @return null
        */
        public void KillCarriage()
        {
            Carriage = false;
            Cargo = false;
            Life = 1;
            GameBoard.Instance.Check_Light_Bulbs();
        }

        /**
        *   @brief function to call the kill the Cargo of the train
        *  
        *   @return null
        */
        public void KillCargo()
        {
            Cargo = false;
            Life = 2;
            GameBoard.Instance.Check_Light_Bulbs();
        }
    };

    [HideInInspector]
    public Train TheTrainLife = new Train(true); ///< Train Struct as well as setting the Default values

    public Material Lighten_Bulb_material; ///< Material of Light Bulb is lighten
    public Material darken_Bulb_material; ///< Material of Light Bulb is Darken

    public List<Renderer> Light_Bulbs; ///< List of the Light_Bulb's Renderer

    public GameObject Victory_Screen; ///< GameObject of the Victory Screen
    public GameObject Lose_Screen; ///< GameObject of the Lose Screen
    public GameObject LevelStatus; ///< GameObject of the level status data

    public TrainMovement Train_Head_Movement; ///< TrainMovement of the Head of the Train

    public TextMesh Progress; ///< Text of the Game Progress State
    public TextMesh victimLeft; ///< Text of number victim left in level to save

    public TextMesh feedback; ///< Text for some feedback

    public static GameBoard Singleton = null; ///< Static Singleton of the GameBoard

    public static GameBoard Instance ///< Static Instance function to get all the Data of GameBoard
    {
        get { return Singleton; }
    }

    /**
    *  @brief At the Awake of the gameobject, need to set the Singleton
    */
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
        LevelStatus.SetActive(true);
    }

    // Use this for initialization
    void Start()
    {
        UpdateVictimText();
    }

    public void UpdateVictimText()
    {
        victimLeft.text = VictimManager.VictimRemain_Level[(int)(AppManager.Instance.gameState)-1].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Check_Text();
        Check_Win_Lose_Condition();
    }

    /**
    *   @brief Set Game Progress according the train distance to the total track distance in BoardTex
    *  
    *   @return null
    */
    void Check_Text()
    {
        if (LevelManager.Instance.MoveOut && LevelManager.Instance.Play && !LevelManager.Instance.ReachStation && TheTrainLife.Life != 0)
        {
            Check_Progress();
            Progress.text = ": " + Check_Progress() + " %";
        }

    }

    /**
    *   @brief get percentage of the train distance to the total track distance
    *  
    *   @return string percentage, int to string
    */
    string Check_Progress()
    {
        int percentage = (int)(Train_Head_Movement.distanceTravel / Train_Head_Movement.Manager.TotalTrackDistance * 100);

        return percentage.ToString();
    }

    /**
    *   @brief Check for Victory and Lose Condition and set according
    *  
    *   @return null
    */
    void Check_Win_Lose_Condition()
    {
        if (TheTrainLife.Life == 0)
        {
            if (!Lose_Screen.activeSelf)
            {
                LevelStatus.SetActive(false);
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

                LevelManager.Instance.VictimList.UpdateVictimList();

                LevelStatus.SetActive(false);
                Victory_Screen.SetActive(true);
            }

            if (LevelManager.Instance.Tutorial)
                TextControll.textNum = 4;
        }

    }

	/**
    *   @brief A function to Change Main Menu Scene
    *  
    *   @return null
    */
    public void Button_Menu()
    {
        AppManager.Instance.RenderingTrack = false;
        AppManager.Instance.gameState = AppManager.GameScene.mainmenu;
        AppManager.Instance.LoadScene();
    }
	
	/**
    *   @brief A function to ReStart current level Scene
    *  
    *   @return null
    */
    public void Button_Retry()
    {
        AppManager.Instance.ReStartLevel = true;
        AppManager.Instance.LoadScene();
    }

	/**
    *   @brief A function to go next level Scene
    *  
    *   @return null
    */
    public void Button_NextLevel()
    {
        int temp = VictimManager.Check_Level_RequireVictimSave((int)(AppManager.Instance.gameState) + 1);
        if (temp == 0)
            AppManager.Instance.NextLevel();
        else
        {
            feedback.text = "Need to save \n" + temp + " more victim !!";
        }
    }

	/**
    *   @brief A function to Change The Light_Bulb's material depending on the Train life
    *  
    *   @return null
    */
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

	/**
    *   @brief A function to Destory the GameBoard's Singleton
    *  
    *   @return null
    */
    public void SelfDestory()
    {
        Victory_Screen.SetActive(false);
        Lose_Screen.SetActive(false);
        GameBoard.Singleton.enabled = false;
        GameBoard.Singleton = null;
    }
}
