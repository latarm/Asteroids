using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SpeedTutorMainMenuSystem
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameData GameData;

        [Header("Levels To Load")]
        public string _newGameButtonLevel;
        private string levelToLoad;

        private int menuNumber;

        #region Menu Dialogs
        [Header("Main Menu Components")]
        [SerializeField] private GameObject MenuDefaultCanvas;
        [SerializeField] private GameObject SelectLevelCanvas;
        [Space(10)]
        [Header("Levels")]
        [SerializeField] private GameObject[] Levels;

        #endregion

        #region Initialisation - Button Selection & Menu Order

        private void Start()
        {
            menuNumber = 1;
        }
        #endregion

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (menuNumber == 2 || menuNumber == 3|| menuNumber == 4 || menuNumber == 5)
                {
                    GoBackToMainMenu();
                    ClickSound();
                }
            }           
        }

        private void ClickSound()
        {
            GetComponent<AudioSource>().Play();
        }

        #region Menu Mouse Clicks

        public void MouseClick(string buttonType)
        {
            if (buttonType == "Select level")
            {
                MenuDefaultCanvas.SetActive(false);
                SelectLevelCanvas.SetActive(true);
                menuNumber = 2;

                for (int index = 0; index < Levels.Length; index++)
                {
                    switch (GameData.LevelDatas[index].LevelState)
                    {
                        case LevelState.Close:
                            {
                                Levels[index].GetComponent<Button>().interactable = false;
                                break;
                            }
                        case LevelState.Complited:
                            {
                                Levels[index].GetComponent<Button>().interactable = true;

                                Levels[index].GetComponent<Text>().text += " (Complited)";
                                break;
                            }
                        case LevelState.Open:
                            {
                                Levels[index].GetComponent<Button>().interactable = true;
                                break;
                            }
                    }
                }
            }

            if (buttonType == "Back")
            {
                SelectLevelCanvas.SetActive(false);
                MenuDefaultCanvas.SetActive(true);
                menuNumber = 3;
            }

            if (buttonType == "Exit")
            {
                Debug.Log("YES QUIT!");
                Application.Quit();
            }
        }

        public void LoadSelectedScene(int levelNumber)
        {
            SceneManager.LoadScene(levelNumber);
        }

        #endregion

        #region Back to Menus

        public void GoBackToMainMenu()
        {
            MenuDefaultCanvas.SetActive(true);
            SelectLevelCanvas.SetActive(false);
            menuNumber = 1;
        }

        public void ClickNoSaveDialog()
        {
            GoBackToMainMenu();
        }
        #endregion
    }
}
