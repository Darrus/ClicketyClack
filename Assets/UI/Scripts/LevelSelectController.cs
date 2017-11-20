﻿/** 
 *  @file    LevelSelectController.cs
 *  @author  Darrus
 *  @date    17/11/2017  
 *  @brief   Controls the animation & level transition of the main menu scene
 */
using UnityEngine;
using System.Collections;

/**
 * @brief   Controls the animation & level transition of the main menu scene
 */
public class LevelSelectController : MonoBehaviour {
    public MainMenuManager mainMenu;
    public GameObject[] boardButtons;
    public HumanController[] victims;
    public HumanController[] bandits;
    public Transform[] victimIdlePoints;
    public Transform[] banditIdlePoints;
    public Transform[] tunnels;

    bool changeLevel = false;

    /**
     * @brief Updates every frame, Debug controls for Unity Editor
     */
#if UNITY_EDITOR
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
            //PlayStartingAnimation();
    }
#endif

    /**
     * @brief Plays the StartingAnimation coroutine
     */
    public void PlayStartingAnimation()
    {
        StartCoroutine(StartingAnimation());
    }

    /**
     * @brief Plays the TransitionAnimation to transition to desired level.
     * @param level, an integer to select what level to transition to.
     */
    public void SelectLevel(int level)
    {
        if (changeLevel)
            return;

        changeLevel = true;
        StartCoroutine(TransitionAnimation(level));
    }

    /**
     * @brief Starting animation when the players chooses level select.
     * @return IEnumerator, returns this coroutine state
     */
    IEnumerator StartingAnimation()
    {
        for(int i = 0; i < victims.Length; ++i)
        {
            victims[i].RunTo(victimIdlePoints[i].position);
            victims[i].ChangeState(HumanController.CharacterStates.DEFEAT);
        }

        for (int i = 0; i < bandits.Length; ++i)
        {
            bandits[i].RunTo(banditIdlePoints[i].position);
            bandits[i].ChangeState(HumanController.CharacterStates.CHEER);
        }

        float timeTillBoardAppear = 10.0f;
        yield return new WaitForSeconds(timeTillBoardAppear);

        for(int i = 0; i < boardButtons.Length; ++i)
        {
            boardButtons[i].SetActive(true);
        }

        for (int i = 0; i < tunnels.Length; ++i)
        {
            tunnels[i].GetComponent<OnClick>().enabled = true;
        }
        yield break;
    }

    /**
     * @brief Transition animation, plays the transitioning animation and eventually calls the main menu to change scene
     * @param level, an integer to select what level to transition to.
     * @return IEnumerator, returns this coroutine state
     */
    IEnumerator TransitionAnimation(int level)
    {
        foreach(Transform child in tunnels[level])
        {
            foreach(HumanController victim in victims)
            {
                victim.RunTo(child.position);
            }
        }

        float timeTillBanditMove = 1.5f;
        yield return new WaitForSeconds(timeTillBanditMove);

        foreach (Transform child in tunnels[level])
        {
            foreach (HumanController bandit in bandits)
            {
                bandit.RunTo(child.position);
            }
        }

        float timeTillSceneTransition = 6.0f;
        yield return new WaitForSeconds(timeTillSceneTransition);

        switch(level)
        {
            case 0:
                mainMenu.Button_Level_1();
                break;
            case 1:
                mainMenu.Button_Level_2();
                break;
            case 2:
                mainMenu.Button_Level_3();
                break;
            case 3:
                mainMenu.Button_Level_4();
                break;
        }

        mainMenu.Button_Load_Level();
        yield break;
    }
}
