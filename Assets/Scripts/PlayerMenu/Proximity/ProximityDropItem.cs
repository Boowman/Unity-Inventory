/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: ProximityDropItem.cs
//	Website: www.deniszpop.co.uk
//	Note: STILL IN DEVELOPMENT
//	Description: The 'ProximityDropItem' script is used to drop items if we click inside the proximity window.
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ProximityDropItem : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (MoveSlot.Slot)
            {
                MoveSlot.Slot.GetComponent<InventorySlot>().DropItem(MoveSlot.Slot.GetComponent<InventorySlot>().CurrentStoredItem);
                Proximity.instance.DestroySelectedObject();
            }
        }
    }
}