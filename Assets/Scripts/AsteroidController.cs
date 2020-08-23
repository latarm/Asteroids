﻿using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    #region Fields

    private LevelData _levelData;

    private GameController _gameController;

    private Transform _playerTransform;
    private Vector2 _playerPosition;
    private Vector2 _moveDirection;
    private float _moveSpeed;
    private float _rotationSpeed;
    private int _damage;
    private Transform _childTransform;

    #endregion

    #region UnityMethods

    public void Start()
    {
        if (_gameController == null)
        {
            _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            _levelData = _gameController.LevelData;
        }


        if (_playerTransform == null)
            _playerTransform = _gameController.Player.transform;

        if (_playerTransform != null)
        {
            _playerPosition = _playerTransform.position;
            _moveDirection = new Vector2(transform.position.x - _playerPosition.x, transform.position.y - _playerPosition.y).normalized;
        }

        _damage = 1;

        _childTransform = transform.GetChild(0);
        _rotationSpeed = Random.Range(-3, 3);

        _moveSpeed = Random.Range(_levelData.AsteroidsMinMoveSpeed, _levelData.AsteroidsMaxMoveSpeed);
    }

    public void Update()
    {
        Vector3 normalizedVector = _moveDirection;
        transform.position -= normalizedVector * _moveSpeed * Time.deltaTime;

        if (transform.childCount == 0)
            Destroy(gameObject);

        if (_playerTransform != null && IsTargetFarThanDistance(_playerTransform.position, 20f))
            Destroy(gameObject);

        _childTransform.Rotate(0, 0, _rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<ShipController>().GetDamage(_damage);
            Destroy(gameObject);
        }
    }

    #endregion


    bool IsTargetFarThanDistance(Vector2 targetPosition, float distance)
    {
        float x = (transform.position.x - targetPosition.x) * (transform.position.x - targetPosition.x);
        float y = (transform.position.y - targetPosition.y) * (transform.position.y - targetPosition.y);

        return distance * distance < x + y;
    }
}
