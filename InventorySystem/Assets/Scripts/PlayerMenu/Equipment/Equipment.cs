/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: Equipment.cs
//	Website: www.deniszpop.co.uk
//	Note:
//	Description: The 'Equipment' script is going to handle all the equipemnt that will show up on the player.
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Equipment : MonoBehaviour 
{
    public static Equipment instance;

    public Transform EquipmentObject;
    public Transform EquipmentLocations;

    private bool updateEquipment = true;

    public bool UpdateEquipment
    {
        get { return updateEquipment; }
        set { updateEquipment = value; }
    }

    void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void EquipItem()
    {
        for (int i = 0; i < EquipmentObject.childCount; i++)
        {
            foreach (Transform child in EquipmentObject.GetChild(i))
            {
                foreach (Transform uiChild in EquipmentLocations)
                {
                    if (!uiChild.GetComponent<InventorySlot>().IsEmpty)
                    {
                        InventorySlot uiItem = uiChild.GetComponent<InventorySlot>();

                        if (child.GetComponent<Item>().Type == uiItem.CurrentStoredItem.Type && child.GetComponent<Item>().Name == uiItem.CurrentStoredItem.Name)
                        {
                            uiChild.GetComponent<InventorySlot>().UseItem();
                            updateEquipment = false;
                        }
                    }
                }
            } 
        }
    }
}