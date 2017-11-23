/** 
*  @file    LevelCutscene.cs
*  @author  Goh Zheng Yuan 
*  @date    23/11/2017
*  @brief   Contain LevelCutscene class
*/

using UnityEngine;

/**
  *  @brief The initial cutscene that plays before the train moves  
  */
public class LevelCutscene : MonoBehaviour {
    public GameObject train;
    public HumanController[] victims;

    private bool once;
    public float timerTillSceneChange;

    private void Start()
    {
        once = false;
    }

    private void Update()
    {
        if (LevelManager.Singleton != null)
        {
            if (!once && !AppManager.Instance.RenderingTrack && LevelManager.Instance.MoveOut)
            {
                ExecuteCutscene();
                once = true;
            }
            else if (once && !LevelManager.Instance.Play)
            {
                bool check = true;
                foreach (HumanController victim in victims)
                {
                    if (victim.gameObject.activeSelf)
                        check = false;
                }
                if (check)
                {
                    LevelManager.Instance.Button_Play();
                    (GetComponent(typeof(LevelCutscene)) as LevelCutscene).enabled = false;
                }
            }
        }
        if(once)
        {
            timerTillSceneChange -= Time.deltaTime;
            if(timerTillSceneChange <= 0.0f)
            {
                foreach (HumanController victim in victims)
                {
                    victim.gameObject.SetActive(false);
                }
            }
        }
    }

    /**
      *  @brief Execute cutscene animation
      */
    public void ExecuteCutscene()
    {
        foreach(HumanController victim in victims)
        {
            victim.RunTo(train.transform.position);
        }
    }
}
