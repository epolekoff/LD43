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
    private readonly string[] StartingAnimationStates = { "Square", "Circle", "Triangle" };

    [Header("Line Drawing")]
    public Camera GameCamera;
    public GameObject LinePrefab;


    [Header("Game State")]
    public bool GameActive = true;
    public float GameTime = 60f;
    private float m_gameTimer;
    public TMPro.TextMeshProUGUI GameTimerText;

    [Header("Canvas")]
    public GameObject TitleScreenCanvas;
    public EndGameCanvas EndGameCanvas;
    public GameObject GameCanvas;

    [Header("Audio")]
    public AudioSource GameMusicSource;
    public AudioSource MenuMusicSource;

    // Use this for initialization
    void Start ()
    {
        m_gameTimer = GameTime;
        GameActive = false;

        GameCanvas.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateGameTimer();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    /// <summary>
    /// Spawn a bunch of units.
    /// </summary>
    public void AddUnits(int amount, int numDiamonds = 2)
    {
        for(int i = 0; i < amount - numDiamonds; i++)
        {
            SpawnUnit(StartingAnimationStates[Random.Range(0, StartingAnimationStates.Length)]);
        }

        // Spawn 2 goldens
        for(int i = 0; i < numDiamonds; i++)
        {
            SpawnUnit("Diamond");
        }
    }

    private void SpawnUnit(string unitType)
    {
        // Spawn the unit at a random position.
        Vector3 position = new Vector3(Random.Range(MinX, MaxX), SpawnHeight, Random.Range(MinY, MaxY));
        var go = GameObject.Instantiate(UnitPrefab);
        go.transform.position = position;

        // Set the starting animation for the sprites.
        go.GetComponentInChildren<Animator>().SetTrigger(unitType);
        if(unitType == "Diamond")
        {
            go.GetComponent<DiamondUnit>().enabled = true;
        }
    }

    /// <summary>
    /// Game time/state.
    /// </summary>
    private void UpdateGameTimer()
    {
        if(!GameActive)
        {
            return;
        }

        m_gameTimer -= Time.deltaTime;
        GameTimerText.text = Mathf.CeilToInt(m_gameTimer).ToString();

        if(m_gameTimer <= 0)
        {
            GameTimerText.text = "";
            EndGame();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void EndGame()
    {
        GameActive = false;

        // Play an animation on the canvas.
        EndGameCanvas.GetComponent<Animator>().SetTrigger("GameEnd");
        EndGameCanvas.InitializeForEndOfGame();
    }

    /// <summary>
    /// Start the game.
    /// </summary>
    public void StartGame()
    {
        GameActive = true;
        AddUnits(NumUnitsToSpawn);

        GameCanvas.SetActive(true);
        TitleScreenCanvas.SetActive(false);

        MenuMusicSource.Stop();
        GameMusicSource.Play();
    }
}
