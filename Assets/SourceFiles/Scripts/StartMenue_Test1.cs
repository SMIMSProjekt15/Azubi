using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenue : MonoBehaviour
{

    public string newGameScene;

    void Start()
    {

    }

    void Update()
    {
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
