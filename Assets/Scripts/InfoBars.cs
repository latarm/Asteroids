using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBars : MonoBehaviour
{
    private GameObject _player;
    private ShipController _shipController;
    [SerializeField] GameObject _ammoPref;
    [SerializeField] private Image _lifesAmount;
    [SerializeField] private Text _ammoAmount;
    [SerializeField] private Text _scoreBar;

    void Start()
    {
        if (_player == null)
            _player = GameObject.FindGameObjectWithTag("Player");

        _shipController = _player.GetComponent<ShipController>();

        if (_lifesAmount == null)
            _lifesAmount = transform.Find("Life Bar").transform.Find("Mask").transform.Find("Life").GetComponent<Image>();
        _lifesAmount.fillAmount = 0;

        if(_ammoAmount==null)
            _ammoAmount = transform.Find("Ammo Bar").transform.Find("Ammo amount").GetComponent<Text>();
        _ammoAmount.text = _shipController.Ammo.ToString();

        if (_scoreBar == null)
            _scoreBar = transform.Find("Score Bar").GetComponent<Text>();
        _scoreBar.text = _shipController.Score.ToString();
    }

    private void LateUpdate()
    {
        CheckLife();
        CheckAmmo();
        ChekScore();
    }

    void ChekScore()
    {
        _scoreBar.text = _shipController.Score.ToString();
    }

    void CheckAmmo()
    {
        _ammoAmount.text = _shipController.Ammo.ToString();
    }

    void CheckLife()
    {
        _lifesAmount.fillAmount = (float)(_shipController.MaxLife - _shipController.Lifes) / 3;
    }
}
