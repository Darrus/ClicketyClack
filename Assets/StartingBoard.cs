using UnityEngine;

public class StartingBoard : MonoBehaviour {
    private void Update()
    {
        if(Room.Instance.done)
        {
            gameObject.SetActive(true);
        }
    }
}
