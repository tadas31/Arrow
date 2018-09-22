using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {

    private GameObject trailLoad;
    private GameObject trail;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        trailLoad = Resources.Load<GameObject>("Trails/" + SaveManager.Instance.GetTrail());
        Instantiate(trailLoad, Vector3.zero, Quaternion.identity);

        
        DontDestroyOnLoad(GameObject.Find(SaveManager.Instance.GetTrail() + "(Clone)"));
        trail = GameObject.Find(SaveManager.Instance.GetTrail() + "(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        if (trail.name != SaveManager.Instance.GetTrail() + "(Clone)")
        {
            Destroy(trail);
            trailLoad = Resources.Load<GameObject>("Trails/" + SaveManager.Instance.GetTrail());
            Instantiate(trailLoad, Vector3.zero, new Quaternion(0, 0, 0, 0));
            DontDestroyOnLoad(GameObject.Find(SaveManager.Instance.GetTrail() + "(Clone)"));
        }

        if (Input.GetMouseButton(0))
        {
            trail.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            trail.GetComponent<ParticleSystem>().Play();
            trail.GetComponent<ParticleSystem>().Play();
            Debug.Log(trail.GetComponent<ParticleSystem>().isPlaying);
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    start = Input.mousePosition;
        //    start = Camera.main.ScreenToWorldPoint(start).normalized;
        //    trail.Play();
        //}
        //else
        //{
        //    trail.GetComponent<ParticleSystem>().Pause();
        //}
    }
}
