using UnityEngine;
using System.Collections;

public class ShipController : Singleton<ShipController>
{
    #region Fields

    public ShipData ShipInformation;

    public Joystick Joystick;

    private float _horizontal, _vertical;
    private bool _shooting=false;
    private Coroutine _reloadRoutine;
    private AudioSource _audioSource;
    

    #endregion

    #region UnityMethods

    public void Start()
    {
        Joystick = GameController.Instance.Joystick;

        gameObject.GetComponent<SpriteRenderer>().sprite = ShipInformation.ShipSprite;
        ShipInformation.Lifes = 3;
        ShipInformation.Score = 0;
        _audioSource = transform.GetComponent<AudioSource>();
    }

    public void Update()
    {
        Move();
        Shooting();
        Reload();

        if (ShipInformation.Lifes > ShipInformation.MaxLife)
            ShipInformation.Lifes = ShipInformation.MaxLife;

        if (ShipInformation.Ammo > ShipInformation.MaxAmmo)
            ShipInformation.Ammo = ShipInformation.MaxAmmo;
    }

    #endregion

    #region Methods

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _shooting = true;
            StartCoroutine(ShootRoutine());
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _shooting = false;
            StopCoroutine(ShootRoutine());
        }
    }

    void Reload()
    {
        if (!_shooting && _reloadRoutine == null && ShipInformation.Ammo < ShipInformation.MaxAmmo)
            _reloadRoutine= StartCoroutine(ReloadRoutine());
        if(_reloadRoutine!=null && _shooting && ShipInformation.Ammo!=0)
        {
            StopCoroutine(_reloadRoutine);
            _reloadRoutine = null;
        }
    }

    IEnumerator ShootRoutine()
    {
        while (_shooting&&ShipInformation.Ammo>0)
        {
            GameObject projectile = Instantiate(ShipInformation.Projectile, transform.position, transform.rotation);
            projectile.transform.parent = null;
            projectile.name = "Projectile";
            projectile.GetComponent<Projectile>()._firing_ship = gameObject;
            _audioSource.PlayOneShot(ShipInformation.AudioClips[0]);
            ShipInformation.Ammo--;
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator ReloadRoutine()
    {
        while (ShipInformation.Ammo < ShipInformation.MaxAmmo)
        {
            yield return new WaitForSeconds(0.3f);
            ShipInformation.Ammo++;
        }
        _reloadRoutine = null;
    }

    void Move()
    {
#if UNITY_EDITOR

        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

#endif

#if UNITY_ANDROID

        _horizontal=Joystick.Horizontal;
        _vertical=Joystick.Vertical;
#endif

        if (_horizontal!=0|| _vertical!=0)
        {
            Vector3 normalizedVector = new Vector3(_horizontal, _vertical, 0).normalized;
            transform.position += normalizedVector * ShipInformation.MoveSpeed * Time.deltaTime;

            float angle = Mathf.Atan2(normalizedVector.y, normalizedVector.x) * Mathf.Rad2Deg-90;
            Quaternion quaternion = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, ShipInformation.RotationSpeed);            
        }
    }

    public void GetDamage(int damage)
    {
        ShipInformation.Lifes -= damage;
        _audioSource.PlayOneShot(ShipInformation.AudioClips[1]);
        if (ShipInformation.Lifes == 0)
        {
            gameObject.SetActive(false);
        }
    }

#endregion
}
