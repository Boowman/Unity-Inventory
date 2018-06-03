/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: EquipmentStorage.cs
//	Website: www.deniszpop.co.uk
//	Note:
//	Description: The 'EquipmentStorage' script is used to set the numbers of slots the player will have available with this specific item.
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class EquipmentStorage : MonoBehaviour 
{
    [Tooltip("The number of slots the player will have available to store items in.")]
    public int Slots = 0;

    public int SlotsAmount
    {
        get { return Slots; }
    }

    void OnEnable()
    { 
        InventoryWindow.instance.AddEquipmentList(this);
    }

    void OnDisable()
    {
        InventoryWindow.instance.RemoveEquipmentList(this);
    }
}