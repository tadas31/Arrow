using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private Touch initialTouch = new Touch();
    private float distance = 0;
    private bool swipedSideways;
    private float deltaX;
    private float deltaY;

    ParticleSystem trail;

    // Use this for initialization
    void Start () {
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

    public void OnStart()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnStore()
    {
        SceneManager.LoadScene("Store");
    }
}
