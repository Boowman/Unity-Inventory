/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: ItemShoes.cs
//	Website: www.deniszpop.co.uk
//	Note:
//	Description: The 'ItemShoes' script is used to display the player model.
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class EquipmentUICamera : MonoBehaviour
{
    public Transform EquipmentButtons;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            //if (Physics.Raycast(ray, out hit))
            //{
            //    if (hit.collider.tag == "EquipedItems")
            //    {
            //        hit.collider.gameObject.SetActive(false);
            //        MoveSlot.instance.SlotMove(hit.collider.GetComponent<Item>());

            //        foreach (Transform child in EquipmentButtons)
            //        {
            //            if (MoveSlot.Slot.CurrentStoredItem.Type == ItemType.Equipment)
            //            {
            //                if (!child.GetComponent<EquipmentSlot>().IsEmpty)
            //                {
            //                    if (child.GetComponent<EquipmentSlot>().CurrentItem.EquipmentLocation == MoveSlot.Slot.CurrentStoredItem.EquipmentLocation)
            //                    {
            //                        if (child.GetComponent<EquipmentSlot>().CurrentItem.Type == MoveSlot.Slot.CurrentStoredItem.Type)
            //                        {
            //                            child.GetComponent<EquipmentSlot>().ClearSlot();
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }
    }
}
