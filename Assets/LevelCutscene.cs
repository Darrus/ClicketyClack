using UnityEngine;

public class LevelCutscene : MonoBehaviour {
    public GameObject train;
    public HumanController[] victims;

    public void ExecuteCutscene()
    {
        foreach(HumanController victim in victims)
        {
            victim.RunTo(train.transform.position);
        }
    }
}
