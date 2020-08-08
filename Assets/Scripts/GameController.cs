using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameController : MonoBehaviour 
{
    public LevelData levelData;
    public GameObject Player;
    public GameObject PlayerPrefab;
    public GameObject InfoBars;
    public CinemachineVirtualCamera VirtualCamera;
    private AsteroidsSpawnController _asteroidsSpawn;

    private void Awake()
    {
        Player = Instantiate(PlayerPrefab, new Vector2(0, 0), Quaternion.identity);
    }

    private void Start()
    {        
        if (Player == null)
            Player = GameObject.FindGameObjectWithTag("Player");
        _asteroidsSpawn = GetComponent<AsteroidsSpawnController>();
    }
    

    void Update()
    {
        if (Player == null)
            _asteroidsSpawn.enabled = false;
    }

}
