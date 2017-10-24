using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSC : MonoBehaviour {

    public GameObject smoke;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0.01f,0), ForceMode.Impulse);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Floor")
        {
            GameObject newSmoke = Instantiate(smoke, transform.position, Quaternion.identity);
            GameObject.Destroy(gameObject);
        }
    }
}
