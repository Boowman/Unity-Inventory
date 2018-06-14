using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour 
{
    public bool LockCameraMovement = true;

    public float sensitivityX = 3F;
    public float sensitivityY = 3F;

    public float minimumY = -90F;
    public float maximumY = 90F;

    float rotationY = 0F;

    GameObject myCamera;

    void Start()
    {
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;

        myCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update ()
    {
        if (this.transform.tag == "Player")
        {
            if (GameObject.Find("Tablet"))
            {
                if (GameObject.Find("Tablet").activeSelf == true)
                {
                    LockCameraMovement = false;

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                LockCameraMovement = true;
            }
        }

        CameraMovement(LockCameraMovement, Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    public bool CameraMovement(bool enable, float movementX, float movementY)
    {
        if(enable == true)
        {
            if (this.transform.tag == "Player")
            {
                transform.Rotate(0, movementX * sensitivityX, 0);
            }

            if (myCamera != null)
            {
                rotationY += movementY * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                myCamera.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);                
            }

            return true;
        }

        return false;
    }
}