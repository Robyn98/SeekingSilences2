using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class AmbientSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource AS;
    [SerializeField] private float Vo = 1f;
    [SerializeField] private float Vn = 0.3f;
    [SerializeField] private float curr;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("LowerSound"))
        {
            StartCoroutine(dec());
        }
    }


    // Once we exit colliding with a trigger collider
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LowerSound"))
        {
            StartCoroutine(inc());
        }
    }


    public IEnumerator dec()
    {
        while (curr >= Vn)
        {
            yield return new WaitForSeconds(0.2f);
            AS.volume = curr;
            curr -= 0.1f;
        }

        //curr = Vo;
    }

    public IEnumerator inc()
    {
        while (curr <= Vo)
        {
            yield return new WaitForSeconds(0.2f);
            AS.volume = curr;
            curr += 0.1f;
        }

        //curr = Vo;
    }
}