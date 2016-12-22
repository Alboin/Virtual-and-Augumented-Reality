using UnityEngine;
using System.Collections;

public class orbit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 45, 0) * 3 * Time.deltaTime);

        //transform.RotateAround(new Vector3(0.1f,0,0), new Vector3(0,1,0), Time.deltaTime * 5);

    }
}
