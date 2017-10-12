using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RayCastPoint : MonoBehaviour {

    public List<Vector3> ClickPoints = new List<Vector3>();
    public float SphereCastRadius = 1.0f;
    public int PlacementMode = 0;
    GameObject mouseCube;
    bool startNewObject = true;
    GameObject currentPoint;
    GameObject previousPoint;
    GameObject selectedObject;
    bool hoveringOnObject;
    bool objectIsSelected;
    bool validPlacement;
    bool placementPreview;

    Vector3 tempStart;
    Vector3 tempEnd;
    Vector3 tempStartTangent;
    Vector3 tempEndTangent;

    //public enum PlacementMode
    //{
    //    RoadStart,
    //    RoadEnd,
    //    BuildingPlop
    //}

    //public LineMy Temp;

    public GameObject Point;

    private Vector3 centerPoint;

    void Start()
    {
        centerPoint = new Vector3((Screen.width * 0.5f), (Screen.height * 0.5f), 0);
        placementPreview = false;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(centerPoint);
        RaycastHit[] hits = Physics.SphereCastAll(ray, SphereCastRadius);
        Vector3 worldHitPosition = Vector3.zero;
        selectedObject = null;

        validPlacement = false;
        //PointCS tempPoint = new PointCS();

        if (hits.Length > 0 /*&& GUIUtility.hotControl == 0*/)
        {
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.gameObject.tag == "Point")
                {
                    RaycastHit preciseGroundCollision;
                    if (hit.collider.Raycast(ray, out preciseGroundCollision, Mathf.Infinity))
                    {
                        //tempPoint = hit.transform.gameObject.GetComponent(typeof(PointCS)) as PointCS;
                        Vector3 precisePosition = preciseGroundCollision.point;
                        worldHitPosition = precisePosition;

                        Debug.Log("test == true");

                    }

                }



            }

        }
        if (Input.GetMouseButtonDown(1))
        Debug.Log(Input.mousePosition.x + " : " + Input.mousePosition.y + " : " + Input.mousePosition.z);

        //if (Input.GetMouseButtonDown(1) && hitSuccess && !PointGod.Temp)
        //{

        //    Vector3 temp = new Vector3(worldHitPosition.x, 0, worldHitPosition.z);
        //    Temp.CreateNewPoints(temp);
        //    Temp.CreateNewPoints(temp);
        //    PointGod.updatePoints();
        //}

        //if (Input.GetMouseButtonDown(1) && hitSuccess2 && PointGod.Temp)
        //{
        //    Vector3 temp = new Vector3(worldHitPosition.x, 0, worldHitPosition.z);
        //    GameObject myPoint = Instantiate(Point, temp, Quaternion.identity);
        //    TempTest newPoint = myPoint.GetComponent(typeof(TempTest)) as TempTest;
        //    newPoint.ID = tempPoint.ID_Team + 1;
        //    newPoint.PositionPoint = temp;
        //}

    }
}
