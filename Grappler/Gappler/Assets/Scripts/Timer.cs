using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public bool ticking;
    float time;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        ticking = true;
        time = 0;
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ticking)
        {
            time += Time.deltaTime;
            text.text = String.Format("{0:0.00}", time);
        }

    }
}
