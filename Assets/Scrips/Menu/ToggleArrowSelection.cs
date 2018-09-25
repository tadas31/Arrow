using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleArrowSelection : MonoBehaviour {

    private Toggle[] allArrowTogles;
    private string currentArrow;

    // Use this for initialization
    void Start () {
        allArrowTogles = FindObjectsOfType<Toggle>();
        currentArrow = SaveManager.Instance.GetArrowSprite();
    }
	
	// Update is called once per frame
	void Update () {
        if (currentArrow != SaveManager.Instance.GetArrowSprite())
        {
            currentArrow = SaveManager.Instance.GetArrowSprite();
            for (int i = 0; i < allArrowTogles.Length; i++)
            {
                Debug.Log("u Work?");
                if (allArrowTogles[i].name == SaveManager.Instance.GetArrowSprite())
                    allArrowTogles[i].isOn = true;
                else
                    allArrowTogles[i].isOn = false;

            }

        }
    }
}
