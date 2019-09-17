﻿using UnityEngine;
using System.Collections;

public class ThrowObject : MonoBehaviour
{

    public chase c;
    public Transform player;
    public Transform playerCam;
    public float throwForce = 10;
    bool hasPlayer = false;
    bool beingCarried = false;
    public AudioClip[] soundToPlay;
    private AudioSource audio;
    public AudioClip[] hitEnv;
    public int dmg;
    private bool touched = false;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, player.position);
        if (dist <= 4.5f)
        {
            hasPlayer = true;
        }
        else
        {
            hasPlayer = false;
        }
        if (hasPlayer && Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = playerCam;
            beingCarried = true;
        }
        if (beingCarried)
        {
            if (touched)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce);
                RandomAudio();
                c.chaseSpeed *= 1.1f;
                //Debug.Log(c.chaseSpeed);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
            }
        }
    }
    void RandomAudio()
    {
        if (audio.isPlaying)
        {
            return;
        }
        audio.clip = soundToPlay[Random.Range(0, soundToPlay.Length)];
        audio.Play();

    }
    void RandomAudioHitEnv()
    {
        if (audio.isPlaying)
        {
            return;
        }
        audio.clip = hitEnv[Random.Range(0, soundToPlay.Length+1)];
        audio.Play();

    }
    void OnTriggerEnter(Collider other)
    {
        if (beingCarried && other.tag != "Door")
        {
            //Debug.Log("Not hitting door");
            Debug.Log("Tag: "+ other.tag);

            RandomAudioHitEnv();
            touched = true;
            
            c.chaseSpeed *=1.1f;
            //Debug.Log(c.chaseSpeed);
            
        }
    }
}