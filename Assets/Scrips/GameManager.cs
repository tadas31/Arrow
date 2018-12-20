using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private Text scoreText;
    private Text levelText;
    private int score = 0;
    private int level = 0;

    // Use this for initialization
    void Start () {
        gameOverScreen = GameObject.Find("GameOverScreenCanvas").GetComponent<Canvas>();
        gameOverScreen.gameObject.SetActive(false);
        controlsScript = controls.GetComponent<Controls>();
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        ArrowInstanisation();
        arrowScript = arrows[currentArrow].GetComponent<ArrowScript>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();
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
           
            score = score + (int)(100f * arrowScript.GetTime());//Change here if i feel like it
            scoreText.text = score.ToString();

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
            GameOver();
        }
        
    }

    /// <summary>
    /// Destroys all of the arrows in the array
    /// </summary>
    public void DestroyArrows()
    {
        if (arrows != null)
        {
            for (int i = 0; i < arrows.Length; i++)
            {
                arrows[i].GetComponent<ArrowScript>().Destroy();
            }
        }
    }

    public void GameOver() {
        if (!gameOverScreen.gameObject.activeSelf)
        {
            if (SaveManager.Instance.GetScore() < score)
                SaveManager.Instance.SetMaxScore(score);
            gameOverScreen.gameObject.SetActive(true);
            DestroyArrows();
            arrowScript = null; 
            //PlayAdScript.ShowAd();
        }
        
    }

    /// <summary>
    /// Creates the arrows
    /// </summary>
    public void ArrowInstanisation()
    {
        StartCoroutine(LevelText(2f, levelText));
        DestroyArrows();
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
            arrows[i].GetComponent<ArrowScript>().StartTimer();
        }
    }


    public IEnumerator LevelText(float time, Text text) {
        Debug.Log("Corountine started");
        text.text = "Level " + ++level;
        StartCoroutine(FadeTextToFullAlpha(time/3, text));
        yield return new WaitForSeconds(time/3);
        StartCoroutine(FadeTextToZeroAlpha(time / 3, text));
    }

    /// <summary>
    /// Let the text apear
    /// </summary>
    /// <param name="t">Time to appear</param>
    /// <param name="i">Text component</param>
    /// <returns>Null</returns>
    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    /// <summary>
    /// Let the text disapear
    /// </summary>
    /// <param name="t">Time to disappear</param>
    /// <param name="i">Text component</param>
    /// <returns>Null</returns>
    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
