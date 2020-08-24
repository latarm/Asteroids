using System.Collections;
using UnityEngine;

public class AsteroidsSpawnController : MonoBehaviour
{
    #region Fields

    private LevelData _levelData;
    private GameObject[] _asteroidsPrefabs;
    private Transform _cameraTransform;

    #endregion

    #region UnityMethods

    public void Start()
    {
        _levelData = GameController.Instance.LevelData;       

        _asteroidsPrefabs = _levelData.AsteroidsPrefabs;

        if (_cameraTransform == null)
            _cameraTransform = Camera.main.transform;

        StartCoroutine(SpawnRoutine());
    }

    #endregion

    #region Methods

    private IEnumerator SpawnRoutine()
    {
        while(ShipController.Instance.enabled)
        {
            GameObject asteroid = Instantiate(_asteroidsPrefabs[Random.Range(0, _asteroidsPrefabs.Length - 1)], RandomSpawnPosition(), Quaternion.identity);
            asteroid.name = "Asteroid";
            asteroid.transform.localScale *= Random.Range(1f, 1.5f);

            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }
        yield return null;
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