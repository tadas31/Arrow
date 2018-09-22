using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    private bool isColorLeapOver;
    private SpriteRenderer background;
    private float lerpTime = 20.0f;
    private List<Color> colors;

    private Touch initialTouch = new Touch();
    private float distance = 0;
    private bool swipedSideways;
    private float deltaX;
    private float deltaY;

    private ParticleSystem trail;
    private Text scoreText;

    // Use this for initialization
    void Start () {
        isColorLeapOver = true;
        colors = new List<Color>() { Color.yellow, Color.red, new Color(0.9905f, 0.4065f, 0.8920f), Color.blue, new Color(0.4078f, 0.9921f, 0.9537f), Color.green, Color.yellow };
        background = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        //trail = GameObject.Find("1").GetComponent<ParticleSystem>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        //trail.Stop();
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "Max score - " + SaveManager.Instance.GetScore();
        if (isColorLeapOver)
            StartCoroutine(ColorLerp());
    }

    
    private void FixedUpdate()
    {

        foreach (Touch t in Input.touches)
        {
            trail.transform.position = Camera.main.ScreenToWorldPoint(t.position);     //draws trail
            if (t.phase == TouchPhase.Began)
            {
                //trail.Play();
                initialTouch = t;
            }
            else if (t.phase == TouchPhase.Moved)
            {
                deltaX = initialTouch.position.x - t.position.x;
                deltaY = initialTouch.position.y - t.position.y;
                distance = Mathf.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                swipedSideways = Mathf.Abs(deltaX) > Mathf.Abs(deltaY);
            }
            else if (t.phase == TouchPhase.Ended)
            {
                if (distance > 100f)
                {
                    if (swipedSideways && deltaX > 0) //swiped left
                    {

                    }
                    else if (swipedSideways && deltaX <= 0) // swiped right
                    {

                    }
                    else if (!swipedSideways && deltaY > 0) //swiped down
                    {
                        SceneManager.LoadScene("Store");
                    }
                    else if (!swipedSideways && deltaY <= 0) //swiped up
                    {
                        SceneManager.LoadScene("Game");
                    }
                }
                initialTouch = new Touch();
                trail.Stop();
            }
        }
    }

    IEnumerator ColorLerp()
    {
        isColorLeapOver = false;

        if (colors.Count >= 2)
        {
            for (int i = 1; i < colors.Count; i++)
            {
                float startTime = Time.time;
                float percentageComplete = 0;

                while (percentageComplete < 1)
                {
                    float elapsedTime = Time.time - startTime;
                    percentageComplete = elapsedTime / (lerpTime / (colors.Count - 1));
                    background.color = Color.Lerp(colors[i - 1], colors[i], percentageComplete);
                    yield return null;
                }
            }
        }
        isColorLeapOver = true;
    }

    public void OnStart()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnStore()
    {
        SceneManager.LoadScene("Store");
    }

    
}
