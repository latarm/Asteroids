using UnityEngine;


[CreateAssetMenu(fileName ="New ship data", menuName ="Ship data", order = 1)]

public class ShipData : ScriptableObject
{
    public int MaxAmmo;
    public int Ammo;
    public Sprite ShipSprite;
    public int MaxLife;
    public int Lifes;
    public float MoveSpeed;
    public float RotationSpeed;
    public AudioClip[] AudioClips;
    public GameObject Projectile;
    public int Score;

}
