/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: ProximitySlot.cs
//	Website: www.deniszpop.co.uk
//  Note: 
//	Description: The 'ProximitySlot' script is located on the slot that is displayed in the proximity window.
//               In here we change the slot color if we are hovering an existing slot while moving a slot around.
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ProximitySlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private int itemIndex = 0;

    private ProximityDetectItems detectItems;
    private Item itemDisplayed;
    private ToolTip slotToolTip;

    public Item ItemDisplayed
    {
        get { return itemDisplayed; }
        set { itemDisplayed = value; }
    }

    public int ItemIndex
    {
        get { return itemIndex; }
        set { itemIndex = value; }
    }
    
    void Start()
    {
        slotToolTip = GetComponent<ToolTip>();
    }

    void Update()
    {
        slotToolTip.ShowToolTip(ItemDisplayed);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!MoveSlot.Slot && eventData.button == PointerEventData.InputButton.Left)
        {
            MoveSlot.instance.SlotMove(ItemDisplayed, 1, ItemIndex, transform.parent.name);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!ToolTip.SlotToolTip)
            slotToolTip.SlotHovered = true;

        if (MoveSlot.Slot)
            MoveSlot.Slot.GetComponent<Image>().color = new Color32(200, 0, 0, 200);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        slotToolTip.HideToolTip();

        if (MoveSlot.Slot)
            MoveSlot.Slot.GetComponent<Image>().color = new Color32(86, 219, 53, 177);
    }
}