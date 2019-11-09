using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class JumpIns : MonoBehaviour
{
    [SerializeField] private GameObject jumpIns;
    private bool hit = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!hit)
        {
            StartCoroutine(StartCountdown(5f, jumpIns, 0f));
            hit = true;
        }
        
    }
    
    private float currCountdownValue;

    public IEnumerator StartCountdown(float countdownValue, GameObject go, float beforeWait)
    {
        yield return new WaitForSeconds(beforeWait);

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
