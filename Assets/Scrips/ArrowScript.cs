﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//Delete when not in use
using UnityEngine.UI;

public class ArrowScript : MonoBehaviour {
    public int position;
    private SpriteRenderer spriteRenderer;
    private Image timer;//Timer component
    public Sprite sprite;

    float timeAmt = 4f;//Time for the timer
    float time;


    //Floats for sprite change
    static float spriteTimeAmt = 0.5f;
    float spriteTime = spriteTimeAmt;
    
    //For changing the sprites
    private Color transparent = new Color(1f, 1f, 1f, 0f);
    private Color visible = new Color(1f, 1f, 1f, 1f);

    bool stop = false;
    // Use this for initialization
    void Start () {
        position = Random.Range(1, 8);
        SetArrowPosition();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = GetComponentInChildren<Image>();
	}
    // Update is called once per frame
	void Update () {
        if (time > 0 && !stop)
        {
            TimerChange();
        }
        else
        {
            //If the time ends(Change here)
            SceneManager.LoadScene("MainMenu");
        }
        //If it is stopped in between the change
        if (spriteTime > 0 && time < (timeAmt + spriteTimeAmt) / 2 && time > (timeAmt - spriteTimeAmt) / 2 && stop)
        {
            ChangeSprite();
        }
    }

    /// <summary>
    /// Changes the sprite form arrow to not visable conponent
    /// </summary>
    private void ChangeSprite()
    {
        spriteTime -= Time.deltaTime;
        float change = spriteTime / spriteTimeAmt;

        if (change < 0.5f)
        {
            spriteRenderer.sprite = sprite;//Changes the sprite from an arrow to a circle (If lag occur could be added an bool to shop the change of a sprite)
        }

        spriteRenderer.color = Color.Lerp(transparent, visible, Mathf.Abs(2 * change - 1f));//Makes the fade of the arrow to a circle
    }

    /// <summary>
    /// Controls the change of the circle timer around the arrow
    /// </summary>
    private void TimerChange()
    {
        time -= Time.deltaTime;
        float change = time / timeAmt;
        timer.fillAmount = change;//Changes the fill amount of the circle
        timer.color = Color.Lerp(Color.red, Color.green, change);//Changes the colours of the circle
        
        if (time < (timeAmt+spriteTimeAmt) / 2 && time > (timeAmt - spriteTimeAmt) / 2)
        {
            ChangeSprite();
        }   
    }

    /// <summary>
    /// Sets the arrow key to a specific position
    /// </summary>
    private void SetArrowPosition()
    {
        RectTransform timerTransform = GetComponentInChildren<RectTransform>();
        //Rotates the arrow
        switch (position) {
            case 1:
                transform.Rotate(new Vector3(0,0,0)) ;
                timerTransform.Rotate(new Vector3(0, 0, 0));
                break;
            case 2:
                transform.Rotate(new Vector3(0, 0, 45));
                timerTransform.Rotate(new Vector3(0, 0, -45));
                break;
            case 3:
                transform.Rotate(new Vector3(0, 0, 90));
                timerTransform.Rotate(new Vector3(0, 0, -90));
                break;
            case 4:
                transform.Rotate(new Vector3(0, 0,135));
                timerTransform.Rotate(new Vector3(0, 0, -135));
                break;
            case 5:
                transform.Rotate(new Vector3(0, 0, 180));
                timerTransform.Rotate(new Vector3(0, 0, -180));
                break;
            case 6:
                transform.Rotate(new Vector3(0, 0, 225));
                timerTransform.Rotate(new Vector3(0, 0, -225));
                break;
            case 7:
                transform.Rotate(new Vector3(0, 0, 270));
                timerTransform.Rotate(new Vector3(0, 0, -270));
                break;
            case 8:
                transform.Rotate(new Vector3(0, 0, 315));
                timerTransform.Rotate(new Vector3(0, 0, -315));
                break;
        }      
    }
    /// <summary>
    /// Sets the time of the arrows livespan
    /// </summary>
    /// <param name="time">The time for arrow to be active</param>
    public void SetTimer(float time)
    {
        timeAmt = time;
        this.time = time;
    }

    /// <summary>
    /// Stops the timer
    /// </summary>
    public void StopTimer()
    {
        stop = true;
    }
}
