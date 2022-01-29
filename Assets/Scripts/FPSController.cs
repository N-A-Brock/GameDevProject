using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController : MonoBehaviour
{
    public CameraController cameraScript;

    bool crouchToggled;
    public bool isCrouching;


    public InputAction moveAction;


    readonly float COLLIDER_NORMAL_HEIGHT = 2f;
    readonly float COLLIDER_CROUCH_HEIGHT = 1f;
    readonly float COLLIDER_NORMAL_CENTER_Y = 0f;
    readonly float COLLIDER_CROUCH_CENTER_Y = -0.5f;


    readonly float JUMP_FORCE = 6f;
    readonly float CROUCH_SPEED = 450f;
    readonly float WALK_SPEED = 700f;
    readonly float SPRINT_SPEED = 1200f;
    readonly float LOOK_SPEED = 2.0f;
    readonly float ROTATION_SPEED = 0.6f;
    float movementSpeed;
    float speedBeforeCrouching;

    private float rY, rX; // X is left right, Y is up down


    Rigidbody rb;
    CapsuleCollider cc;
    // Start is called before the first frame update
    void Start()
    {
        EnableActions();
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
        Debug.Log(moveAction.ReadValue<Vector2>());
        //Vision
        LookCharacter(cameraScript.activeCamera);

        //Sprint
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            movementSpeed = SPRINT_SPEED;
            
        }
        else if (!isCrouching)
        {
            movementSpeed = WALK_SPEED;
            
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, Vector3.down, 1.5f))
        {
            rb.velocity += (Vector3.up * JUMP_FORCE);

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
            cameraScript.EnableCamera(cameraScript.ActiveCamera(true));
        }

        //Sliding
        if (isCrouching)
        {
            if (movementSpeed > 1 && movementSpeed < 1.6)
            {
                movementSpeed = CROUCH_SPEED;
                speedBeforeCrouching = 0;
            }
            else if (speedBeforeCrouching == SPRINT_SPEED)
            {
                //rb.velocity = Vector3.MoveTowards(rb.velocity, rb.velocity.normalized * CROUCH_SPEED, 50f * Time.deltaTime);
                movementSpeed = Mathf.Lerp(movementSpeed, CROUCH_SPEED, 2f * Time.deltaTime);
            }
            else
            {
                movementSpeed = CROUCH_SPEED;
            }
        }
        
    }

    
    public void LookCharacter(GameObject actCam)
    {

        rY += -Input.GetAxis("Mouse Y");
        rX += Input.GetAxis("Mouse X");
        rY = Mathf.Clamp(rY, -40f, 40f);
        if (actCam == cameraScript.fpCamera)
        {
            transform.eulerAngles = new Vector2(0, rX) * LOOK_SPEED;
        }
        cameraScript.cameraAssembly.transform.eulerAngles = new Vector2(0, rX) * LOOK_SPEED;
        cameraScript.fpCamera.transform.localRotation = Quaternion.Euler(rY * LOOK_SPEED, 0, 0);
        cameraScript.tpAssembly.transform.localRotation = Quaternion.Euler(rY * LOOK_SPEED, 0, 0);

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
            if (inX != 0 || inY != 0)
            {
                this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, Quaternion.LookRotation(Vector3.Normalize((new Vector3(actCam.transform.forward.x, 0, actCam.transform.forward.z) * inY) + (new Vector3(actCam.transform.right.x, 0, actCam.transform.right.z) * inX))), ROTATION_SPEED);
            }
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
        cc.height = COLLIDER_CROUCH_HEIGHT;
        cc.center = new Vector3(0, COLLIDER_CROUCH_CENTER_Y, 0);
        cameraScript.ToggleCameraCrouch(true);
    }
    public void Decrouch()
    {
        
        movementSpeed = WALK_SPEED;
        cc.height = COLLIDER_NORMAL_HEIGHT;
        cc.center = new Vector3(0, COLLIDER_NORMAL_CENTER_Y, 0);
        cameraScript.ToggleCameraCrouch(false);
    }

    public void EnableActions()
    {
        moveAction.Enable();
    }
}
