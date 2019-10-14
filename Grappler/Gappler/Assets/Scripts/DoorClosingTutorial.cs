using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClosingTutorial : MonoBehaviour
{
    [SerializeField]
    float doorSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 12)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - doorSpeed, transform.position.z);
        }
    }
}
