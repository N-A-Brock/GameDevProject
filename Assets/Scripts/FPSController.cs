using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController : MonoBehaviour
{
    public CameraController cameraScript;

    bool crouchToggled;
    public bool isCrouching;

    private PlayerActions playerActions;
    private InputAction moveAction;
    private InputAction lookAction;
    public GameObject characterModel; //Once a script exists specifically for controlling the model, replace this variable with reference to that script


    const float COLLIDER_NORMAL_HEIGHT = 2f;
    const float COLLIDER_CROUCH_HEIGHT = 1f;
    const float COLLIDER_NORMAL_CENTER_Y = 0f;
    const float COLLIDER_CROUCH_CENTER_Y = -0.5f;


    const float JUMP_FORCE = 6f;
    const float CROUCH_SPEED = 450f;
    const float WALK_SPEED = 700f;
    const float SPRINT_SPEED = 1200f;
    const float LOOK_SPEED = 0.1f;
    const float ROTATION_SPEED = 0.2f;
    float movementSpeed;
    float speedBeforeCrouching;

    private float rY, rX; // X is left right, Y is up down


    Rigidbody rb;
    CapsuleCollider cc;

    private void Awake()
    {
        playerActions = new PlayerActions();
    }

    private void OnEnable()
    {
        moveAction = playerActions.PlayerControls.Movement;
        moveAction.Enable();

        lookAction = playerActions.PlayerControls.Look;
        lookAction.Enable();

        playerActions.PlayerControls.Jump.performed += JumpCharacter;
        playerActions.PlayerControls.Jump.Enable();

        playerActions.PlayerControls.Run.Enable();

        playerActions.PlayerControls.Crouch.performed += CrouchCharacter;
        playerActions.PlayerControls.Crouch.Enable();

        playerActions.PlayerControls.ChangeCamera.performed += ChangeCamera;
        playerActions.PlayerControls.ChangeCamera.Enable();
    }


    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        playerActions.PlayerControls.Jump.Disable();
    }

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
        MoveCharacter(cameraScript.activeCamera, moveAction.ReadValue<Vector2>());     
    }

    // Update is called once per frame
    void Update()
    {
        //Vision
        LookCharacter(cameraScript.activeCamera, lookAction.ReadValue<Vector2>());

        //Sprint
        if ((playerActions.PlayerControls.Run.ReadValue<float>() == 1) && !isCrouching)
        {
            movementSpeed = SPRINT_SPEED;
            
        }
        else if (!isCrouching)
        {
            movementSpeed = WALK_SPEED;
            
        }

        //Sliding
        if (isCrouching)
        {
            if (movementSpeed > 1 && movementSpeed < 1.6) //Ngl idk what this does
            {
                movementSpeed = CROUCH_SPEED;
                speedBeforeCrouching = 0;
            }
            else if (speedBeforeCrouching == SPRINT_SPEED)
            {
                movementSpeed = Mathf.Lerp(movementSpeed, CROUCH_SPEED, 2f * Time.deltaTime);
            }
            else
            {
                movementSpeed = CROUCH_SPEED;
            }
        }
    }


    private void ChangeCamera(InputAction.CallbackContext obj)
    {
        cameraScript.EnableCamera(cameraScript.ActiveCamera(true));
    }


    public void LookCharacter(GameObject actCam, Vector2 inVal)
    {
        rY += -inVal.y;
        rX += inVal.x;
        rY = Mathf.Clamp(rY, -89f, 89f);
        Debug.Log(rY);
        if (actCam == cameraScript.fpCamera)
        {
            this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, Quaternion.Euler(0, rX, 0), LOOK_SPEED);
            cameraScript.fpCamera.transform.localRotation = Quaternion.Slerp(cameraScript.fpCamera.transform.localRotation, Quaternion.Euler(rY, 0, 0), LOOK_SPEED);
        }
        else if (actCam == cameraScript.tpCamera)
        {
            //cameraScript.cameraAssembly.transform.localRotation = Quaternion.Slerp(cameraScript.cameraAssembly.transform.localRotation, Quaternion.Euler(0, rX, 0), LOOK_SPEED);
            cameraScript.tpAssembly.transform.localRotation = Quaternion.Slerp(cameraScript.tpAssembly.transform.localRotation, Quaternion.Euler(rY, rX, 0), LOOK_SPEED);
        }
        cameraScript.CameraAction();
    }


    public void MoveCharacter(GameObject actCam, Vector2 inVal) //inVal.x is horizontal input, inY is vertical. No correllation to 3D coordinates.
    {
        if (actCam == cameraScript.fpCamera)
        {
            rb.AddForce(Vector3.Normalize((this.transform.forward * inVal.y) + (this.transform.right * inVal.x)) * movementSpeed);
        }
        else if (actCam == cameraScript.tpCamera)
        {
            if (inVal.x != 0 || inVal.y != 0)
            {
                //This fucker right here
                characterModel.transform.rotation = Quaternion.Slerp(characterModel.transform.rotation, Quaternion.LookRotation(Vector3.Normalize((new Vector3(actCam.transform.forward.x, 0, actCam.transform.forward.z) * inVal.y) + (new Vector3(actCam.transform.right.x, 0, actCam.transform.right.z) * inVal.x))), ROTATION_SPEED);
            }
            rb.AddForce(Vector3.Normalize((new Vector3(actCam.transform.forward.x, 0, actCam.transform.forward.z) * inVal.y) + (new Vector3(actCam.transform.right.x, 0, actCam.transform.right.z) * inVal.x)) * movementSpeed);
        }
    }


    private void JumpCharacter(InputAction.CallbackContext obj)
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.5f))
        {
            rb.velocity += (Vector3.up * JUMP_FORCE);
        }
    }


    private void CrouchCharacter(InputAction.CallbackContext obj)
    {
        if (!isCrouching)
        {
            isCrouching = true;
            speedBeforeCrouching = movementSpeed;
            cc.height = COLLIDER_CROUCH_HEIGHT;
            cc.center = new Vector3(0, COLLIDER_CROUCH_CENTER_Y, 0);
            cameraScript.ToggleCameraCrouch(true);
        }
        else if (isCrouching)
        {
            isCrouching = false;
            movementSpeed = WALK_SPEED;
            cc.height = COLLIDER_NORMAL_HEIGHT;
            cc.center = new Vector3(0, COLLIDER_NORMAL_CENTER_Y, 0);
            cameraScript.ToggleCameraCrouch(false);
        }
    }
}
