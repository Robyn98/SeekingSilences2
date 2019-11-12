using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject WakeUpPanel;

    [SerializeField] private Pause P;
    
//    [SerializeField] private DontDestroyMusic DDM;
//    private void Awake()
//    {
//        DDM = GameObject.FindObjectOfType<DontDestroyMusic>();
//        Destroy(DDM);
//    }
[SerializeField] private PlayerLook PL;

    
    
        
    
    void Start()
    {
        WakeUpPanel.gameObject.SetActive(true);
        P = GameObject.FindObjectOfType<Pause>();
        PL = GameObject.FindObjectOfType<PlayerLook>();
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            P.pauseGame();
        }
    }

    void PauseGame()
    {
        P.pauseGame();
    }

    public void QuitGame()
    {
        P.pauseGame();
        PL.UnLockCursor();
        SceneManager.LoadScene("MenuScene");
        
    }
}