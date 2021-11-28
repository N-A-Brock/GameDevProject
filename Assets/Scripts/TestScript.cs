using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private bool toggle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (toggle)
            {
                toggle = false;
            }
            else
            {
                toggle = true;
            }
        }

        if (toggle)
        {
            Debug.Log("Right: " + this.transform.right + "    Product: " + Vector3.Scale(this.transform.forward, this.transform.right));
        }
        else
        {
            Debug.Log("Forward: " + this.transform.forward);

        }

    }
}
