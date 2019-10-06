using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrappleCDBar : MonoBehaviour
{
    Slider cdBar;
    PlayerControl playerControlReference;
    Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        cdBar = GetComponent<Slider>();
        playerControlReference = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        cdBar.value = playerControlReference.grappleCD / playerControlReference.grappleCDMax;
        if (cdBar.value >= 1)
        {
            img.enabled = false;
        }
        else {
            img.enabled = true;
        }
    }
}
