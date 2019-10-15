using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    float vert;
    float horiz;
    float slideInput;
    public float grappleDistance = 10f;
    [SerializeField]
    float speed = 5;
    [SerializeField]
    float sprintSpeed = 8;
    [SerializeField]
    float strafeSpeed = 5;
    float speedMag;

    Transform cameraAnchor;
    Transform hookLaunchPoint;
    GameObject grapplingHook;
    public bool shooting;
    public bool grappled;
    public float grappleCD = 1f;
    public float grappleCDMax = 1f;
    public Transform grappleTarget;
    public float hookSpeed = 10;
    private Vector3 grappleDir;

    public float maxStamina = 1f;
    public float curStamina = 1f;
    public bool canSprint;

    Vector3 horizontalVelocity;
    Vector3 forwardVelocity;
    Vector3 verticalVelocity;

    BoxCollider playerBoxCollider;
    bool sliding;
    bool crouching;
    Vector3 originalSlideVelocity;
    float incrementX;
    float incrementZ;
    Rigidbody rb;
    bool grounded;
    bool sprintJumped;
    Vector3 velocityAtJump;
    public float jumpForce;

    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        cameraAnchor = GameObject.Find("CameraAnchor").transform;
        playerBoxCollider = GetComponent<BoxCollider>();
        canSprint = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        sliding = false;
        grappleCD = grappleCDMax;
        hookLaunchPoint = GameObject.Find("HookLaunchPoint").transform;
        grapplingHook = Resources.Load<GameObject>("Prefabs/GrapplingHook");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grappled)
        {
            moveToHook();
        }
        else
        {
            //Gets Vertical and horizontal input to use later
            vert = Input.GetAxis("Vertical");
            horiz = Input.GetAxis("Horizontal");
            slideInput = Input.GetAxis("Slide");


            grounded = Physics.Raycast(this.transform.position, -Vector3.up, out hit, 1.5f);
            if (grounded)
                sprintJumped = false;

            //Jumping
            if (Input.GetAxis("Jump") > 0.01 && grounded && !crouching)
            {
                if (Input.GetKey(KeyCode.LeftShift) && curStamina > 0 && grounded && canSprint && rb.velocity.magnitude > 0.3f)
                    sprintJumped = true;
                velocityAtJump = rb.velocity;
                speedMag = Vector3.Magnitude(new Vector3(rb.velocity.x, 0f, rb.velocity.z));
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.y);
            }

            //Sliding
            if (slideInput > 0.1f)
            {
                if (rb.velocity.magnitude > speed * 1.5f && grounded && !sliding && !crouching)
                {
                    originalSlideVelocity = rb.velocity;
                    incrementX = originalSlideVelocity.x * 0.7f;
                    incrementZ = originalSlideVelocity.z * 0.7f;
                    sliding = true;
                    rb.velocity = new Vector3(rb.velocity.x * 1.2f, rb.velocity.y,rb.velocity.z*1.2f);
                    cameraAnchor.position = new Vector3(cameraAnchor.position.x, cameraAnchor.position.y - 1, cameraAnchor.position.z);
                    playerBoxCollider.size = new Vector3(playerBoxCollider.size.x, 1, playerBoxCollider.size.z);
                    playerBoxCollider.center = new Vector3(playerBoxCollider.center.x, -0.5f, playerBoxCollider.center.z);

                }
                else if (!crouching && grounded && !sliding)
                {
                    cameraAnchor.position = new Vector3(cameraAnchor.position.x, cameraAnchor.position.y - 1, cameraAnchor.position.z);
                    playerBoxCollider.size = new Vector3(playerBoxCollider.size.x, 1, playerBoxCollider.size.z);
                    playerBoxCollider.center = new Vector3(playerBoxCollider.center.x, -0.5f, playerBoxCollider.center.z);
                    crouching = true;
                }
            }

            if (crouching && slideInput < 0.01)
            {
                cameraAnchor.position = new Vector3(cameraAnchor.position.x, cameraAnchor.position.y + 1, cameraAnchor.position.z);
                playerBoxCollider.size = new Vector3(playerBoxCollider.size.x, 2, playerBoxCollider.size.z);
                playerBoxCollider.center = new Vector3(playerBoxCollider.center.x, 0, playerBoxCollider.center.z);
                crouching = false;
            }

            if (sliding)
            {
                if (sliding && (slideInput < 0.01 || !grounded))
                {
                    cameraAnchor.position = new Vector3(cameraAnchor.position.x, cameraAnchor.position.y + 1, cameraAnchor.position.z);
                    playerBoxCollider.size = new Vector3(playerBoxCollider.size.x, 2, playerBoxCollider.size.z);
                    playerBoxCollider.center = new Vector3(playerBoxCollider.center.x, 0, playerBoxCollider.center.z);
                    sliding = false;
                }

                if (Vector3.Magnitude(rb.velocity) > 0.2f)
                {
                    rb.velocity = new Vector3(rb.velocity.x - incrementX * Time.deltaTime, rb.velocity.y, rb.velocity.z - incrementZ * Time.deltaTime);
                }
                else
                {
                    sliding = false;
                    crouching = true;
                }

            }
            else
            {
                //Sprinting
                if (Input.GetKey(KeyCode.LeftShift) && curStamina > 0 && grounded && canSprint && rb.velocity.magnitude > 0.3f || sprintJumped)
                {
                    curStamina -= 0.2f * Time.deltaTime; 
                    forwardVelocity = transform.forward * sprintSpeed * vert;
                }
                else
                {
                    if (curStamina < 0.2f)
                        canSprint = false;
                    else
                    {
                        canSprint = true;
                    }
                    if (curStamina < maxStamina && grounded)
                    {
                        curStamina += 0.2f * Time.deltaTime / 2;
                    }
                    forwardVelocity = transform.forward * speed * vert;
                }
                horizontalVelocity = transform.right * strafeSpeed * horiz;
                verticalVelocity = new Vector3(0, rb.velocity.y, 0);

                //Combines all velocitys to get final
                if (!crouching)
                {
                    rb.velocity = forwardVelocity + horizontalVelocity + verticalVelocity;
                }
                else if (crouching)
                {
                    rb.velocity = .5f * forwardVelocity + .5f * horizontalVelocity + verticalVelocity;
                }

                /*
                 * For Jumping
                float tempYVelocity = rb.velocity.y;
                rb.velocity = transform.forward * speedMag;
                rb.velocity = new Vector3(rb.velocity.x, tempYVelocity, rb.velocity.z);
                */


                //Shoot Grappling hook
                if (Input.GetAxis("Fire2") > 0.1 && !shooting && grappleCD >= grappleCDMax)
                {
                    ShootGrapplingHook();
                }

                if (!grappled && !shooting && grappleCD < grappleCDMax)
                {
                    grappleCD += Time.deltaTime;
                }
            }

        }
    }

    public void ShootGrapplingHook()
    {
        grappleCD = 0;
        setShooting(true);
        GameObject hook = Instantiate(grapplingHook, hookLaunchPoint.position, GameObject.Find("Main Camera").transform.rotation);
    }

    public void setShooting(bool value)
    {
        shooting = value;
    }

    public void moveToHook()
    {
        grappleDir = grappleTarget.position - transform.position;
        grappleDir = grappleDir / grappleDir.magnitude;
        rb.velocity = grappleDir * hookSpeed;
       
    }
}
