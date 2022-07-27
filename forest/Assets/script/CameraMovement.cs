using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed;
    public float scrollSpeed;
    // Start is called before the first frame update
    void Start()
    {
        // make 60 fps 
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {

        //move camera horizontal and verical (WASD)
        gameObject.transform.Translate(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed, 0);
        //move camera forward and back (scroll)
        gameObject.transform.Translate(Vector3.forward* Input.GetAxis("Mouse ScrollWheel")* scrollSpeed);
    }
}
