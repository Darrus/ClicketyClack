/** 
*  @file    TrainMovementManager.cs
*  @author  Yin Shuyu (150713R) 
*  
*  @brief Contain class TrainMovementManager
*  
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*  @brief A Parent Class for TrainMovement
*/
#if UNITY_EDITOR
[ExecuteInEditMode()]
#endif
public class TrainMovementManager : MonoBehaviour {

    /**
    *  @brief A struct of Train Part data
    */
    [System.Serializable]
    public struct TrainPart
    {
        public int ID; ///< ID of the train part
        public float distanceGap; ///< Distance gap from the front part of the train
    }

    public float MainSpeed; ///< speed of the train
    public float RenderSpeed; ///< speed of rendering the track
    public float UnRenderSpeed; ///< speed of unrendering the track
    public float TotalTrackDistance; ///< Total Distance of the track

    public TrainPart[] TheTrain; ///< Array of TrainPart for multiply parts of the train

    public bool once; ///< bool trigger to call some codes once


    // Use this for initialization
    void Start () {
        once = false;
    }

    /**
    *  @brief Get Total Track Distance form BezierCurve2.TrackData_List once, when condition met
    */
    void Update()
    {
        if (!once && BezierCurve2.Go)
        {
            TotalTrackDistance = BezierCurve2.TrackData_List[BezierCurve2.TrackData_List.Length - 1].distance;
            once = true;
        }
    }
}
