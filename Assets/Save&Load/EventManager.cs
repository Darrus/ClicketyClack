using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public enum Event_Type
    {
        Bridge_Repair_pair = 1,
        Bridge_Event = 2,
        Dynamite_Event = 3,
        Tower_Event = 4,
        Mound_Event = 5,
        Airship_Event = 6

    }

    public GameObject Bridge_Repair_pair;
    public GameObject Bridge_Event;
    public GameObject Dynamite_Event;
    public GameObject Tower_Event;
    public GameObject Mound_Event;
    public GameObject Airship_Event;

    public GameObject Event_Parent;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateEvents(Vector3 position, Quaternion rotation, int type)
    {
        GameObject newEvent = null;
        switch (type)
        {
            case (int)Event_Type.Bridge_Repair_pair:
                {
                    newEvent = Instantiate(Bridge_Repair_pair, position, rotation);
                    EventSC myEventSc = newEvent.GetComponent(typeof(EventSC)) as EventSC;
                    myEventSc.type = (int)Event_Type.Bridge_Repair_pair;
                    break;
                }
            case (int)Event_Type.Bridge_Event:
                {
                    newEvent = Instantiate(Bridge_Event, position, rotation);
                    EventSC myEventSc = newEvent.GetComponent(typeof(EventSC)) as EventSC;
                    myEventSc.type = (int)Event_Type.Bridge_Event;
                    break;
                }
            case (int)Event_Type.Dynamite_Event:
                {
                    newEvent = Instantiate(Dynamite_Event, position, rotation);
                    EventSC myEventSc = newEvent.GetComponent(typeof(EventSC)) as EventSC;
                    myEventSc.type = (int)Event_Type.Dynamite_Event;
                    break;
                }
            case (int)Event_Type.Tower_Event:
                {
                    newEvent = Instantiate(Tower_Event, position, rotation);
                    EventSC myEventSc = newEvent.GetComponent(typeof(EventSC)) as EventSC;
                    myEventSc.type = (int)Event_Type.Tower_Event;
                    break;
                }
            case (int)Event_Type.Mound_Event:
                {
                    newEvent = Instantiate(Mound_Event, position, rotation);
                    EventSC myEventSc = newEvent.GetComponent(typeof(EventSC)) as EventSC;
                    myEventSc.type = (int)Event_Type.Mound_Event;
                    break;
                }
            case (int)Event_Type.Airship_Event:
                {
                    newEvent = Instantiate(Airship_Event, position, rotation);
                    EventSC myEventSc = newEvent.GetComponent(typeof(EventSC)) as EventSC;
                    myEventSc.type = (int)Event_Type.Airship_Event;
                    break;
                }
        }

        newEvent.tag = "Event";
        newEvent.transform.parent = Event_Parent.transform;
    }

}
