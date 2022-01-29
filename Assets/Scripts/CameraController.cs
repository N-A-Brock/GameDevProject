using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private readonly float fpNormalPositionY = 0.9f;
    private readonly float fpCrouchPositionY = 0.2f;

    RaycastHit tpCameraClipHit;
    readonly float MIN_CAMERA_DISTANCE = -1f;
    readonly float CAM_MOVE_SPEED = 0.01f;
    readonly float CAM_CLIP_BUFFER = 0.2f; //To avoid tpCam moving just far enough to be out of the collider of another object, move it a little farther (a little represented by this amount)
    readonly float CAM_JITTER_BUFFER = 0.3f; //When the camera is raycasting out of an object, it will normally over adjust and jitter forwards and backwards. This value represents a bit of wiggle room

    public GameObject activeCamera; //Camera currently in use
    public GameObject tpCameraNormalPosition; //Where the tp camera should be if there was no clipping
    public GameObject fpCamera; //First Person Camera
    public GameObject tpCamera; //Third Person Camera
    public GameObject tpAssembly;
    public GameObject cameraAssembly; //All components of Third Person Camera
    public GameObject[] cameras;

    // Start is called before the first frame update
    void Start()
    {
        cameras = new GameObject[] { fpCamera, tpCamera };
        EnableCamera(fpCamera);
        activeCamera = fpCamera;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //enables specified camera, disables all others
    public void EnableCamera(GameObject cameraToEnable)
    {
        activeCamera = cameraToEnable;
        foreach (GameObject item in cameras)
        {
            if (item != cameraToEnable)
            {
                item.SetActive(false);
            }
        }
        cameraToEnable.SetActive(true);
    }


    //returns which camera (fp or tp) should be enabled
    public GameObject ActiveCamera(bool invertReturn)
    {
        foreach (GameObject item in cameras)
        {
            if(invertReturn)
            {
                if (!item.activeSelf)
                {
                    return (item);
                }
            }
            else if(!invertReturn)
            {
                if (item.activeSelf)
                {
                    return (item);
                }
            }
            else
            {
                return (fpCamera);
            }
        }
        return (null);
    }

    public void ToggleCameraCrouch(bool crouch) //true if crouching, false if standing up
    {
        if (!crouch)
        {
            cameraAssembly.transform.localPosition = new Vector3(0, fpNormalPositionY, 0);
        }
        else if(crouch)
        {
            cameraAssembly.transform.localPosition = new Vector3(0, fpCrouchPositionY, 0);
        }
    }

    public void CameraAction()
    {
        if (activeCamera == fpCamera)
        {

        }
        else if (activeCamera == tpCamera)
        {
            if (Physics.Linecast(fpCamera.transform.position, tpCameraNormalPosition.transform.position))
            {
                if ((Physics.Linecast(fpCamera.transform.position, tpCamera.transform.position)) || ((Physics.Linecast(fpCamera.transform.position, tpCamera.transform.position)) && (Physics.Linecast(tpCamera.transform.position, tpCameraNormalPosition.transform.position))))
                {
                    tpCamera.transform.localPosition = new Vector3(0, 0, (Mathf.Lerp(tpCamera.transform.localPosition.z, MIN_CAMERA_DISTANCE, CAM_MOVE_SPEED) + CAM_CLIP_BUFFER));
                }
                else if ((Physics.Linecast(tpCamera.transform.position, tpCameraNormalPosition.transform.position, out tpCameraClipHit)) && !(Physics.Linecast(fpCamera.transform.position, tpCamera.transform.position)))
                {
                    if (Vector3.Distance(tpCamera.transform.position, tpCameraClipHit.transform.position) <= CAM_JITTER_BUFFER)
                    {
                        tpCamera.transform.position = Vector3.MoveTowards(tpCamera.transform.position, tpCameraClipHit.point, CAM_MOVE_SPEED);
                    }

                }
            }
            else
            {
                if (tpCamera.transform.localPosition != tpCameraNormalPosition.transform.localPosition)
                {
                    tpCamera.transform.localPosition = Vector3.MoveTowards(tpCamera.transform.localPosition, tpCameraNormalPosition.transform.localPosition, CAM_MOVE_SPEED);
                }
                else
                {
                    tpCamera.transform.localPosition = tpCameraNormalPosition.transform.localPosition;
                }
            }
        }
        else
        {

        }
    }
    
}
