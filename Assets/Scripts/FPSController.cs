using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public CameraController cameraScript;

    bool crouchToggled;
    public bool isCrouching;

    public GameObject test;

    RaycastHit tpCameraClip;

    readonly float jumpForce = 5000f;
    readonly float crouchSpeed = 1f;
    readonly float walkSpeed = 7.5f;
    readonly float sprintSpeed = 12f;
    readonly float lookSpeed = 2.0f;
    readonly float rotationSpeed = 100f;
    float movementSpeed;
    float speedBeforeCrouching;

    public GameObject fpHead;
    public GameObject tpHead;

    private float rY, rX; // X is left right, Y is up down.

    private Vector3 movementDirection;

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
        CharacterMove(movementDirection);
       
    }

    // Update is called once per frame
    void Update()
    {
        //Movement/Vision
        if (cameraScript.activeCamera = cameraScript.fpCamera)
        {
            FpLook();
            DirectionOutput(MovementInput(), cameraScript.fpCamera);
        }
        else if (cameraScript.activeCamera = cameraScript.tpCamera)
        {
            TpLook();
            DirectionOutput(MovementInput(), cameraScript.tpCamera);
        }
        else
        {

        }

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
            Debug.Log("Input detected: V");
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
    public void FpLook() // Look rotation (UP down is fpHead (X)) (Left right is player (Y))
    {
        rY += -Input.GetAxis("Mouse Y");
        rX += Input.GetAxis("Mouse X");
        rY = Mathf.Clamp(rY, -40f, 40f);
        transform.eulerAngles = new Vector2(0, rX) * lookSpeed;
        fpHead.transform.localRotation = Quaternion.Euler(rY * lookSpeed, 0, 0);

        //Debug.Log("fplook");

    }

    public void TpLook()
    {
        rY += -Input.GetAxis("Mouse Y");
        rX += Input.GetAxis("Mouse X");
        rY = Mathf.Clamp(rY, -40f, 40f);
        tpHead.transform.localRotation = Quaternion.Euler(rY * lookSpeed, rX * lookSpeed, 0);

        


        if (Physics.Linecast(fpHead.transform.position, cameraScript.tpCameraNormalPosition.transform.position))
        {
            cameraScript.tpCamera.transform.localPosition = Vector3.MoveTowards(cameraScript.tpCamera.transform.localPosition, fpHead.transform.localPosition, 2);
            Debug.Log("ouch, my fpHead");
        }
        else if (cameraScript.tpCamera.transform.localPosition != cameraScript.tpCameraNormalPosition.transform.localPosition)
        {
            cameraScript.tpCamera.transform.localPosition = Vector3.MoveTowards(cameraScript.tpCamera.transform.localPosition, cameraScript.tpCameraNormalPosition.transform.localPosition, 2);
        }

    }

    public Vector3 MovementInput()
    {
        float x, z;
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        return (new Vector3(x, 0, z));
    }

    public void DirectionOutput(Vector3 input, GameObject actCam)
    {
        if (actCam == cameraScript.fpCamera)
        {
            movementDirection = input * movementSpeed;
        }
        else if (actCam == cameraScript.tpCamera)
        {
            movementDirection = Vector3.Scale((cameraScript.tpCamera.transform.right * input.x), (cameraScript.tpCamera.transform.forward * input.z));
        }
    }

    public void CharacterMove(Vector3 direction)
    {
        rb.AddForce(direction);
    }

    public void FpMove()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * movementSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.back * movementSpeed);
        }
    }

    public void TpMove()
    {

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.Normalize(-cameraScript.tpCamera.transform.right) * Time.deltaTime * movementSpeed, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.Normalize(cameraScript.tpCamera.transform.right) * Time.deltaTime * movementSpeed, Space.World);
        }
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.Normalize(new Vector3(cameraScript.tpCamera.transform.forward.x, 0, cameraScript.tpCamera.transform.forward.z)) * Time.deltaTime * movementSpeed, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.Normalize(new Vector3(-cameraScript.tpCamera.transform.forward.x, 0, -cameraScript.tpCamera.transform.forward.z)) * Time.deltaTime * movementSpeed, Space.World);     
        }
        Debug.Log(rb.velocity);
    }

    public void TpRotate(Vector3 direction)
    {
        //fpHead.transform.rotation = Quaternion.FromToRotation(fpHead.transform.rotation, );
    }

    //replace with reference to camera script
    public void Crouch()
    {
        speedBeforeCrouching = movementSpeed;
        cameraScript.ToggleCameraCrouch(true);
        cc.enabled = false;
        
    }
    public void Decrouch()
    {
        
        movementSpeed = walkSpeed;
        cc.enabled = true;
        cameraScript.ToggleCameraCrouch(false);
    }
}
