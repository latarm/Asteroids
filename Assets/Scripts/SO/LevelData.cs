using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New level data", menuName ="Level data", order =1)]
public class LevelData : ScriptableObject
{
    public Sprite LevelBackground;
    public LevelState LevelState;
    public AudioClip BackgroundMusic;
    public GameObject[] AsteroidsPrefs;
    public int WinScore;
}

public enum LevelState
{
    Close,
    Open,
    Finished
}
