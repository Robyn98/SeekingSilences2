

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
    
    private float currCountdownValue;
    
    [SerializeField] private GameObject Key2Hint;
    
    // As long as we are colliding with a trigger collider
    private void OnTriggerStay(Collider other)
    {
        // Check if the object has the tag 'Box'
        if (other.CompareTag("Box"))
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
                    //Intercom 3
                    if (!firstBoxOpen)
                    {
                        AS.PlayOneShot(Thud);
                        firstBoxOpen = true;
                        StartCoroutine(StartCountdown(5f, Key2Hint,true));
                    }
                }
                else
                {
                    RandomClosedBoxAudio();
                }


            }


        }
    }

    // Once we exit colliding with a trigger collider
    private void OnTriggerExit(Collider other)
    {
        // Check it is a Box
        if (other.CompareTag("Box"))
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

