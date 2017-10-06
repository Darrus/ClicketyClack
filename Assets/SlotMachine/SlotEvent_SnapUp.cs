using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotEvent_SnapUp : ISlotEvent
{
    public float jitterDuration = 2.0f;
    public float jitterSpeed = 2.5f;
    public int SnapAngle
    {
        get
        {
            return Random.Range(5, 10);
        }
    }

    float jitterTimer;
    Transform hand;

    public SlotEvent_SnapUp(Transform _hand)
    {
        hand = _hand;
        jitterTimer = jitterDuration;
    }

    public bool UpdateEvent()
    {
        jitterTimer -= Time.deltaTime;

        if (jitterTimer > 0.0f)
        {
            jitterSpeed = -jitterSpeed;
            hand.transform.Rotate(jitterSpeed, 0.0f, 0.0f);
        }
        else
        {
            jitterTimer = jitterDuration;
            float x = hand.localEulerAngles.x;
            hand.localEulerAngles = new Vector3(x + (float)SnapAngle, 0.0f, 0.0f);
            return true;
        }
        return false;
    }
}
