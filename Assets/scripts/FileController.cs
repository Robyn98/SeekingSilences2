/* 
* Created by: Omar Balfaqih (@OBalfaqih)
* http://obalfaqih.com
*
* Interacting with Files | Unity
*
* This simple script is to interact with Files (open/close) when the player presses "E"
*
* Full tutorial available at:
* https://www.youtube.com/watch?v=nONlAXpCkag
*/

/*
* How to use:
* 1- Attach this script to your player
* 2- Make sure that the player has Rigidbody and Collider components
* 3- The File's parent has a collider with trigger checked
* 4- File's parent has the tag "File"
* 5- The File itself has an Animator and it has a parameter of type trigger called "OpenClose"
*    which is responsible for the transition between opening and closing.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileController : MonoBehaviour
{
    // The gameobject / UI that has the instructions for the player "Press 'E' to interact."
    public GameObject instructions;
    public AudioSource FileAudio;
    public AudioSource closedFileAudio;
    public AudioClip[] soundToPlay;
    public AudioClip[] closedFileSoundToPlay;
    public chase c;
    public Image FileContent;
    public bool fileOpen = false;
    
    //public KeyController kc;


    void Start()
    {
        //audio = GetComponent<AudioSource>();
        //anim = GetComponent<Animator>();
    }
    // As long as we are colliding with a trigger collider
    private void OnTriggerStay(Collider other)
    {
        // Check if the object has the tag 'File'
        if (other.tag == "File")
        {
            // Show the instructions
            instructions.SetActive(true);
            // Get the Animator from the child of the File (If you have the Animator component in the parent,
            // then change it to "GetComponent")
            Animator anim = other.GetComponentInChildren<Animator>();
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                fileOpen = !fileOpen;
                if(fileOpen)
                {
                    FileContent.gameObject.SetActive(true);
                }
                else
                {
                    FileContent.gameObject.SetActive(false);
                }
                anim.SetTrigger("OpenCloseFile"); //Set the trigger "OpenClose" which is in the Animator
                                                  //FileAudio.PlayOneShot(FileSound);
                RandomFileAudio();
               // kc.hasKeyImage.gameObject.SetActive(false); //no visual rep



            }


        }
    }

    // Once we exit colliding with a trigger collider
    private void OnTriggerExit(Collider other)
    {
        // Check it is a File
        if (other.tag == "File")
        {
            // Hide instructions
            instructions.SetActive(false);
            
            
            fileOpen = !fileOpen;
            if (!fileOpen)
            {
                FileContent.gameObject.SetActive(false);
            }
            other.GetComponentInChildren<Animator>().SetTrigger("OpenCloseFile");

        }
    }
    void RandomFileAudio()
    {
        if (FileAudio.isPlaying)
        {
            return;
        }
        FileAudio.clip = soundToPlay[Random.Range(0, soundToPlay.Length)];
        FileAudio.Play();
        c.chaseSpeed *= 1.1f; //make noise = increase speedw

    }
    void RandomClosedFileAudio()
    {
        if (closedFileAudio.isPlaying)
        {
            return;
        }
        closedFileAudio.clip = closedFileSoundToPlay[Random.Range(0, closedFileSoundToPlay.Length)];
        closedFileAudio.Play();
        c.chaseSpeed *= 1.1f; //make noise = increase speedw

    }
}

