using UnityEngine;
using System.Collections;

public class cuserinteraction : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
            transform.Rotate(new Vector3(15, 30, 45) *3* Time.deltaTime);

    }
}
