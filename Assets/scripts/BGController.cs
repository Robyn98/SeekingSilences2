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


public class BGController : MonoBehaviour
{
    // The gameobject / UI that has the instructions for the player "Press 'E' to interact."
    public GameObject BGinstructions;

    public AudioSource keyAudio;
    public AudioClip[] soundToPlay;
    public chase c;
    
    public Image hasBGImage;
    public bool hasBG = false;
    public GameObject BGTrigger;
    [SerializeField] private GameObject BGHint;


    void Start()
    {
        //audio = GetComponent<AudioSource>();
    }
    // As long as we are colliding with a trigger collider
    private void OnTriggerStay(Collider other)
    {


        if (other.name == "TriggerBG")
        {
            // Show the instructions
            BGinstructions.SetActive(true);
            // Get the Animator from the child of the door (If you have the Animator component in the parent,
            // then change it to "GetComponent")
            //Animator anim = other.GetComponentInChildren<Animator>();
            // Check if the player hits the "E" key
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Picked up BG");
                //anim.SetTrigger("OpenCloseDoor"); //Set the trigger "OpenClose" which is in the Animator
                //doorAudio.PlayOneShot(doorSound);
                RandomKeyAudio();
                StartCoroutine(StartCountdown(5f, BGHint,true));
                hasBGImage.gameObject.SetActive(true); //visual rep
                hasBG = true; //for logic
                Destroy(BGTrigger); //no physical key
                BGinstructions.SetActive(false); //no ins
            }
        }

    }


    // Once we exit colliding with a trigger collider
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "BG")
        {
            // Hide instructions
            BGinstructions.SetActive(false);
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
    
    private float currCountdownValue;
    public IEnumerator StartCountdown(float countdownValue, GameObject go, bool beforeWait)
    {
        if (beforeWait)
        {
            yield return new WaitForSeconds(5.0f);
        }
        
        go.gameObject.SetActive(true);
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            //Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);

            currCountdownValue--;
        }

        go.gameObject.SetActive(false);
    }
}
