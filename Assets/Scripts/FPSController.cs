using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public CameraController cameraScript;

    bool crouchToggled;
    public bool isCrouching;


    readonly float colliderNormalHeight = 2f;
    readonly float colliderCrouchHeight = 1f;
    readonly float colliderNormalCenterY = 0f;
    readonly float colliderCrouchCenterY = -0.5f;


    readonly float jumpForce = 17000f;
    readonly float crouchSpeed = 450f;
    readonly float walkSpeed = 700f;
    readonly float sprintSpeed = 1200f;
    readonly float lookSpeed = 2.0f;
    readonly float rotationSpeed = 100f;
    float movementSpeed;
    float speedBeforeCrouching;

    
    public GameObject fpHead;
    public GameObject tpHead;
    public GameObject characterModel;

    private float rY, rX; // X is left right, Y is up down


    Rigidbody rb;
    CapsuleCollider cc;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
    }

    void FixedUpdate()
    {
        //Movement
        MoveCharacter(MovementOutput(cameraScript.activeCamera, Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));     
    }

    // Update is called once per frame
    void Update()
    {

        //Vision
        LookCharacter(cameraScript.activeCamera);

        //Sprint
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            movementSpeed = sprintSpeed;
            
        }
        else if (!isCrouching)
        {
            movementSpeed = walkSpeed;
            
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, Vector3.down, 1.5f))
        {
            rb.AddForce(Vector3.up * jumpForce);

        }

        //Crouch
        if (Input.GetKeyDown(KeyCode.C) && !crouchToggled)
        {
            Crouch();
            crouchToggled = true;
            isCrouching = true;

        }
        else if (Input.GetKeyDown(KeyCode.C) && crouchToggled)
        {
            Decrouch();
            crouchToggled = false;
            isCrouching = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
            isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Decrouch();
            crouchToggled = false;
            isCrouching = false;
        }

        //Camera
        if (Input.GetKeyDown(KeyCode.V))
        {
            if(cameraScript.ActiveCamera(false))
            {
                fpHead.transform.localRotation = Quaternion.Euler(0, fpHead.transform.localRotation.y, 0);
            }
            cameraScript.EnableCamera(cameraScript.ActiveCamera(true));
        }

        //Sliding
        if (isCrouching)
        {
            if (movementSpeed > 1 && movementSpeed < 1.6)
            {
                movementSpeed = crouchSpeed;
                speedBeforeCrouching = 0;
            }
            else if (speedBeforeCrouching == sprintSpeed)
            {
                //rb.velocity = Vector3.MoveTowards(rb.velocity, rb.velocity.normalized * crouchSpeed, 50f * Time.deltaTime);
                movementSpeed = Mathf.Lerp(movementSpeed, crouchSpeed, 2f * Time.deltaTime);
            }
            else
            {
                movementSpeed = crouchSpeed;
            }
        }
        
    }

    
    public void LookCharacter(GameObject actCam)
    {

        if (actCam == cameraScript.fpCamera) //fpCam
        {
            rY += -Input.GetAxis("Mouse Y");
            rX += Input.GetAxis("Mouse X");
            rY = Mathf.Clamp(rY, -40f, 40f);
            transform.eulerAngles = new Vector2(0, rX) * lookSpeed;
            cameraScript.fpCamera.transform.localRotation = Quaternion.Euler(rY * lookSpeed, 0, 0);
        }
        else if (actCam == cameraScript.tpCamera) //tpCam
        {
            rY += -Input.GetAxis("Mouse Y");
            rX += Input.GetAxis("Mouse X");
            rY = Mathf.Clamp(rY, -40f, 40f);
            tpHead.transform.localRotation = Quaternion.Euler(rY * lookSpeed, rX * lookSpeed, 0);
        }

        cameraScript.CameraAction();
    }

    public Vector3 MovementOutput(GameObject actCam, float inX, float inY) //inX is horizontal input, inY is vertical. No correllation to 3D coordinates.
    {
        if (actCam == cameraScript.fpCamera)
        {
            return (Vector3.Normalize((this.transform.forward * inY) + (this.transform.right * inX)) * movementSpeed);
        }
        else if (actCam == cameraScript.tpCamera)
        {
            /*return (Vector3.Normalize((actCam.transform.forward * inY) + (actCam.transform.right * inX)) * movementSpeed);*/  //Non Corrected Version of following line (does not cancel vertical component of movement vector)
            return (Vector3.Normalize((new Vector3(actCam.transform.forward.x, 0, actCam.transform.forward.z) * inY) + (new Vector3(actCam.transform.right.x, 0, actCam.transform.right.z) * inX)) * movementSpeed);
        }
        else
        {
            return Vector3.zero;
        }
        
    }

    public void MoveCharacter(Vector3 direction)
    {
        rb.AddForce(direction);
    }



    //replace with reference to camera script
    public void Crouch()
    {
        speedBeforeCrouching = movementSpeed;
        cc.height = colliderCrouchHeight;
        cc.center = new Vector3(0, colliderCrouchCenterY, 0);
        cameraScript.ToggleCameraCrouch(true);
    }
    public void Decrouch()
    {
        
        movementSpeed = walkSpeed;
        cc.height = colliderNormalHeight;
        cc.center = new Vector3(0, colliderNormalCenterY, 0);
        cameraScript.ToggleCameraCrouch(false);
    }
}
