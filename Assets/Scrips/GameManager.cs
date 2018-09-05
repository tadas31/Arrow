using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject mainArrow;
    public GameObject controls;
    ArrowScript arrowScript;
    Controls controlsScript;

    GameObject[] arrows;//The massive in which all of the arrows is stored
    public int amountOfArrows = 3;// Change it later from public
    float arrowHeigth = (5f * 3f) / 8f;//The height at witch the arrows are set

    // Use this for initialization
    void Start () {
        controlsScript = controls.GetComponent<Controls>();
        GameObject arr = Instantiate(mainArrow, new Vector2(1f,5f),Quaternion.identity);
        arrowScript = arr.GetComponent<ArrowScript>();
        ArrowInstanisation();
    }
	
	// Update is called once per frame
	void Update () {
	}

    /// <summary>
    /// For testing purposes
    /// </summary>
    public void ChangeInDraw()
    {
        if (arrowScript.position == controlsScript.dragPosition)
        {
            Debug.Log("Same position");
        }
        else {
            Debug.Log("Difrent");
        }
        
    }

    /// <summary>
    /// Creates the arrows
    /// </summary>
    public void ArrowInstanisation()
    {
        //Calculating all of the width of the camera (5f is the camera size 2f is to both sides like from - 2.08 to 2.08)
        float cameraWidth = Camera.main.aspect * 5f *2f;
        arrows = new GameObject[amountOfArrows];
        for (int i = 0; i < amountOfArrows; i++)
        {
            //Sets it to the right position
            arrows[i] = Instantiate(mainArrow, new Vector2((cameraWidth/amountOfArrows*i) + (cameraWidth/ amountOfArrows / 2f) - cameraWidth/2f, arrowHeigth), Quaternion.identity);
        }
    }
}
