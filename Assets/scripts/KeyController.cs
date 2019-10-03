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
    public GameObject Keyinstructions;

    

    public AudioSource keyAudio;
    public AudioClip[] soundToPlay;
    public chase c;
    
    public Image hasKeyImage;
    public bool hasKey = false;
    public GameObject KeyTrigger;
    
    public GameObject KeyinstructionsExit;
    public Image hasKeyImageExit;
    public bool hasKeyExit = false;
    public GameObject KeyTriggerExit;
    [SerializeField] private AudioSource AS;
    [SerializeField] private AudioClip run;
    [SerializeField] private GameObject runHint;


    void Start()
    {
        //AS = GetComponent<AudioSource>();
    }
    // As long as we are colliding with a trigger collider
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "TriggerKey")
        {
           
            // Show the instructions
            Keyinstructions.SetActive(true);
        
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Picked up Key");
                
                RandomKeyAudio();
                
                hasKeyImage.gameObject.SetActive(true); //visual rep
                hasKey = true; //for logic
                Destroy(KeyTrigger); //no physical key
                Keyinstructions.SetActive(false); //no ins
            }
        }

        if (other.name != "TriggerKeyExit") return;
        // Show the instructions
        Keyinstructions.SetActive(true);

        if (!Input.GetKeyDown(KeyCode.E)) return;
        Debug.Log("Picked up Key for exit");
                
        RandomKeyAudio();
        AS.PlayOneShot(run);
        
        StartCoroutine(StartCountdown(15f,runHint));
        
        hasKeyImageExit.gameObject.SetActive(true); //visual rep
        hasKeyExit = true; //for logic
        Destroy(KeyTriggerExit); //no physical key
        KeyinstructionsExit.SetActive(false); //no ins

    }


    // Once we exit colliding with a trigger collider
    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Key"))
        {
            // Hide instructions
            Keyinstructions.SetActive(false);
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
 public IEnumerator StartCountdown(float countdownValue, GameObject go)
 {
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
