using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    public GameObject objectToReset;
    Transform objectAtStart;
    public Text displayText;

    // Start is called before the first frame update
    void Start()
    {
        objectAtStart = objectToReset.transform;
        displayText = GameObject.Find("Display Text").GetComponent<Text>();
    }

    void resetObject() {
        objectToReset.transform.position = new Vector3(objectToReset.transform.position.x,29,objectToReset.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        displayText.text = "Press E to reset Door";   
    }

    private void OnTriggerExit(Collider other)
    {

        displayText.text = "";
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E)) {

            displayText.text = "Door Reset!";
            resetObject();
        }
    }
}
