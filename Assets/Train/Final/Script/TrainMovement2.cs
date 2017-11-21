/** 
*  @file    TrainMovement2.cs
*  @author  Yin Shuyu (150713R) 
*  @date    21/11/2017
*  @brief Contain class TrainMovement2
*  
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*  @brief Class for Train to Move along with the TrainMovement Point
*/
public class TrainMovement2 : MonoBehaviour {

    public TrainMovement PointFollow; ///< TrainMovement point to get the position and the rotation

    public GameObject SmokeParticleEffect; ///< GameObject of the Smoke Particle Effect
    public GameObject FlameParticleEffect; ///< GameObject of the Flame Particle Effect
    public float TrainHeightGap; ///< height gap of the train to align with the track
    private bool connected; ///< bool trigger connection between TrainMovement point

    /**
    *  @brief Set ParticleEffects state
    */
    void Start () {

        FlameParticleEffect.GetComponentInChildren<ParticleSystem>().Stop();

        if (PointFollow.ID == 1 && SmokeParticleEffect != null)
            SmokeParticleEffect.GetComponent<ParticleSystem>().Play();
        connected = true;
    }

    /**
    *  @brief update check for Death, movement and ParticleEffects
    */
    void Update () 
    {
        if (BezierCurve2.Go)
        {
            if (connected)
            {
                transform.position = new Vector3(PointFollow.transform.position.x, PointFollow.transform.position.y + TrainHeightGap, PointFollow.transform.position.z);
                transform.localRotation = PointFollow.transform.localRotation;


                //Vector3 lookPos = transform.position + BezierCurve2.TrackData_List[PointFollow.Point_ID].tangent;
                //var rotation = Quaternion.LookRotation(lookPos);
                //rotation *= Quaternion.Euler(0, -90, 0); // this adds a 90 degrees Y rotation
                //transform.localRotation = rotation;
                FlameParticleEffect.GetComponentInChildren<ParticleSystem>().Stop();
            }
            else
            {
                if (PointFollow.ID == 1 && SmokeParticleEffect != null)
                    if (SmokeParticleEffect.GetComponent<ParticleSystem>().isPlaying)
                        SmokeParticleEffect.GetComponent<ParticleSystem>().Stop();

                if (!FlameParticleEffect.GetComponentInChildren<ParticleSystem>().isPlaying)
                    FlameParticleEffect.GetComponentInChildren<ParticleSystem>().Play();

                FlameParticleEffect.transform.rotation = Quaternion.identity;
            }
            checkConnection();
        }
        else if(LevelManager.Instance.CargoOn)
        {
            transform.position = new Vector3(PointFollow.transform.position.x, PointFollow.transform.position.y + TrainHeightGap, PointFollow.transform.position.z);
            transform.localRotation = PointFollow.transform.localRotation;
        }
    }

    /**
   *   @brief Update Train connection with train death
   *  
   *   @return null
   */
    void checkConnection()
    {
        if (GameBoard.Singleton != null)
        {
            if (connected)
            {
                if (PointFollow.ID == 1)
                    connected = GameBoard.Instance.TheTrainLife.head;
                if (PointFollow.ID == 2)
                    connected = GameBoard.Instance.TheTrainLife.Carriage;
                if (PointFollow.ID == 3)
                    connected = GameBoard.Instance.TheTrainLife.Cargo;
            }
        }
    }
}
