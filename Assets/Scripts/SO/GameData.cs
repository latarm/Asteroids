using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New game data", menuName = "Game data", order =1)]
public class GameData : ScriptableObject
{
    public LevelData[] LevelDatas;
    public ShipData[] ShipDatas;
}
