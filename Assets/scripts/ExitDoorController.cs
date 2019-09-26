/* 
* Created by: Omar Balfaqih (@OBalfaqih)
* http://obalfaqih.com
*
* Interacting with ExitDoors | Unity
*
* This simple script is to interact with ExitDoors (open/close) when the player presses "E"
*
* Full tutorial available at:
* https://www.youtube.com/watch?v=nONlAXpCkag
*/

/*
* How to use:
* 1- Attach this script to your player
* 2- Make sure that the player has Rigidbody and Collider components
* 3- The ExitDoor's parent has a collider with trigger checked
* 4- ExitDoor's parent has the tag "ExitDoor"
* 5- The ExitDoor itself has an Animator and it has a parameter of type trigger called "OpenClose"
*    which is responsible for the transition between opening and closing.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorController : MonoBehaviour
{
    // The gameobject / UI that has the instructions for the player "Press 'E' to interact."
    public GameObject instructions;
    public AudioSource ExitDoorAudio;
    public AudioSource closedExitDoorAudio;
    public AudioClip[] soundToPlay;
    public AudioClip[] closedExitDoorSoundToPlay;
    public chase c;
    public KeyController kc;
    private static readonly int OpenCloseDoor = Animator.StringToHash("OpenCloseDoor");


    void Start()
    {
        //audio = GetComponent<AudioSource>();
    }

    // As long as we are colliding with a trigger collider
    private void OnTriggerStay(Collider other)
    {
        // Check if the object has the tag 'ExitDoor'
        if (other.CompareTag("DoubleDoor"))
        {
            // Show the instructions
            instructions.SetActive(true);
            // Get the Animator from the child of the ExitDoor (If you have the Animator component in the parent,
            // then change it to "GetComponent")
            Animator anim = other.GetComponentInChildren<Animator>();
            // Check if the player hits the "E" key and the player has the key
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (kc.hasKey)
                {
                    anim.SetTrigger("OpenCloseDoor"); //Set the trigger "OpenClose" which is in the Animator
                    //ExitDoorAudio.PlayOneShot(ExitDoorSound);
                    RandomExitDoorAudio();
                    kc.hasKeyImage.gameObject.SetActive(false); //no visual rep
                }
                else
                {
                    RandomClosedExitDoorAudio();
                }
            }
        }

        if (!other.CompareTag("ExitDoor")) return;
        {
            // Show the instructions
            instructions.SetActive(true);
            // Get the Animator from the child of the ExitDoor (If you have the Animator component in the parent,
            // then change it to "GetComponent")
            Animator anim = other.GetComponentInChildren<Animator>();
            // Check if the player hits the "E" key and the player has the key
            if (!Input.GetKeyDown(KeyCode.E)) return;
            if (kc.hasKeyExit)
            {
                anim.SetTrigger(OpenCloseDoor); //Set the trigger "OpenClose" which is in the Animator
                //ExitDoorAudio.PlayOneShot(ExitDoorSound);
                RandomExitDoorAudio();
                kc.hasKeyImageExit.gameObject.SetActive(false); //no visual rep
            }
            else
            {
                RandomClosedExitDoorAudio();
            }
        }
    }

    // Once we exit colliding with a trigger collider
    private void OnTriggerExit(Collider other)
    {
        // Check it is a ExitDoor
        if (other.CompareTag("ExitDoor") || other.CompareTag("DoubleDoor"))
        {
            // Hide instructions
            instructions.SetActive(false);
        }
    }

    void RandomExitDoorAudio()
    {
        if (ExitDoorAudio.isPlaying)
        {
            return;
        }

        ExitDoorAudio.clip = soundToPlay[Random.Range(0, soundToPlay.Length)];
        ExitDoorAudio.Play();
        c.chaseSpeed *= 1.1f; //make noise = increase speedw
    }

    void RandomClosedExitDoorAudio()
    {
        if (closedExitDoorAudio.isPlaying)
        {
            return;
        }

        closedExitDoorAudio.clip = closedExitDoorSoundToPlay[Random.Range(0, closedExitDoorSoundToPlay.Length)];
        closedExitDoorAudio.Play();
        c.chaseSpeed *= 1.1f; //make noise = increase speedw
    }
}