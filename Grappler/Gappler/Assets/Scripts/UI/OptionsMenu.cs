using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    bool optionsActive;
    float optionsCDMax;
    float optionsCDCurrent;
    public GameObject options;
    
    // Start is called before the first frame update
    void Start()
    {
        optionsCDMax = 0.2f;
        optionsCDCurrent = optionsCDMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (optionsCDCurrent < optionsCDMax)
            optionsCDCurrent += Time.deltaTime;

        if (Input.GetAxis("Cancel") > 0.1 && optionsCDCurrent >= optionsCDMax)
        {
            optionsActive = !optionsActive;
            optionsCDCurrent = 0;
            options.gameObject.SetActive(optionsActive);

            if (optionsActive)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

    }
}
