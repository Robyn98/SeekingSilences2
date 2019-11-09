/* 
* Created by: Omar Balfaqih (@OBalfaqih)
* http://obalfaqih.com
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
    public FEController fec;
    private bool firstDoorOpen = false;
    public AudioSource AS;
    public AudioClip SIR;
    public AudioClip BreakSound;
    private static readonly int OpenCloseDoor = Animator.StringToHash("OpenCloseDoor");

    private bool ER1DT = false;
    private bool FETR= false;

    //private bool TRDT = false;
    [SerializeField] private AudioClip DoorSlamSound;

    [SerializeField] private GameObject FEHint;
    [SerializeField] private GameObject Key1Hint;

    // As long as we are colliding with a trigger collider
    private void OnTriggerStay(Collider other)
    {
        // Check if the object has the tag 'Door'
        if (!other.CompareTag("Door")) return;
        // Show the instructions
        instructions.SetActive(true);
        // Get the Animator from the child of the door (If you have the Animator component in the parent,
        // then change it to "GetComponent")
        var anim = other.GetComponentInChildren<Animator>();
        // Check if the player hits the "E" key and the player has the key
        if (!Input.GetKeyDown(KeyCode.E)) return;
        if (fec.hasFE)
        {
            anim.SetTrigger(OpenCloseDoor); //Set the trigger "OpenClose" which is in the Animator
            //doorAudio.PlayOneShot(doorSound);
            if (!FETR)
            {
                doorAudio.PlayOneShot(BreakSound);
                FETR = true;
            }
            else
            {
                RandomDoorAudio();
            }
            
            fec.hasFEImage.gameObject.SetActive(false); //no visual rep
            //Intercom 2
            if (firstDoorOpen) return;
            AS.PlayOneShot(SIR);
            firstDoorOpen = true;
            StartCoroutine(StartCountdown(5f, Key1Hint,true));
        }
        else
        {
            RandomClosedDoorAudio();
            StartCoroutine(StartCountdown(5f, FEHint,false));
        }
    }

    // Once we exit colliding with a trigger collider
    private void OnTriggerExit(Collider other)
    {
        // Check it is a door
        if (other.CompareTag("Door"))
        {
            // Hide instructions
            instructions.SetActive(false);

            //if first time
            if (!ER1DT && other.name == "R1DT")
            {
                var anim = other.GetComponentInChildren<Animator>();
                anim.SetTrigger(OpenCloseDoor);
                AS.PlayOneShot(DoorSlamSound);
                ER1DT = true;
            }
        }
    }

    private void RandomDoorAudio()
    {
        if (doorAudio.isPlaying)
        {
            return;
        }

        doorAudio.clip = soundToPlay[Random.Range(0, soundToPlay.Length)];
        doorAudio.Play();
        c.chaseSpeed *= 1.1f; //make noise = increase speed
    }

    private void RandomClosedDoorAudio()
    {
        if (closedDoorAudio.isPlaying)
        {
            return;
        }

        closedDoorAudio.clip = closedDoorSoundToPlay[Random.Range(0, closedDoorSoundToPlay.Length)];
        closedDoorAudio.Play();
        c.chaseSpeed *= 1.1f; //make noise = increase speed
    }

    private float currCountdownValue;

    public IEnumerator StartCountdown(float countdownValue, GameObject go, bool beforeWait)
    {
        if (beforeWait)
        {
            yield return new WaitForSeconds(10.0f);
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