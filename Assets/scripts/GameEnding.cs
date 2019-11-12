using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public Image exitBackgroundImage;
    public AudioSource exitAudio;
    public Image caughtBackgroundImage;
    public AudioSource caughtAudio;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool m_HasAudioPlayed;

    public chase c;

    private float timeLeft = 0.1f;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private Image GameOverImage;
    [SerializeField] private Image GameOverTextImage;
    private float alphaLevel = 0f;
    private int count = 50;

    private float wintimeLeft = 0.1f;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Image winImage;
    [SerializeField] private Image winTextImage;
    private float winalphaLevel = 0f;
    private int wincount = 50;

    [SerializeField] private PlayerLook PL;
    [SerializeField] private Pause P;

    void Start()
    {
        PL = GameObject.FindObjectOfType<PlayerLook>();
        P = GameObject.FindObjectOfType<Pause>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Exit1");
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
            //Debug.Log("Exit2");
        }
    }

    public void CaughtPlayer()
    {
        //Debug.Log("[CaughtPlayer()]");
        m_IsPlayerCaught = true;
        //Debug.Log("Caught");
    }

    private void Update()
    {
        //Debug.Log("[UPDATE]");
        if (m_IsPlayerAtExit)
        {
            EndLevel(exitBackgroundImage, false, exitAudio);
            //Debug.Log("Exit3");
            winPanel.gameObject.SetActive(true);
            Win();
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImage, true, caughtAudio);
            //Debug.Log("[CAUGHT]");
            GameOverPanel.gameObject.SetActive(true);
            Caught();
        }
    }

    private void EndLevel(Component image, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

        m_Timer += Time.deltaTime;
        //image.alpha = m_Timer / fadeDuration;
        ///image.gameObject.SetActive(true);
        //Debug.Log("Exit4");
        if (!(m_Timer > fadeDuration + displayImageDuration)) return;
        if (doRestart)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        else
        {
            //Debug.Log("Exit5");
            //Application.Quit();

            if (P.isPaused)
            {
                P.pauseGame();
            }

            PL.UnLockCursor();
            SceneManager.LoadScene("CreditsScene");
        }
    }

    private void Caught()
    {
        timeLeft -= Time.deltaTime;
        if (count > 0)
        {
            if (timeLeft < 0)
            {
                alphaLevel += 0.02f;
                GameOverImage.color = new Color(0, 0, 0, alphaLevel);
                GameOverTextImage.color = new Color(0, 0, 0, alphaLevel);
                timeLeft = 0.1f;
                count--;
            }
        }
        else
        {
            GameOverTextImage.gameObject.SetActive(false);
            GameOverImage.gameObject.SetActive(false);
            GameOverPanel.gameObject.SetActive(false);
        }
    }

    private void Win()
    {
        //Debug.Log("Won!");
        wintimeLeft -= Time.deltaTime;
        if (wincount > 0)
        {
            if (wintimeLeft < 0)
            {
                //  Debug.Log("winalphaLevel: "+winalphaLevel);
                winalphaLevel += 0.02f;
                winImage.color = new Color(1, 1, 1, winalphaLevel);
                winTextImage.color = new Color(1, 1, 1, winalphaLevel);
                wintimeLeft = 0.1f;
                wincount--;
            }
        }
        else
        {
            //winTextImage.gameObject.SetActive(false);
            //winImage.gameObject.SetActive(false);
            //winPanel.gameObject.SetActive(false);
            //Debug.Log("Won!");
            //Application.Quit();
        }
    }
}