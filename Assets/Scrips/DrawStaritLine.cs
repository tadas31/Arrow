using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawStaritLine : MonoBehaviour {

    public Transform trail;
    float i = -14;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        trail.position = new Vector2(i, 0);
        i += 0.2f;
	}
}
