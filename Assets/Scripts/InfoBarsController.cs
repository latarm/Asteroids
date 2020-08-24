using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfoBarsController : MonoBehaviour
{
    #region FIelds

    private GameObject _winDialog;
    private GameObject _loseDialog;
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
        _levelData = GameController.Instance.LevelData;       

        if (_lifesAmount == null)
            _lifesAmount = transform.Find("Life Bar").transform.Find("Mask").transform.Find("Life").GetComponent<Image>();
        _lifesAmount.fillAmount = 0;

        if(_ammoAmount==null)
            _ammoAmount = transform.Find("Ammo Bar").transform.Find("Ammo amount").GetComponent<Text>();
        _ammoAmount.text = ShipController.Instance.ShipInformation.Ammo.ToString();

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
            ShipController.Instance.gameObject.SetActive(false);
            _canvas.sortingOrder = 2;
            _winDialog.SetActive(true);

            for (int index = 1; index < GameController.Instance.GameData.LevelDatas.Length; index++)
            {
                if (GameController.Instance.GameData.LevelDatas[index - 1].LevelState == LevelState.Complited && GameController.Instance.GameData.LevelDatas[index].LevelState == LevelState.Close)
                {
                    GameController.Instance.GameData.LevelDatas[index].LevelState = LevelState.Open;
                }
            }
        }
        _scoreBar.text = _levelData.CurrentScore.ToString()+"/"+_levelData.WinScore.ToString();
    }

    void CheckAmmo()
    {
        _ammoAmount.text = ShipController.Instance.ShipInformation.Ammo.ToString();
    }

    void CheckLife()
    {
        _lifesAmount.fillAmount = (float)(ShipController.Instance.ShipInformation.MaxLife - ShipController.Instance.ShipInformation.Lifes) / ShipController.Instance.ShipInformation.MaxLife;
        if (ShipController.Instance.ShipInformation.Lifes == 0)
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
        SceneManager.LoadScene(2);

        //if (nextLevel <= SceneManager.sceneCountInBuildSettings - 1)
        //    SceneManager.LoadScene(nextLevel);
        //else
        //    SceneManager.LoadScene("MainMenu");
    }


    #endregion
}
