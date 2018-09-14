using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Store : MonoBehaviour {

    private Touch initialTouch = new Touch();
    private float distance = 0;
    private bool swipedSideways;
    private float deltaX;
    private float deltaY;
    private Transform maxHeight;
    private bool navigation;                //if true then uses tahes user to menu or game else navigates store

    ParticleSystem trail;

    // Use this for initialization
    void Start () {
        navigation = false;
        maxHeight = GameObject.Find("MaxNavigationHeight").GetComponent<Transform>();
        trail = GameObject.Find("DefaultTrail").GetComponent<ParticleSystem>();
        trail.Stop();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {

        foreach (Touch t in Input.touches)
        {
            if (t.position.y <= maxHeight.position.y && !navigation)
                navigation = true;

            trail.transform.position = Camera.main.ScreenToWorldPoint(t.position);     //draws trail
            if (t.phase == TouchPhase.Began)
            {
                trail.Play();
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
                        if (navigation)
                        {
                            SceneManager.LoadScene("MainMenu");
                            navigation = false;
                        }
                    }
                    else if (swipedSideways && deltaX <= 0) // swiped right
                    {
                        if (navigation)
                        {
                            SceneManager.LoadScene("Game");
                            navigation = false;
                        }
                            
                    }
                    else if (!swipedSideways && deltaY > 0) //swiped down
                    {

                    }
                    else if (!swipedSideways && deltaY <= 0) //swiped up
                    {

                    }
                }
                initialTouch = new Touch();
                trail.Stop();
            }
        }
    }

}
