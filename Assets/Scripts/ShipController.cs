using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipController : MonoBehaviour
{
    #region Fields

    public int MaxLife;
    public int Lifes;
    public int Ammo;
    public int MaxAmmo;
    public int Score;
    public AudioClip[] _audioClips;
    public GameObject Projectile;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    private float _horizontal, _vertical;
    private bool _shooting=false;
    private Coroutine _reloadRoutine;
    private AudioSource _audioSource;

    #endregion

    #region UnityMethods

    private void Start()
    {
        Score = 0;
        _audioSource = transform.GetComponent<AudioSource>();
    }

    void Update()
    {
        Move();
        Shoot();
        Reload();
    }

    #endregion

    #region Methods

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _shooting = true;
            StartCoroutine(ShootCoroutine());
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _shooting = false;
            StopCoroutine(ShootCoroutine());
        }
    }

    void Reload()
    {
        if (!_shooting && _reloadRoutine == null && Ammo < MaxAmmo)
            _reloadRoutine= StartCoroutine(ReloadCoroutine());
        if(_reloadRoutine!=null && _shooting && Ammo!=0)
        {
            StopCoroutine(_reloadRoutine);
            _reloadRoutine = null;
        }
    }

    IEnumerator ShootCoroutine()
    {
        while (_shooting&&Ammo>0)
        {
            GameObject projectile = Instantiate(Projectile, transform.position, transform.rotation);
            projectile.transform.parent = null;
            projectile.name = "Projectile";
            _audioSource.PlayOneShot(_audioClips[0]);
            Ammo--;
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator ReloadCoroutine()
    {
        while (Ammo < MaxAmmo)
        {
            yield return new WaitForSeconds(0.3f);
            Ammo++;
        }
        _reloadRoutine = null;
    }

    void Move()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D))
        {
            Vector3 normalizedVector = new Vector3(_horizontal, _vertical, 0).normalized;
            transform.position += normalizedVector * _moveSpeed * Time.deltaTime;

            float angle = Mathf.Atan2(normalizedVector.y, normalizedVector.x) * Mathf.Rad2Deg-90;
            Quaternion quaternion = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, _rotationSpeed);            
        }
    }

    public void GetDamage(int damage)
    {
        Lifes -= damage;
        _audioSource.PlayOneShot(_audioClips[1]);
        if (Lifes == 0)
        {
            Destroy(gameObject);
        }
    }

    #endregion
}
