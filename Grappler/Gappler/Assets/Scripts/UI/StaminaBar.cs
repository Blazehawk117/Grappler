using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StaminaBar : MonoBehaviour
{
    Slider cdBar;
    PlayerControl playerControlReference;
    Image img;
    Color originalColor;
    public Color cantSprintColor;
    // Start is called before the first frame update
    void Start()
    {
        cdBar = GetComponent<Slider>();
        playerControlReference = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        img = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        originalColor = img.color;
    }

    // Update is called once per frame
    void Update()
    {
        cdBar.value = playerControlReference.curStamina / playerControlReference.maxStamina;
        Debug.Log(cdBar.value);
        if (playerControlReference.canSprint)
        {
            img.color = originalColor;
        }
        else
        {
            img.color = cantSprintColor;
        }
    }
}
