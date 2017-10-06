using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachine : MonoBehaviour {
    public string[] categoryNames; // Temporary
    [Range(0, 360)]
    public int[] categoryAngles;

    public Transform hand;
    public TextMesh categoryText;
    public TextMesh itemText;

    [Range(0.0f, 1.0f)]
    public float eventChance;

    public float initialSpinDuration;
    public float minSpinSpeed;
    public float maxSpinSpeed;

    [Range(0.0f, 1.0f)]
    public float cutOffSpeed;

    private bool spinning;

    //private ISlotEvent[] events;
    //private SLOT_EVENTS selectedEvent;

    private float speed;
    private float initialSpinTimer;
    private const float pi = 3.142f;

#if UNITY_EDITOR
    private float time;
#endif

    private float RandomSpeed
    {
        get
        {
            return Random.Range(minSpinSpeed, maxSpinSpeed);
        }
    }

    private void Start()
    {
        hand = transform.Find("Pivot");
        initialSpinTimer = initialSpinDuration;
        
        //events = new ISlotEvent[(int)SLOT_EVENTS.MAX];
        //events[(int)SLOT_EVENTS.SNAP_UP] = new SlotEvent_SnapUp(hand);
        //selectedEvent = SLOT_EVENTS.SNAP_UP;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spin();
        }

        if (spinning)
            time += Time.deltaTime;
#endif
    }

    private void FixedUpdate()
    {
        if(spinning)
        { 
            initialSpinTimer -= Time.fixedDeltaTime;
            if (speed > 1.0f)
            {
                if(initialSpinTimer <= 0.0f)
                {
                    float deacceleration = speed * cutOffSpeed * Time.fixedDeltaTime;
                    speed -= deacceleration;
                }
                hand.Rotate(0.0f, 0.0f, -speed * Time.fixedDeltaTime);
            }
            else
            {
                spinning = false;
                initialSpinTimer = initialSpinDuration;
                float handAngle = -hand.localEulerAngles.z;
                if (handAngle < 0.0f)
                    handAngle = 360.0f + handAngle;

#if UNITY_EDITOR
                Debug.Log(handAngle);
                Debug.Log(time);
#endif
                for(int i = 1; i < categoryAngles.Length; ++i)
                {
                    if(handAngle < categoryAngles[i - 1])
                    {
                        categoryText.text = categoryNames[i - 1];
                        return;
                    }
                }
                categoryText.text = categoryNames[categoryNames.Length - 1];
            }
        }

    }

    public void Spin()
    {
        if(!spinning)
        {
            spinning = true;
            //slotState = SLOT_STATES.SPIN;
            speed = RandomSpeed;
#if UNITY_EDITOR
            time = 0.0f;
#endif
        }
    }
}
