using UnityEngine;

public class StartingBoard : MonoBehaviour {
    private void Update()
    {
        if(Room.Instance.done)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
