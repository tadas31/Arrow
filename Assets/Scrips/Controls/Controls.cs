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
        if(Input.GetMouseButtonDown(0))
        {
            start = Input.mousePosition;

            start = Camera.main.ScreenToWorldPoint(start);
        }

        if (Input.GetMouseButtonUp(0))
        {
            end = Input.mousePosition;

            end = Camera.main.ScreenToWorldPoint(end);

            Debug.DrawLine(start, end, Color.red);

        }
    }
}
