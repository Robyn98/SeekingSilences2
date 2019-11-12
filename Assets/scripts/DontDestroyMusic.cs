using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyMusic : MonoBehaviour
{
    Scene currentScene = SceneManager.GetActiveScene();
    [SerializeField] private AudioSource myAudio;

    void Awake()
    {
        DontDestroyOnLoad(myAudio);
    }
}