using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {
    public int position;
    private SpriteRenderer spriteRenderer;
    public Sprite sprite;
    //public GameObject arrow;
	// Use this for initialization
	void Start () {
        position = Random.Range(1, 8);
        SetArrowPosition();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
	}


    private void SetArrowPosition()/// Sets the arrow key to a specific position
    {
        //Rotates the arrow
        switch (position) {
            case 1:
                transform.Rotate(new Vector3(0,0,0)) ;
                break;
            case 2:
                transform.Rotate(new Vector3(0, 0, 45));
                break;
            case 3:
                transform.Rotate(new Vector3(0, 0, 90));
                break;
            case 4:
                transform.Rotate(new Vector3(0, 0,135));
                break;
            case 5:
                transform.Rotate(new Vector3(0, 0, 180));
                break;
            case 6:
                transform.Rotate(new Vector3(0, 0, 225));
                break;
            case 7:
                transform.Rotate(new Vector3(0, 0, 270));
                break;
            case 8:
                transform.Rotate(new Vector3(0, 0, 315));
                break;
        }
        
            
    }
	// Update is called once per frame
	void Update () {
		
	}
}
