using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondUnit : MonoBehaviour
{

    public GameObject DiamondParticles;

	void OnEnable()
    {
        DiamondParticles.SetActive(true);
    }
}
