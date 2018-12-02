using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticlesOnDeath : MonoBehaviour
{
    public GameObject ParticlesBone;
    public GameObject ParticlesPrefab;

    /// <summary>
    /// Die
    /// </summary>
    public void Die()
    {
        var go = GameObject.Instantiate(ParticlesPrefab);
        go.transform.position = ParticlesBone.transform.position;
    }
}
