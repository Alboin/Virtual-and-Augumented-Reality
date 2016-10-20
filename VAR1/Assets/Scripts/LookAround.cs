using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class LookAround : MonoBehaviour {

    private float movement_speed;
    private float turn_speed;
    private Vector3 movement;
    private float x_angle;
    private float y_angle;
    private Quaternion rotation;
    private float interaction_range;

    int nTextures;
    int arrayPos = 0;

    private Light flash;

    private GameObject cylinder;

    Camera cam;

	// Use this for initialization
	void Start () {

        Cursor.visible = false;

        movement_speed = 3f;
        turn_speed = 5.0f;

        cylinder = GameObject.Find("PlayerCylinder");
   
        cam = cylinder.GetComponent<Camera>();
        interaction_range = 3.5f;

        flash = cam.GetComponent<Light>();

        nTextures = 3;
        GameObject.Find("Sphere1").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Sphere2").GetComponent<MeshRenderer>().enabled = false;

        GameObject.Find("Light_yellow").GetComponent<Light>().color = Color.yellow;
        GameObject.Find("Light_cyan").GetComponent<Light>().color = Color.cyan;
        GameObject.Find("Light_magenta").GetComponent<Light>().color = Color.magenta;

    }

    void FixedUpdate ()
    {
        // Read the user input
        var x = CrossPlatformInputManager.GetAxis("Mouse X");
        var y = CrossPlatformInputManager.GetAxis("Mouse Y");

        // Adjust the look angle by an amount proportional to the turn speed and horizontal input.
        x_angle -= x * turn_speed;
        y_angle -= y * turn_speed;
        // Rotate the rig (the root object) around Y axis only:
        rotation = Quaternion.Euler(y_angle, -x_angle, 0f);

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement = Quaternion.Euler(0, -x_angle, 0) * movement;
    }

    // Update is called once per frame
    void Update () {
	
	}

    void LateUpdate ()
    {
        cylinder.GetComponent<Rigidbody>().velocity = movement * movement_speed;
        cylinder.transform.position = new Vector3(cylinder.transform.position.x, 1, cylinder.transform.position.z);

        //transform.position = transform.position + movement * movement_speed;
        transform.rotation = rotation;

        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit_object;
        if(Input.GetMouseButton(0))
        {
            if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit_object, interaction_range))
            {
                if(hit_object.collider.gameObject.tag == "Movable")
                {
                    //If we hit something
                    float scale = hit_object.transform.localScale.z;
                    hit_object.transform.position = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1.0f + scale / 1.5f));
                }
            }
            else
            {
                //If we don't hit anything
            }
        }
        if (Input.GetMouseButton(1))
        {
            if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit_object, interaction_range))
            {
                if (hit_object.collider.gameObject.tag == "Movable")
                {
                    hit_object.transform.eulerAngles += new Vector3(0, 1, 0);
                    if (hit_object.transform.eulerAngles.y > 358)
                        hit_object.transform.eulerAngles = new Vector3(0, 0, 0);
                }

            }
        }
        if (Input.GetMouseButton(2))
        {
            if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit_object, interaction_range))
            {
                if (hit_object.collider.gameObject.tag == "Movable")
                    hit_object.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit_object, interaction_range))
            {
                if (hit_object.collider.gameObject.name == "Button_green")
                {
                    float intens = 0;
                    if (GameObject.Find("Light_yellow").GetComponent<Light>().intensity < 0.1)
                        intens = 0.6f;
                    GameObject.Find("Light_yellow").GetComponent<Light>().intensity = intens;
                    GameObject.Find("Light_cyan").GetComponent<Light>().intensity = intens * 0.5f;
                    GameObject.Find("Light_magenta").GetComponent<Light>().intensity = intens;
                }
                if (hit_object.collider.gameObject.name == "Button_red")
                {
                    Color col1 = Color.yellow;
                    Color col2 = Color.cyan;
                    Color col3 = Color.magenta;

                    if(GameObject.Find("Light_yellow").GetComponent<Light>().color.b < 1.0f)
                    {
                        col1 = Color.white;
                        col2 = Color.white;
                        col3 = Color.white;
                    }
                    else
                    {
                        col1 = Color.yellow;
                        col2 = Color.cyan;
                        col3 = Color.magenta;
                    }
                    GameObject.Find("Light_yellow").GetComponent<Light>().color = col1;
                    GameObject.Find("Light_cyan").GetComponent<Light>().color = col2;
                    GameObject.Find("Light_magenta").GetComponent<Light>().color = col3;
                }
            }

        }




        if (Input.GetButtonDown("flashlight_off"))
        {
            if (flash.intensity < 0.1)
                flash.intensity = 1;
            else
                flash.intensity = 0;
        }
        //Change red
        if (Input.GetButton("red") && Input.GetAxisRaw("red") > 0)
            flash.color = new Color(flash.color.r + 0.05f, flash.color.g, flash.color.b, 1); 
        if (Input.GetButton("red") && Input.GetAxisRaw("red") < 0)
            flash.color = new Color(flash.color.r - 0.05f, flash.color.g, flash.color.b, 1);
        //Change green
        if (Input.GetButton("green") && Input.GetAxisRaw("green") > 0)
            flash.color = new Color(flash.color.r, flash.color.g + 0.05f, flash.color.b, 1);
        if (Input.GetButton("green") && Input.GetAxisRaw("green") < 0)
            flash.color = new Color(flash.color.r, flash.color.g - 0.05f, flash.color.b, 1);
        //Change blue
        if (Input.GetButton("blue") && Input.GetAxisRaw("blue") > 0)
            flash.color = new Color(flash.color.r, flash.color.g, flash.color.b + 0.05f, 1);
        if (Input.GetButton("blue") && Input.GetAxisRaw("blue") < 0)
            flash.color = new Color(flash.color.r, flash.color.g, flash.color.b - 0.05f, 1);

        if (Input.GetButtonDown("change_material"))
        {
            if (arrayPos == nTextures - 1)
            {
                arrayPos = 0;
            }
            else
            {
                arrayPos++;
            }
            if(arrayPos == 0)
            {
                GameObject.Find("Sphere").GetComponent<MeshRenderer>().enabled = true;
                GameObject.Find("Sphere1").GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("Sphere2").GetComponent<MeshRenderer>().enabled = false;
            }
            else if(arrayPos == 1)
            {
                GameObject.Find("Sphere").GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("Sphere1").GetComponent<MeshRenderer>().enabled = true;
                GameObject.Find("Sphere2").GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                GameObject.Find("Sphere").GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("Sphere1").GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("Sphere2").GetComponent<MeshRenderer>().enabled = true;
            }
        }

    }
}
