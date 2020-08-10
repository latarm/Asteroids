using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfoBarsController : MonoBehaviour
{
    #region FIelds

    public GameController GameController;

    private GameObject _player;
    private GameObject _winDialog;
    private GameObject _loseDialog;
    private ShipController _shipController;
    private LevelData _levelData;

    private Image _lifesAmount;
    private Text _ammoAmount;
    private Text _scoreBar;
    private Canvas _canvas;
    private GameObject _shootButton;

    #endregion

    #region UnityMethods

    void Start()
    {
        if (GameController == null)
        {
            GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            _levelData = GameController.LevelData;
            _player = GameController.Player;
            _shipController = _player.GetComponent<ShipController>();
        }

        if (_lifesAmount == null)
        {
            _shootButton = transform.Find("ShootButton").gameObject;
        }


        if (_lifesAmount == null)
            _lifesAmount = transform.Find("Life Bar").transform.Find("Mask").transform.Find("Life").GetComponent<Image>();
        _lifesAmount.fillAmount = 0;

        if(_ammoAmount==null)
            _ammoAmount = transform.Find("Ammo Bar").transform.Find("Ammo amount").GetComponent<Text>();
        _ammoAmount.text = _shipController.ShipInformation.Ammo.ToString();

        if (_scoreBar == null)
            _scoreBar = transform.Find("Score Bar").GetComponent<Text>();

        if (_winDialog == null)
        {
            _winDialog = transform.Find("Win").gameObject;
            _winDialog.SetActive(false);
        }

        if (_loseDialog == null)
        {
            _loseDialog = transform.Find("Lose").gameObject;
            _loseDialog.SetActive(false);
        }

        _canvas = GetComponent<Canvas>();

        _scoreBar.text = _levelData.CurrentScore.ToString();

        
    }

    private void Update()
    {
        CheckLife();
        CheckAmmo();
        ChekScore();
    }

    #endregion

    #region Methods

    void ChekScore()
    {
        if (_levelData.CurrentScore >= _levelData.WinScore)
        {
            _levelData.CurrentScore = _levelData.WinScore;
            _levelData.LevelState = LevelState.Complited;
            _player.SetActive(false);
            _canvas.sortingOrder = 2;
            _winDialog.SetActive(true);

            for (int index = 1; index < GameController.GameData.LevelDatas.Length; index++)
            {
                if (GameController.GameData.LevelDatas[index - 1].LevelState == LevelState.Complited && GameController.GameData.LevelDatas[index].LevelState == LevelState.Close)
                {
                    GameController.GameData.LevelDatas[index].LevelState = LevelState.Open;
                }
            }
        }
        _scoreBar.text = _levelData.CurrentScore.ToString()+"/"+_levelData.WinScore.ToString();
    }

    void CheckAmmo()
    {
        _ammoAmount.text = _shipController.ShipInformation.Ammo.ToString();
    }

    void CheckLife()
    {
        _lifesAmount.fillAmount = (float)(_shipController.ShipInformation.MaxLife - _shipController.ShipInformation.Lifes) / _shipController.ShipInformation.MaxLife;
        if (_shipController.ShipInformation.Lifes == 0)
            _loseDialog.SetActive(true);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainManu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextLevel<=SceneManager.sceneCountInBuildSettings-1)        
            SceneManager.LoadScene(nextLevel);       
        else
            SceneManager.LoadScene(0);        
    }


    #endregion
}
