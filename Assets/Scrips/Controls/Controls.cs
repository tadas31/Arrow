using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {


    Vector3 start = Vector3.zero;
    Vector3 end = Vector3.zero;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //gets first point
        if(Input.GetMouseButtonDown(0))
        {
            start = Input.mousePosition;
            start = Camera.main.ScreenToWorldPoint(start).normalized;
        }

        //gets last point
        if (Input.GetMouseButtonUp(0))
        {
            end = Input.mousePosition;
            end = Camera.main.ScreenToWorldPoint(end).normalized;
        }

        //draws lines
        Debug.DrawLine(Vector3.zero, Vector3.up, Color.blue);
        Debug.DrawLine(Vector3.zero, Vector3.down, Color.blue);
        Debug.DrawLine(start, end, Color.red);

        //gets angle of line
        float angle = Vector2.SignedAngle(start - end, Vector3.down);


        Debug.Log("start   " + start + "  end  " + end);
        Debug.Log("Kampas   " + angle);


        //determines which arrow was selected
        if (angle > -22.5 && angle < 22.5)
            Debug.Log("up");
        else if (angle > 22.5 && angle < 67.5)
            Debug.Log("Right-up");
        else if (angle > 67.5 && angle < 112.5)
            Debug.Log("Right");
        else if (angle > 112.5 && angle < 157.5)
            Debug.Log("Right-down");
        else if ((angle > 157.5 && angle < 180) || (angle > -180 && angle < -157.5))
            Debug.Log("Down");
        else if (angle > -157.5 && angle < -112.5)
            Debug.Log("Left-down");
        else if (angle > -112.5 && angle < -67.5)
            Debug.Log("Left");
        else if (angle > -67.5 && angle < -22.5)
            Debug.Log("Left-up");
    }
}
