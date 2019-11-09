using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WakeUp : MonoBehaviour
{
    private float timeLeft = 0.1f;
    [SerializeField] private GameObject WakeUpPanel;
    [SerializeField] private Image WakeUpImage;
    [SerializeField] private Image IntroImage;
    private float alphaLevel = 1f;
    private int count = 100;

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (count > 0)
        {
            if (timeLeft < 0)
            {
                alphaLevel -= 0.01f;
                WakeUpImage.color = new Color(0, 0, 0, alphaLevel);
                IntroImage.color = new Color(0, 0, 0, alphaLevel);
                timeLeft = 0.1f;
                count--;
            }
        }
        else
        {
            IntroImage.gameObject.SetActive(false);
            WakeUpImage.gameObject.SetActive(false);
            WakeUpPanel.gameObject.SetActive(false);
        }
    }
}