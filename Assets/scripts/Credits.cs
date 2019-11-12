using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] private string Menu;


    void Update()
    {
        //quit
        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene(Menu);
        }
    }


    public void StartMenu()
    {
        SceneManager.LoadScene(Menu);
    }
}