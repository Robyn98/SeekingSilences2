/* 
* Created by: Omar Balfaqih (@OBalfaqih)
* http://obalfaqih.com
*
* Interacting with Doors | Unity
*
* This simple script is to interact with doors (open/close) when the player presses "E"
*
* Full tutorial available at:
* https://www.youtube.com/watch?v=nONlAXpCkag
*/

/*
* How to use:
* 1- Attach this script to your player
* 2- Make sure that the player has Rigidbody and Collider components
* 3- The door's parent has a collider with trigger checked
* 4- Door's parent has the tag "Door"
* 5- The door itself has an Animator and it has a parameter of type trigger called "OpenClose"
*    which is responsible for the transition between opening and closing.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // The gameobject / UI that has the instructions for the player "Press 'E' to interact."
    public GameObject instructions;
    public AudioSource doorAudio;
    public AudioSource closedDoorAudio;
    public AudioClip[] soundToPlay;
    public AudioClip[] closedDoorSoundToPlay;
    public chase c;
    public KeyController kc;


    void Start()
    {
        //audio = GetComponent<AudioSource>();
    }
    // As long as we are colliding with a trigger collider
    private void OnTriggerStay(Collider other)
    {
        // Check if the object has the tag 'Door'
        if (other.tag == "Door")
        {
            // Show the instructions
            instructions.SetActive(true);
            // Get the Animator from the child of the door (If you have the Animator component in the parent,
            // then change it to "GetComponent")
            Animator anim = other.GetComponentInChildren<Animator>();
            // Check if the player hits the "E" key and the player has the key
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(kc.hasKey)
                {
                    anim.SetTrigger("OpenCloseDoor"); //Set the trigger "OpenClose" which is in the Animator
                                                      //doorAudio.PlayOneShot(doorSound);
                    RandomDoorAudio();
                    kc.hasKeyImage.gameObject.SetActive(false); //no visual rep
                }
                else
                {
                    RandomClosedDoorAudio();
                }
                
            }
            
                
        }
    }

    // Once we exit colliding with a trigger collider
    private void OnTriggerExit(Collider other)
    {
        // Check it is a door
        if (other.tag == "Door")
        {
            // Hide instructions
            instructions.SetActive(false);
        }
    }
    void RandomDoorAudio()
    {
        if (doorAudio.isPlaying)
        {
            return;
        }
        doorAudio.clip = soundToPlay[Random.Range(0, soundToPlay.Length)];
        doorAudio.Play();
        c.chaseSpeed *= 1.1f; //make noise = increase speedw

    }
    void RandomClosedDoorAudio()
    {
        if (closedDoorAudio.isPlaying)
        {
            return;
        }
        closedDoorAudio.clip = closedDoorSoundToPlay[Random.Range(0, closedDoorSoundToPlay.Length)];
        closedDoorAudio.Play();
        c.chaseSpeed *= 1.1f; //make noise = increase speedw

    }
}

