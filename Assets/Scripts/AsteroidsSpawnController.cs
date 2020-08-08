using UnityEngine;

public class AsteroidsSpawnController : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject[] _asteroidsPrefabs;
    private Transform _cameraTransform;
    private float _spawnDelay;

    #endregion

    #region UnityMethods

    private void Start()
    {
        _spawnDelay = 2f;
        if (_cameraTransform == null)
            _cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        _spawnDelay -= Time.deltaTime;
        if (_spawnDelay <= 0)
        {
            Spawn(RandomSpawnPosition());
            _spawnDelay = Random.Range(0.5f, 1.5f);
        }
    }

    #endregion

    #region Methods

    private void Spawn(Vector3 spawnPosition)
    {
        GameObject asteroid = Instantiate(_asteroidsPrefabs[Random.Range(0, 6)], spawnPosition, Quaternion.identity);
        asteroid.name = "Asteroid";
        asteroid.transform.localScale *= Random.Range(1f, 1.5f);
    }

    private Vector3 RandomSpawnPosition()
    {
        float offset = Random.Range(1.5f, 1.75f);

        float _spawnRange = Camera.main.orthographicSize * offset;

        float randomAngle = Random.Range(0, Mathf.PI * 2);
        float x = _cameraTransform.position.x + Mathf.Cos(randomAngle) * _spawnRange*1.25f;
        float y = _cameraTransform.position.y + Mathf.Sin(randomAngle) * _spawnRange;

        Vector3 spawnPosition = new Vector3(x, y, 2);

        return spawnPosition;
    }

    #endregion

}