using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookChainRenderer : MonoBehaviour
{
    LineRenderer lr;
    Transform playerPos;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0,playerPos.position);
        lr.SetPosition(1,transform.position);
    }
}
