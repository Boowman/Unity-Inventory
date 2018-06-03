/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: ProximityDetectItems.cs
//	Website: www.deniszpop.co.uk
//  Note: 
//	Description: The 'ProximityDetectItems' script is used detect items near the player, give out information about the items and display them.
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class ProximityDetectItems : MonoBehaviour 
{
    public int index = -1;
    public int objectIndex = -1;
 
    void Start()
    {
        index = -1;
        objectIndex = -1;
    }

    void Update()
    {
        if (objectIndex == -1)
            objectIndex = index;

        //Reset the index if the tablet is not visible.
        if (!GameObject.Find("Tablet"))
            index = -1;
    }

    void OnTriggerStay(Collider other)
    {
        if (GameObject.Find("Tablet") && other.tag == "ItemOnGroundDisplay")
        {
            //The item will be displayed only once because the index will not be -1 after we are within range.
            if (index == -1)
            {
                index = ++Proximity.instance.index;
                Proximity.instance.DisplayItem(GetComponent<Item>());
                Proximity.instance.transform.FindChild("ProximityGroup").GetChild(index).GetComponent<ProximitySlot>().ItemDisplayed = GetComponent<Item>();
                Proximity.instance.transform.FindChild("ProximityGroup").GetChild(index).GetComponent<ProximitySlot>().ItemIndex = index;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (GameObject.Find("Tablet") && other.tag == "ItemOnGroundDisplay")
        {
            Proximity.instance.RemoveDisplayItem(this.GetComponent<Item>(), index);

            if (MoveSlot.Slot)
                Destroy(GameObject.Find("MovingSlot"));

            if (ToolTip.SlotToolTip)
                Destroy(GameObject.Find("SlotToolTip"));

            if (index != -1)
                index = -1;
        }
    }
}