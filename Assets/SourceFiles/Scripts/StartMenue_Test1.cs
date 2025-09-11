using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenue : MonoBehaviour
{

    public string newGameScene;

    public GameObject optionScreen;

    void Start()
    {
        optionScreen.SetActive(false);
    }

    void Update()
    {
    }

    public void openOptions()
    {
        optionScreen.SetActive(true);
    }

    public void closeOptions()
    {
        optionScreen.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void QuitGame()
    {
        Application.Quit();

    }


}
