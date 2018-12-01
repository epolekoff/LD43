﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Destroy the other object.
    /// </summary>
    public void OnTriggerEnter(Collider col)
    {
        Destroy(col.transform.gameObject);
    }
}