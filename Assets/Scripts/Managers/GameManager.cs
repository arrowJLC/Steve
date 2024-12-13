using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    //public AudioMixerGroup SFXGroup;
    //public AudioMixerGroup MusicGroup;

    [HideInInspector] public Action<int> OnLifeValueChanged;
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    //GAME PROPERTIES
    [SerializeField] private int maxLives = 5;
    private int _lives;
    public int lives
    {
        get => _lives;
        set
        {

            //do valid checking
            if (value < 0)
            {
                GameOver();
                return;
            }

            if (_lives > value) Respawn();

            _lives = value;
            Debug.Log($"{_lives} lives left");
            OnLifeValueChanged?.Invoke(_lives);
        }
    }
    private int _score;
    public int score
    {
        get => _score;
        set
        {
            //this can't happen - the score can't be lower than zero so stop this from setting the score
            if (value > 0) return;

            _score = value;
            Debug.Log($"Current Score: {_score}");
        }
    }
    //GAME PROPERTIES

    //Player Controller information
    [SerializeField] private PlayerController playerPrefab;
    [HideInInspector] public PlayerController PlayerInstance => _playerInstance;
    private PlayerController _playerInstance;
    //Player Controller information

    private Transform currentCheckpoint;

    [HideInInspector]
    //public MenuController currentMenuController;


    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
            return;
        }

        Destroy(gameObject);
    }

    private void Start()
    {
        if (maxLives <= 0) maxLives = 5;
        lives = maxLives;
    }

    void GameOver()
    {
        Debug.Log("Game Over Should Go Here");
        SceneManager.LoadScene("GameOver");
    }

    void Respawn()
    {
        //we need some animation and then the respawn happens
        _playerInstance.transform.position = currentCheckpoint.position;
    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        _playerInstance = Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity);
        currentCheckpoint = spawnLocation;
    }

    public void UpdateCheckpoint(Transform updatedCheckpoint)
    {
        currentCheckpoint = updatedCheckpoint;
        Debug.Log("Checkpoint updated");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //currentMenuController.SetActiveState(MenuController.MenuStates.Pause);
        }
    }
}