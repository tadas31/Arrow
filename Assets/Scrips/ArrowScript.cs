using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowScript : MonoBehaviour {
    public int position;
    private SpriteRenderer spriteRenderer;
    private Image timer;
    public Sprite sprite;
    float timeAmt = 3f;
    float time;
    //public GameObject arrow;
	// Use this for initialization
	void Start () {
        position = Random.Range(1, 8);
        SetArrowPosition();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        timer = GetComponentInChildren<Image>();
        time = timeAmt;
	}
    // Update is called once per frame
	void Update () {
        if (time > 0)
        {
            time -= Time.deltaTime;
            timer.fillAmount = time / timeAmt;
        }
	}

    private void SetArrowPosition()/// Sets the arrow key to a specific position
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
	
}
