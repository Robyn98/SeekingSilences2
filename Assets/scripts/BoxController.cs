/* 
* Created by: Omar Balfaqih (@OBalfaqih)
* http://obalfaqih.com
*
* Interacting with Boxs | Unity
*
* This simple script is to interact with Boxs (open/close) when the player presses "E"
*
* Full tutorial available at:
* https://www.youtube.com/watch?v=nONlAXpCkag
*/

/*
* How to use:
* 1- Attach this script to your player
* 2- Make sure that the player has Rigidbody and Collider components
* 3- The Box's parent has a collider with trigger checked
* 4- Box's parent has the tag "Box"
* 5- The Box itself has an Animator and it has a parameter of type trigger called "OpenClose"
*    which is responsible for the transition between opening and closing.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    // The gameobject / UI that has the instructions for the player "Press 'E' to interact."
    public GameObject instructions;
    public AudioSource BoxAudio;
    public AudioSource closedBoxAudio;
    public AudioClip[] soundToPlay;
    public AudioClip[] closedBoxSoundToPlay;
    public chase c;
    public BGController bgc;
    public GameObject BoxTrigger;
    public GameObject KeyTrigger;
    private bool firstBoxOpen = false;
    public AudioSource AS;
    public AudioClip Thud;

    void Start()
    {
        //audio = GetComponent<AudioSource>();
    }
    // As long as we are colliding with a trigger collider
    private void OnTriggerStay(Collider other)
    {
        // Check if the object has the tag 'Box'
        if (other.tag == "Box")
        {
            // Show the instructions
            instructions.SetActive(true);
            // Get the Animator from the child of the Box (If you have the Animator component in the parent,
            // then change it to "GetComponent")
            Animator anim = other.GetComponentInChildren<Animator>();
            // Check if the player hits the "E" key and the player has the key
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (bgc.hasBG)
                {
                    //anim.SetTrigger("OpenCloseBox"); //Set the trigger "OpenClose" which is in the Animator
                                                      //BoxAudio.PlayOneShot(BoxSound);
                    RandomBoxAudio();
                    bgc.hasBGImage.gameObject.SetActive(false); //no visual rep
                    //destroy box and ins
                    Destroy(BoxTrigger);
                    instructions.SetActive(false);
                    KeyTrigger.gameObject.SetActive(true); //show key
                }
                else
                {
                    RandomClosedBoxAudio();
                }
                //Intercom 3
                if (!firstBoxOpen)
                {
                    AS.PlayOneShot(Thud);
                    firstBoxOpen = true;
                }

            }


        }
    }

    // Once we exit colliding with a trigger collider
    private void OnTriggerExit(Collider other)
    {
        // Check it is a Box
        if (other.tag == "Box")
        {
            // Hide instructions
            instructions.SetActive(false);
        }
    }
    void RandomBoxAudio()
    {
        if (BoxAudio.isPlaying)
        {
            return;
        }
        BoxAudio.clip = soundToPlay[Random.Range(0, soundToPlay.Length)];
        BoxAudio.Play();
        c.chaseSpeed *= 1.1f; //make noise = increase speedw

    }
    void RandomClosedBoxAudio()
    {
        if (closedBoxAudio.isPlaying)
        {
            return;
        }
        closedBoxAudio.clip = closedBoxSoundToPlay[Random.Range(0, closedBoxSoundToPlay.Length)];
        closedBoxAudio.Play();
        c.chaseSpeed *= 1.1f; //make noise = increase speedw

    }
}

