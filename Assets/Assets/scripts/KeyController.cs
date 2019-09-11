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
using UnityEngine.UI;

public class KeyController : MonoBehaviour
{
    // The gameobject / UI that has the instructions for the player "Press 'E' to interact."
    public GameObject instructions;
    public GameObject FEinstructions;

    public AudioSource keyAudio;
    public AudioClip[] soundToPlay;
    public chase c;
    public Image hasKeyImage;
    public bool hasKey = false;
    public GameObject keyTrigger;

    public Image hasFEImage;
    public bool hasFE = false;
    public GameObject FETrigger;



    void Start()
    {
        //audio = GetComponent<AudioSource>();
    }
    // As long as we are colliding with a trigger collider
    private void OnTriggerStay(Collider other)
    {
        // Check if the object has the tag 'Door'
        if (other.tag == "Key")
        {
            if (other.name == "KeyTrigger")
            {
                // Show the instructions
                instructions.SetActive(true);
                // Get the Animator from the child of the door (If you have the Animator component in the parent,
                // then change it to "GetComponent")
                //Animator anim = other.GetComponentInChildren<Animator>();
                // Check if the player hits the "E" key
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Picked up key");
                    //anim.SetTrigger("OpenCloseDoor"); //Set the trigger "OpenClose" which is in the Animator
                    //doorAudio.PlayOneShot(doorSound);
                    RandomKeyAudio();

                    hasKeyImage.gameObject.SetActive(true); //visual rep
                    hasKey = true; //for logic
                    Destroy(keyTrigger); //no physical key
                    instructions.SetActive(false); //no ins
                }
            }
            if (other.name == "FETrigger")
            {
                // Show the instructions
                FEinstructions.SetActive(true);
                // Get the Animator from the child of the door (If you have the Animator component in the parent,
                // then change it to "GetComponent")
                //Animator anim = other.GetComponentInChildren<Animator>();
                // Check if the player hits the "E" key
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Picked up FE");
                    //anim.SetTrigger("OpenCloseDoor"); //Set the trigger "OpenClose" which is in the Animator
                    //doorAudio.PlayOneShot(doorSound);
                    RandomKeyAudio();

                    hasFEImage.gameObject.SetActive(true); //visual rep
                    hasFE = true; //for logic
                    Destroy(FETrigger); //no physical key
                    FEinstructions.SetActive(false); //no ins
                }
            }

        }
    }

    // Once we exit colliding with a trigger collider
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Key")
        {
            // Hide instructions
            instructions.SetActive(false);
        }

        if (other.tag == "FE")
        {
            // Hide instructions
            FEinstructions.SetActive(false);
        }

    }
    void RandomKeyAudio()
    {
        if (keyAudio.isPlaying)
        {
            return;
        }
        keyAudio.clip = soundToPlay[Random.Range(0, soundToPlay.Length)];
        keyAudio.Play();
        c.chaseSpeed *= 1.1f; //make noise = increase speed

    }
}
