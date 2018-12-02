using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticlesOnDeath : MonoBehaviour
{
    public GameObject ParticlesBone;
    public GameObject ParticlesPrefab;


    /// <summary>
    /// When this thing dies, enable some particles.
    /// </summary>
    void OnDestroy()
    {
        var go = GameObject.Instantiate(ParticlesPrefab);
        go.transform.position = ParticlesBone.transform.position;
    }
}
