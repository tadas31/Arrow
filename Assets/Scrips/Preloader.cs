using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour
{
    private CanvasGroup fadeGroup;
    private float loadTime;
    private float minimumLogoTime = 3.0f;       //minimum time of that scene

    // Use this for initialization
    private void Start()
    {
        //grab CanvasGroup
        fadeGroup = FindObjectOfType<CanvasGroup>();

        //starts with wite Screen
        fadeGroup.alpha = 1;

        //preloade game

        if (Time.time < minimumLogoTime)
            loadTime = minimumLogoTime;
        else
            loadTime = Time.time;
    }

    // Update is called once per frame
    private void Update()
    {
        // Fade in
        if (Time.time < minimumLogoTime)
        {
            fadeGroup.alpha = 1 - Time.time;
        }

        //Fade out
        if (Time.time > minimumLogoTime && loadTime != 0)
        {
            fadeGroup.alpha = Time.time - minimumLogoTime;
            if (fadeGroup.alpha >= 1)
            {
                // Cahnge the scene
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
