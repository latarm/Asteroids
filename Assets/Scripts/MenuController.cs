using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    [SerializeField] private GameData GameData;
    [Space(20)]
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _selectLevelMenu;
    [Space(20)]
    [SerializeField] private GameObject[] _levels;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoBackToMainMenu();
            ClickSound();
        }
    }

    private void ClickSound()
    {
        GetComponent<AudioSource>().Play();
    }

    public void MouseClick(string button)
    {
        switch (button)
        {
            case "Select level":
                _mainMenu.SetActive(false);
                _selectLevelMenu.SetActive(true);
                UpdateLevelsMenu();
                break;

            case "Back":
                _selectLevelMenu.SetActive(false);
                _mainMenu.SetActive(true);
                break;

            case "Exit":
                Application.Quit();
                break;

        }
    }

    void UpdateLevelsMenu()
    {
        for (int index = 0; index < _levels.Length; index++)
        {
            switch (GameData.LevelDatas[index].LevelState)
            {
                case LevelState.Close:
                    {
                        _levels[index].GetComponent<Button>().interactable = false;
                        break;
                    }
                case LevelState.Complited:
                    {
                        _levels[index].GetComponent<Button>().interactable = true;

                        _levels[index].GetComponent<Text>().text = GameData.LevelDatas[index].name + " (Complited)";
                        break;
                    }
                case LevelState.Open:
                    {
                        _levels[index].GetComponent<Button>().interactable = true;
                        break;
                    }
            }
        }
    }

    public void LoadSelectedScene(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }

    public void GoBackToMainMenu()
    {
        _mainMenu.SetActive(true);
        _selectLevelMenu.SetActive(false);
    }
}

