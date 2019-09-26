using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpscare : MonoBehaviour {

    public GameObject jumpscareObject;
    private bool entered = false;
    private bool done = false;
    [SerializeField] private float translationMultiplier = 15f;
    [SerializeField] private float deleteTime = 2.5f;
    private void Start () {

        jumpscareObject.SetActive(false);
    }

    private void Update()
    {
        if (!entered || done) return;
        // Move the object forward along its z axis 1 unit/second.
        jumpscareObject.transform.Translate(translation: translationMultiplier * Time.deltaTime * Vector3.forward);
        
    }

    private void OnTriggerEnter (Collider player) {
        Debug.Log("Entered: "+ entered);
        if (!player.CompareTag("Player") || entered) return;
        entered = true;
        jumpscareObject.SetActive(true);
        StartCoroutine(DestroyObject());
        
        //Debug.Log("Entered");
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(deleteTime);
        Destroy(jumpscareObject);
        Destroy(gameObject);
        done = true;
    }
}