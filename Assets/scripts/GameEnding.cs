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

        }
        else if (m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImage, true, caughtAudio);
            Debug.Log("[CAUGHT]");
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
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
        else
        {
            //Debug.Log("Exit5");
            Application.Quit();
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
}
