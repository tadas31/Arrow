using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    Vector3 start = Vector3.zero;
    Vector3 end = Vector3.zero;
    ParticleSystem trail;

    // Use this for initialization
    void Start () {
		trail = GameObject.Find("Trail").GetComponent< ParticleSystem>();
        trail.Stop();
    }
	
	// Update is called once per frame
	void Update () {

        //gets first point
        if(Input.GetMouseButtonDown(0))
        {
            start = Input.mousePosition;
            start = Camera.main.ScreenToWorldPoint(start).normalized;
            trail.Play();
        }

        trail.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);     //draws trail

        //gets last point
        if (Input.GetMouseButtonUp(0))
        {
            end = Input.mousePosition;
            end = Camera.main.ScreenToWorldPoint(end).normalized;
            trail.Stop();
        }
        
        float angle = Vector2.SignedAngle(start - end, Vector3.down);   //gets angle of line

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
