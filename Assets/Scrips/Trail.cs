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
            trail = GameObject.Find(SaveManager.Instance.GetTrail() + "(Clone)");
        }

        if (Input.GetMouseButtonDown(0))
            trail.GetComponent<ParticleSystem>().Play();

        if (Input.GetMouseButton(0))
            trail.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
