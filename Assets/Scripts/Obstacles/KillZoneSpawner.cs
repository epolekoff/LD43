using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneSpawner : MonoBehaviour
{
    public List<GameObject> KillZonePrefabs;
    public float SpawnHeight;

    private const float TimerMin = 1f;
    private const float TimerMax = 6f;
    private float Timer = TimerMin;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(GameManager.Instance.GameActive)
        {
            UpdateSpawnKillZone();
        }
    }

    /// <summary>
    /// Keep spawning them.
    /// </summary>
    public void UpdateSpawnKillZone()
    {
        Timer -= Time.deltaTime;

        if(Timer <= 0)
        {
            SpawnKillZone();
            Timer = Random.Range(TimerMin, TimerMax);
        }
    }

    /// <summary>
    /// Create a kill zone.
    /// </summary>
    public void SpawnKillZone()
    {
        // Pick a prefab to spawn
        int prefabIndex = Random.Range(0, KillZonePrefabs.Count);
        var go = GameObject.Instantiate(KillZonePrefabs[prefabIndex]);
        
        // Pick a position in range.
        go.transform.position = new Vector3(
            Random.Range(GameManager.Instance.MinX, GameManager.Instance.MaxX), 
            SpawnHeight, 
            Random.Range(GameManager.Instance.MinY, GameManager.Instance.MaxY));
    }
}
