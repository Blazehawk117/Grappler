using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            GameObject.Find("Timer").GetComponent<Timer>().ticking = false;
            GameObject.Find("Display Text").GetComponent<Text>().text = "Press E to go to exit level";
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E)) 
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Application.LoadLevel(2);
        }
    }
}
