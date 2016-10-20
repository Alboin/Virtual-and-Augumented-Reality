using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Movement : MonoBehaviour {

    public float speed;

    private Rigidbody sphere;
    private int points;

    void Start ()
    {
        points = 0;
        sphere.GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        sphere.AddForce(movement * speed);
	}
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick_up"))
        {
            points++;
            other.gameObject.SetActive(false);
        }
    }
    //    Destroy(other.gameObject);

}
