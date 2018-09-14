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

    private Canvas gameOverScreen;

    // Use this for initialization
    void Start () {
        gameOverScreen = GameObject.Find("GameOverScreenCanvas").GetComponent<Canvas>();
        gameOverScreen.gameObject.SetActive(false);
        controlsScript = controls.GetComponent<Controls>();
        ArrowInstanisation();
        arrowScript = arrows[currentArrow].GetComponent<ArrowScript>();
    }
	
	// Update is called once per frame
	void Update () {
	}

    /// <summary>
    /// Checks if the drag position was right
    /// </summary>
    public void ChangeInDraw()
    {
        if (arrowScript.position == controlsScript.dragPosition)
        {
            if (currentArrow == amountOfArrows - 1)
            {
                //If it's was the last arrow what to do
                amountOfArrows++;
                currentArrow = 0;
                ArrowInstanisation();
                arrowScript = arrows[currentArrow].GetComponent<ArrowScript>();
            }
            else
            {
                //If it's a normal arrow
                arrowScript.StopTimer();
                arrowScript = arrows[++currentArrow].GetComponent<ArrowScript>();
            }
            
        }
        else {
            //If the direction of the drag is in the diffrent side than it was pointing
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
        float time = 3f;
        for (int i = 0; i < amountOfArrows; i++)
        {
            //Sets it to the right position
            arrows[i] = Instantiate(mainArrow, new Vector2((cameraWidth/amountOfArrows*i) + (cameraWidth/ amountOfArrows / 2f) - cameraWidth/2f, arrowHeigth), Quaternion.identity);
            //Scales down the arrows that all of them would fit
            arrows[i].transform.localScale = new Vector2(scale,scale);
            arrows[i].GetComponent<ArrowScript>().SetTimer(time+(0.5f*i));//Sets the time for every arrow
        }
    }
}
