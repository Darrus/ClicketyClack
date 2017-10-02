using System.Collections;

public interface ISlotEvent {
    bool UpdateEvent();
}

public enum SLOT_EVENTS
{
    SNAP_UP,
    SNAP_DOWN,
    ROTATE_180,
    MAX,
};