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
        PlayParticlesOnDeath script = col.transform.GetComponent<PlayParticlesOnDeath>();
        if (script != null)
        {
            script.Die();
        }

        var diamond = col.transform.GetComponent<DiamondUnit>();
        if (diamond != null && diamond.enabled)
        {
            GameManager.Instance.AddUnits(30, 1);

            // Play a powerup sound
            AudioManager.Instance.PlaySound(AudioManager.Instance.GoldenDeathSound);
        }
        else
        {
            // Play a death sound
            if(Random.Range(0, 100) < 10)
            {
                AudioManager.Instance.PlayDeathVoice();
            }
        }

        Destroy(col.transform.gameObject);
    }
}
