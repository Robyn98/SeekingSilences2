using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roamingStart : MonoBehaviour {
    
    private bool entered = false;
    [SerializeField] private GameObject doctor;
    [SerializeField] private AudioSource AS;
    [SerializeField] private AudioClip doctorDoorOpen;
    [SerializeField] private AudioClip comingToGetYou;
    [SerializeField] private GameObject bigDoor;
    private static readonly int OpenCloseDoor = Animator.StringToHash("OpenCloseDoor");
    private void OnTriggerEnter (Collider player) {
        //Debug.Log("Entered: "+ entered);
        if (!player.CompareTag("Player") || entered) return;
        entered = true;
        
        //is activated
        doctor.gameObject.SetActive(true);
        //sound 
        AS.PlayOneShot(comingToGetYou);
        
        //doors open
        var anim = bigDoor.GetComponentInChildren<Animator>();
        StartCoroutine(StartCountdown(25f, anim));

    }

    public IEnumerator StartCountdown(float countdownValue, Animator anim)
    {
        
        anim.SetTrigger("OpenCloseDoor"); 
        yield return new WaitForSeconds(countdownValue);
        anim.SetTrigger("OpenCloseDoor"); 
    }

}