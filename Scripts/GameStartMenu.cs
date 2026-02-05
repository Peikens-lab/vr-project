using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject options;

    [Header("Main Menu Buttons")]
    public Button startButton;
    public Button optionButton;
    public Button quitButton;

    [Header("Return Buttons")]
    public List<Button> returnButtons;

    void Start()
    {
        Debug.Log("GameStartMenu started on " + gameObject.name);

        EnableMainMenu();

        if (startButton)
            startButton.onClick.AddListener(StartGame);

        if (optionButton)
            optionButton.onClick.AddListener(EnableOption);

        if (quitButton)
            quitButton.onClick.AddListener(QuitGame);

        foreach (Button btn in returnButtons)
        {
            if (btn)
                btn.onClick.AddListener(EnableMainMenu);
        }
    }

    public void StartGame()
    {
        Debug.Log("StartGame called");
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(1);
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame called");
        Application.Quit();
    }

    void HideAll()
    {
        if (mainMenu) mainMenu.SetActive(false);
        if (options) options.SetActive(false);
    }

    public void EnableMainMenu()
    {
        Debug.Log("EnableMainMenu called");
        if (mainMenu) mainMenu.SetActive(true);
        if (options) options.SetActive(false);
    }

    public void EnableOption()
    {
        Debug.Log("EnableOption called");
        if (mainMenu) mainMenu.SetActive(false);
        if (options) options.SetActive(true);
    }
}
