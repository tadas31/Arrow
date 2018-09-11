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
    int currentArrow = 0;
    float arrowHeigth = (5f * 3f) / 8f;//The height at witch the arrows are set

    // Use this for initialization
    void Start () {
        controlsScript = controls.GetComponent<Controls>();
        ArrowInstanisation();
        arrowScript = arrows[currentArrow].GetComponent<ArrowScript>();
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
            if (currentArrow == amountOfArrows - 1)
            {
                amountOfArrows++;
                currentArrow = 0;
                ArrowInstanisation();
                arrowScript = arrows[currentArrow].GetComponent<ArrowScript>();
            }
            else
                arrowScript = arrows[++currentArrow].GetComponent<ArrowScript>();
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
        if (arrows != null)
        {
            for (int i = 0; i < arrows.Length; i++)
            {
                Destroy(arrows[i]);
            }
        }
        //Calculating all of the width of the camera (5f is the camera size 2f is to both sides like from - 2.08 to 2.08)
        float cameraWidth = Camera.main.aspect * 5f *2f;
        float scale = cameraWidth / (2.5f * amountOfArrows);//Change 2.5f if your arrow size is diffrent
        arrows = new GameObject[amountOfArrows];
        for (int i = 0; i < amountOfArrows; i++)
        {
            //Sets it to the right position
            arrows[i] = Instantiate(mainArrow, new Vector2((cameraWidth/amountOfArrows*i) + (cameraWidth/ amountOfArrows / 2f) - cameraWidth/2f, arrowHeigth), Quaternion.identity);
            //Scales down the arrows that all of them would fit
            arrows[i].transform.localScale = new Vector2(scale,scale);
        }
    }
}
