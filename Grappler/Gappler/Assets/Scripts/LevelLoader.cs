using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public GameObject[] panels;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadPlay() {
        Application.LoadLevel(1);
    }

    public void LoadTitleScreen() {
        Application.LoadLevel(0);
    }


    public void SetActivePanel(int n) {
        for (int i = 0; i < panels.Length; i++) {
            if (i == n)
                panels[i].SetActive(true);
            else {
                panels[i].SetActive(false);
            }
        }
    }

}
