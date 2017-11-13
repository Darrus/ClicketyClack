using System.Collections.Generic;
using UnityEngine;

public class BanditManager : MonoBehaviour {
    [SerializeField]
    EventBase eventBase;

    public GameObject[] banditTypes;
    public GameObject[] spawnPoints;
    public float gatherRadius;

    List<GameObject> activeBandits;
    bool spawned = false;
    bool defeat = false;

    private void Awake()
    {
        activeBandits = new List<GameObject>();
    }

    public void SpawnBandits()
    {
        if (eventBase.Solved || spawned) 
            return;

        spawned = true;
        Vector3 center = transform.position;

        for(int i = 0; i < spawnPoints.Length; ++i)
        {
            int banditSelect = Random.Range(0, banditTypes.Length);
            GameObject newBandit = Instantiate(banditTypes[banditSelect]);
            newBandit.transform.position = spawnPoints[i].transform.position;
            activeBandits.Add(newBandit);
            HumanController control = newBandit.GetComponent<HumanController>();
            Vector3 target = center + (spawnPoints[i].transform.position - center).normalized * gatherRadius;
            control.RunTo(target);
            control.ChangeState(HumanController.CharacterStates.CHEER);
        }
    }

    public void BanditsDefeat()
    {
        if (!spawned || LevelManager.TheTrainLife.Life == 0)
            return;

        defeat = true;
        foreach(GameObject bandit in activeBandits)
        {
            HumanController sm = bandit.GetComponent<HumanController>();
            sm.ChangeState(HumanController.CharacterStates.DEFEAT);
        }
    }

    private void Update()
    {
        if(spawned && eventBase.Solved)
        {
            foreach(GameObject bandit in activeBandits)
            {
                HumanController sm = bandit.GetComponent<HumanController>();
                sm.ChangeState(HumanController.CharacterStates.DEFEAT);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.0f, 0.0f, 1.0f);
        Gizmos.DrawWireSphere(transform.position, gatherRadius);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.5f, 0.5f, 0.5f);
        Vector3 cubeSize = new Vector3(0.05f, 0.05f, 0.05f);
        foreach (GameObject spawn in spawnPoints)
        {
            Gizmos.DrawWireCube(spawn.transform.position, cubeSize);
        }
    }
}
