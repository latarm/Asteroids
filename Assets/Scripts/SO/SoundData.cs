using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New sound data", menuName ="Sound data", order =1)]
public class SoundData : ScriptableObject
{
    public AudioClip[] audioClips;
}
