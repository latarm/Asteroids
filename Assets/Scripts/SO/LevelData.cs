﻿using UnityEngine;

[CreateAssetMenu(fileName ="New level data", menuName ="Level data", order =1)]
public class LevelData : ScriptableObject
{
    //public Sprite LevelBackground;
    public Material SkyBoxMaterial;
    public LevelState LevelState;
    public AudioClip BackgroundMusic;
    public GameObject PlayerPrefab;
    public int CurrentScore;
    public int WinScore;
    [Space(20)]
    public GameObject[] AsteroidsPrefabs;
    public float AsteroidsMinMoveSpeed;
    public float AsteroidsMaxMoveSpeed;
}

public enum LevelState
{
    Close,
    Open,
    Complited
}
