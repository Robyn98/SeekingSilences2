using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pause : MonoBehaviour
{
    public bool isPaused = false;

    public Text pauseText;
    public GameObject pausePanel;
    [SerializeField] private PlayerLook PL;

    void Start()
    {
        PL = GameObject.FindObjectOfType<PlayerLook>();
    }
    
    public void pauseGame()
    {
        if(isPaused) /// paused -> play
        {
            isPaused = false;
            Time.timeScale = 1;
            //Debug.Log("IS Playing");
            pausePanel.gameObject.SetActive(false);
            PL.LockCursor();
        }
        else /// play -> pause
        {
            isPaused = true;
            Time.timeScale = 0;
            //Debug.Log("IS PAUSED");
            pausePanel.gameObject.SetActive(true);
            PL.UnLockCursor();
        }
    }
}
