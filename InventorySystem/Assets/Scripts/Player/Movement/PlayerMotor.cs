using UnityEngine;
using System.Collections.Generic;

public class PlayerMotor : MonoBehaviour 
{
    public string Name = "DefaultName";
    public int Distance = 3;
    public bool GUIShow = false;
    public bool IsSitting = false;
    private Transform parentObject = null;
    GameObject mainCamera;

    //void Start()
    //{
    //    parentObject = transform.parent;
    //    mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    //}

    //void Update () 
    //{
    //    RaycastHit hit;
    //    Vector3 fwDirection = mainCamera.transform.forward;

    //    Debug.DrawRay(mainCamera.transform.position, fwDirection * Distance, Color.red);

    //    if (Physics.Raycast(mainCamera.transform.position, fwDirection, out hit, Distance))
    //    {
    //        if (hit.collider.gameObject.tag == "Chairs")
    //        {
    //            if (Input.GetKeyDown(KeyCode.E))
    //            {
    //                if(IsSitting == false)
    //                {
    //                    hit.collider.GetComponent<BoxCollider>().isTrigger = true;
    //                    transform.position = hit.collider.transform.position;
    //                    transform.rotation = hit.collider.transform.rotation;
    //                    IsSitting = true;
    //                    GUIShow = false;
    //                }
    //            }
    //        }
    //    }
    //}

    //void OnTriggerStay(Collider other)
    //{
    //    if (other.transform.tag == "Chairs")
    //    {
    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            if (IsSitting == true)
    //            {
    //                other.GetComponent<BoxCollider>().isTrigger = false;
    //                transform.position = other.transform.parent.FindChild("OutOfSeat").position;
    //                transform.rotation = other.transform.parent.FindChild("OutOfSeat").rotation;
    //                IsSitting = false;
    //                GUIShow = false;
    //            }
    //        }
    //    }
    //}
}