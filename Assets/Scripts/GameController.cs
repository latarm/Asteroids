using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameController : MonoBehaviour 
{

    #region Fields

    public LevelData LevelData;

    public GameObject Player;
    public CinemachineVirtualCamera VirtualCamera;
    private AsteroidsSpawnController _asteroidsSpawnController;
    private ShipController _shipController;
    private AudioSource _audioSource;

    #endregion

    #region UnityMethods

    private void Awake()
    {
        Player = Instantiate(LevelData.PlayerPrefab, new Vector2(0, 0), Quaternion.identity);
        _shipController = Player.GetComponent<ShipController>();
        _shipController.GameController = this;
        VirtualCamera.Follow = Player.transform;
        LevelData.CurrentScore = 0;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = LevelData.BackgroundMusic;
        _audioSource.Play();

        if (Player == null)
            Player = GameObject.FindGameObjectWithTag("Player");
        _asteroidsSpawnController = GetComponent<AsteroidsSpawnController>();
    }    

    void Update()
    {
        if (Player == null)
            _asteroidsSpawnController.enabled = false;
    }

    #endregion

    #region Methods

    private void Win()
    {
        if(LevelData.CurrentScore>=LevelData.WinScore)
        {
            LevelData.LevelState = LevelState.Finished;
        }
        else
        {
            LevelData.LevelState = LevelState.Open;
        }
    }

    private void Lose()
    {
        if(_shipController.ShipInformation.Lifes==0)
        {

        }
    }

    #endregion
}
