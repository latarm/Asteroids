using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameController : Singleton<GameController> 
{

    #region Fields

    public GameData GameData;
    public LevelData LevelData;
    public Joystick Joystick;
    public GameObject PlayerPrefab;
    public CinemachineVirtualCamera VirtualCamera;

    private GameObject _player;
    private AsteroidsSpawnController _asteroidsSpawnController;
    private ShipController _shipController;
    private AudioSource _audioSource;

    #endregion

    #region UnityMethods

    public override  void Awake()
    {
        base.Awake();

        if (_player == null)
        {
            _player = Instantiate(PlayerPrefab, new Vector2(0, 0), Quaternion.identity);
            _player.name = "Player ship";
            ShipController.Instance.Joystick = Joystick;
        }
       
        LevelData.CurrentScore = 0;
    }

    private void Start()
    {
        if (VirtualCamera == null)
            VirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        VirtualCamera.Follow = _player.transform;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = LevelData.BackgroundMusic;
        _audioSource.Play();

        RenderSettings.skybox = LevelData.SkyBoxMaterial;

        if (_player == null)
            _player = GameObject.FindGameObjectWithTag("Player");
        _asteroidsSpawnController = GetComponent<AsteroidsSpawnController>();
    }    

    void Update()
    {
        if (_player == null)
            _asteroidsSpawnController.enabled = false;
    }

    #endregion

}
