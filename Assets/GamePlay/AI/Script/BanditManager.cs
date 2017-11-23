/** 
 *  @file    BanditManager.cs
 *  @author  Darrus
 *  @date    17/11/2017  
 *  @brief   Handles and controls the group of bandits spawned by events
 */
using UnityEngine;

/** 
 *  @brief   Handles and controls the group of bandits spawned by events
 */
public class BanditManager : MonoBehaviour {
    [SerializeField]
    EventBase eventBase;

    public GameObject[] banditTypes;
    public GameObject[] spawnPoints;
    public float gatherRadius;

    public GameObject[] activeBandits;
    bool spawned = true;
    bool cheer = false;
    bool defeat = false;

    /** 
     *  @brief Checks whether there are active bandits, if add the amount of active bandits according to spawn
     */
    private void Awake()
    {
        if(activeBandits.Length <= 0)
        {
            activeBandits = new GameObject[spawnPoints.Length];
            spawned = false;
        }
    }

    /** 
     *  @brief Spawn the bandits
     */
    public void SpawnBandits()
    {
        if (eventBase.Solved || spawned) 
            return;

        spawned = true;

        for (int i = 0; i < activeBandits.Length; ++i)
        {
            int banditSelect = Random.Range(0, banditTypes.Length);
            GameObject newBandit = Instantiate(banditTypes[banditSelect]);
            newBandit.transform.position = spawnPoints[i].transform.position;
            newBandit.transform.SetParent(transform);
            activeBandits[i] = newBandit;
        }
    }

    /** 
     *  @brief Queue cheer commands for all active bandits
     */
    public void BanditsCheer()
    {
        if (!spawned || cheer)
            return;

        cheer = true;
        foreach (GameObject bandit in activeBandits)
        {
            HumanController sm = bandit.GetComponent<HumanController>();
            sm.ChangeState(HumanController.CharacterStates.CHEER);
        }
    }

    /** 
     *  @brief Get all active bandits to gather around the center with a given radius
     */
    public void Gather()
    {
        if (GameBoard.Singleton == null)
            return;
        if (!spawned || GameBoard.Instance.TheTrainLife.Life == 0)
            return;

        Vector3 center = transform.position;
        foreach (GameObject bandit in activeBandits)
        {
            HumanController sm = bandit.GetComponent<HumanController>();
            Vector3 target = center + (bandit.transform.position - center).normalized * gatherRadius;
            sm.RunTo(target);
        }
    }

    /** 
     *  @brief Queue defeat commands for all active bandits
     */
    public void BanditsDefeat()
    {
        if(GameBoard.Singleton == null)
            return;

        if (!spawned || GameBoard.Instance.TheTrainLife.Life == 0 || defeat)
            return;

        defeat = true;
        foreach(GameObject bandit in activeBandits)
        {
            HumanController sm = bandit.GetComponent<HumanController>();
            sm.ChangeState(HumanController.CharacterStates.DEFEAT);
        }
    }

    /** 
     *  @brief Check if the event has been solved, if so set all active bandits to defeat
     */
    private void Update()
    {
        if(spawned)
        {
            if(eventBase.Solved)
            {
                foreach (GameObject bandit in activeBandits)
                {
                    HumanController sm = bandit.GetComponent<HumanController>();
                    sm.ChangeState(HumanController.CharacterStates.DEFEAT);
                }
            }
            else if (GameBoard.Singleton != null && GameBoard.Instance.TheTrainLife.Life == 0 && !cheer)
            {
                cheer = true;
                foreach (GameObject bandit in activeBandits)
                {
                    HumanController sm = bandit.GetComponent<HumanController>();
                    sm.ChangeState(HumanController.CharacterStates.CHEER);
                }
            }
        }
    }

    /** 
     *  @brief Draw a blue wire sphere to indicate gather radius
     */
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.0f, 0.0f, 1.0f);
        Gizmos.DrawWireSphere(transform.position, gatherRadius);
    }

    /** 
     *  @brief Draw a grey cube at bandit spawn positions
     */
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
