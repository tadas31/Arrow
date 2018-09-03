using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {
    private int position;
    public GameObject arrow;
	// Use this for initialization
	void Start () {
        position = Random.Range(1, 8);
	}


    private void SetArrowPosition()/// Sets the arrow key to a specific position
    {
        //Rotates the arrow
        switch (position) {
            case 1:
                arrow.transform.rotation.y
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
        }
        
            
    }
	// Update is called once per frame
	void Update () {
		
	}
}
