using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPointScript : MonoBehaviour
{
    GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
