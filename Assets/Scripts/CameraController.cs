using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private readonly float fpNormalPositionY = 0;
    private readonly float fpCrouchPositionY = 0.7f;

    public GameObject activeCamera; //Camera currently in use
    public GameObject tpCameraNormalPosition; //Where the tp camera should be if there was no clipping
    public GameObject fpCamera; //First Person Camera
    public GameObject tpCamera; //Third Person Camera
    public GameObject[] cameras;

    // Start is called before the first frame update
    void Start()
    {
        cameras = new GameObject[] { fpCamera, tpCamera };
        EnableCamera(fpCamera);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //enables specified camera, disables all others
    public void EnableCamera(GameObject cameraToEnable)
    {
        activeCamera = cameraToEnable;
        Debug.Log(cameraToEnable);
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
                Debug.LogError("AlternateCamera (CameraController.cs) is royally fucked");
                return (fpCamera);
            }
        }
        Debug.LogError("AlternateCamera (CameraController.cs) is really, really, really royally fucked");
        return (null);
    }

    public void ToggleCameraCrouch(bool crouch)
    {
        if(!crouch)
        {
            fpCamera.transform.localPosition = new Vector3(0, fpCrouchPositionY, 0);//.transform.localPosition;
        }
        else if(crouch)
        {
            fpCamera.transform.localPosition = new Vector3(0, fpNormalPositionY, 0);//.transform.localPosition;
        }
    }
}
