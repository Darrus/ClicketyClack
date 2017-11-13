using UnityEngine;
using System.Collections;

public class LevelSelectController : MonoBehaviour {
    public MainMenuManager mainMenu;
    public GameObject[] boardButtons;
    public HumanController[] victims;
    public HumanController[] bandits;
    public Transform[] victimIdlePoints;
    public Transform[] banditIdlePoints;
    public Transform[] tunnels;

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            PlayStartingAnimation();
    }
#endif

    public void PlayStartingAnimation()
    {
        StartCoroutine(StartingAnimation());
    }

    public void SelectLevel(int level)
    {
        StartCoroutine(TransitionAnimation(level));
    }

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
        yield break;
    }

    IEnumerator TransitionAnimation(int level)
    {
        foreach(Transform child in tunnels[level])
        {
            foreach(HumanController victim in victims)
            {
                victim.RunTo(child.position);
            }
        }

        float timeTillBanditMove = 3.0f;
        yield return new WaitForSeconds(timeTillBanditMove);

        foreach (Transform child in tunnels[level])
        {
            foreach (HumanController bandit in bandits)
            {
                bandit.RunTo(child.position);
            }
        }

        float timeTillSceneTransition = 8.0f;
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
