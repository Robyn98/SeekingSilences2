﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOffOnPlay : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        gameObject.SetActive(!Application.isPlaying);
    }

}
