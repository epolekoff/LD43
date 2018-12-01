using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [Header("Bounds")]
    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;
    public float SpawnHeight;

    [Header("Unit Spawning")]
    public GameObject UnitPrefab;
    public int NumUnitsToSpawn;

    [Header("Line Drawing")]
    public Camera GameCamera;
    public GameObject LinePrefab;

    // Use this for initialization
    void Start ()
    {
        AddUnits(NumUnitsToSpawn);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Spawn a bunch of units.
    /// </summary>
    private void AddUnits(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            // Spawn the unit at a random position.
            Vector3 position = new Vector3(Random.Range(MinX, MaxX), SpawnHeight, Random.Range(MinY, MaxY));
            var go = GameObject.Instantiate(UnitPrefab);
            go.transform.position = position;
        }
    }
}
