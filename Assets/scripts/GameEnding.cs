﻿using System.Collections;
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
        m_IsPlayerCaught = true;
        //Debug.Log("Caught");
    }

    private void Update()
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel(exitBackgroundImage, false, exitAudio);
            //Debug.Log("Exit3");

        }
        else if (m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImage, true, caughtAudio);
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
        image.gameObject.SetActive(true);
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
}
