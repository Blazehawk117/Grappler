using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    PlayerControl playerControlReference;
    Transform playerPos;
    GameObject player;
    bool returning;
    bool grappled;
    Rigidbody rb;
    Rigidbody playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        returning = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform;
        playerControlReference = player.GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grappled)
        {
            rb.velocity = Vector3.zero;
        }
        else if (!grappled && !returning && Vector3.Distance(transform.position, playerPos.position) > playerControlReference.grappleDistance)
        {
            returning = true;
        }
        else if (returning)
        {
            transform.LookAt(player.transform);
            rb.velocity = transform.forward * playerControlReference.hookSpeed * 4;
        }
        else if (!returning)
        {
            rb.velocity = transform.forward * playerControlReference.hookSpeed;
        }

        if ((grappled || returning) && Vector3.Distance(transform.position, playerPos.position) < 1.5 || (Input.GetAxis("Fire2") > 0.1 && grappled))
        {
            playerControlReference.grappled = false;
            playerControlReference.setShooting(false);
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            grappled = true;
            playerControlReference.grappled = true;
            playerControlReference.grappleTarget = this.transform;
        }
        else {
            returning = true;
        }
    }
}
