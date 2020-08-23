using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameController : MonoBehaviour 
{

    #region Fields

    public GameData GameData;
    public LevelData LevelData;
    public Joystick Joystick;

    public GameObject Player;
    public CinemachineVirtualCamera VirtualCamera;
    private AsteroidsSpawnController _asteroidsSpawnController;
    private ShipController _shipController;
    private AudioSource _audioSource;
    private Transform _background;

    #endregion

    #region UnityMethods

    private void Awake()
    {
        if (Player == null)
        {
            Player = Instantiate(LevelData.PlayerPrefab, new Vector2(0, 0), Quaternion.identity);
            Player.name = "Player ship";
            _shipController = Player.GetComponent<ShipController>();
            _shipController.GameController = this;
            _shipController.Joystick = Joystick;
        }
       
        LevelData.CurrentScore = 0;
    }

    private void Start()
    {
        if (VirtualCamera == null)
            VirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        VirtualCamera.Follow = Player.transform;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = LevelData.BackgroundMusic;
        _audioSource.Play();

        RenderSettings.skybox = LevelData.SkyBoxMaterial;

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

}
