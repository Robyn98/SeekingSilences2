using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private string Game;
    [SerializeField] private string Credits;

    void Update()
    {
        //start
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(Game);
        }

        //quit
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(Game);
    }
    
    public void StartCredits()
    {
        SceneManager.LoadScene(Credits);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
    
    
}