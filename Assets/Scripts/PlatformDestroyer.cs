using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour {

    public GameObject thePlatformDestructionPoint;

    void Start()
    {
        thePlatformDestructionPoint = GameObject.FindWithTag("PlatformDestructionPoint");
    }
	
    void Update () {

        if (transform.position.x < thePlatformDestructionPoint.transform.position.x)
            {
                gameObject.SetActive(false);
            }   
	}
}
