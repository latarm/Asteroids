using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfoBarsController : MonoBehaviour
{
    #region FIelds

    public GameObject WinDialog;
    public GameObject _loseDialog;
    public Image Life;
    public Text AmmoAmount;
    public Text ScoreBar;

    private GameObject _shootButton;
    private LevelData _levelData;
    
    #endregion

    #region UnityMethods

    void Start()
    {
        _levelData = GameController.Instance.LevelData;       

        if (Life == null)
            Life = transform.Find("Life Bar").transform.Find("Mask").transform.Find("Life").GetComponent<Image>();
        Life.fillAmount = 0;

        if(AmmoAmount==null)
            AmmoAmount = transform.Find("Ammo Bar").transform.Find("Ammo amount").GetComponent<Text>();
        AmmoAmount.text = ShipController.Instance.ShipInformation.Ammo.ToString();

        if (ScoreBar == null)
            ScoreBar = transform.Find("Score Bar").GetComponent<Text>();

        if (WinDialog == null)
        {
            WinDialog = transform.Find("Win").gameObject;
            WinDialog.SetActive(false);
        }

        if (_loseDialog == null)
        {
            _loseDialog = transform.Find("Lose").gameObject;
            _loseDialog.SetActive(false);
        }

        ScoreBar.text = _levelData.CurrentScore.ToString();       
    }

    private void Update()
    {
        CheckLife();
        CheckAmmo();
        CheсkScore();
    }

    #endregion

    #region Methods

    public void ShipShoot()
    {
        ShipController.Instance.Shoot();
    }

    void CheсkScore()
    {
        if (_levelData.CurrentScore >= _levelData.WinScore)
        {
            _levelData.CurrentScore = _levelData.WinScore;
            _levelData.LevelState = LevelState.Complited;
            ShipController.Instance.gameObject.SetActive(false);
            WinDialog.SetActive(true);

            for (int index = 1; index < GameController.Instance.GameData.LevelDatas.Length; index++)
            {
                if (GameController.Instance.GameData.LevelDatas[index - 1].LevelState == LevelState.Complited && GameController.Instance.GameData.LevelDatas[index].LevelState == LevelState.Close)
                {
                    GameController.Instance.GameData.LevelDatas[index].LevelState = LevelState.Open;
                }
            }
        }
        ScoreBar.text = _levelData.CurrentScore.ToString()+"/"+_levelData.WinScore.ToString();
    }

    void CheckAmmo()
    {
        AmmoAmount.text = ShipController.Instance.ShipInformation.Ammo.ToString();
    }

    void CheckLife()
    {
        Life.fillAmount = (float)(ShipController.Instance.ShipInformation.MaxLife - ShipController.Instance.ShipInformation.Lifes) / ShipController.Instance.ShipInformation.MaxLife;
        if (ShipController.Instance.ShipInformation.Lifes == 0)
            _loseDialog.SetActive(true);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainManu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextLevel+1 == SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextLevel);
        else
            SceneManager.LoadScene("MainMenu");
    }


    #endregion
}
