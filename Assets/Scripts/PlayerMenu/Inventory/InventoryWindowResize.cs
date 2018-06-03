/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: InventoryWindowResize.cs
//	Website: www.deniszpop.co.uk
//	Note:
//	Description: The 'InventoryWindowResize' script is used to resize each storage space displayed in the inventory window.
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class InventoryWindowResize : MonoBehaviour 
{
    public int Order = 0;

    public Vector2 DefaultSize = new Vector2(680, 256);
    public Vector3 DefaultPosition = new Vector3(0, 0, 0);

    public Vector2 InvSlotsSize = new Vector2(680, 136);
    public RectTransform InvSlots;

    private RectTransform rectTransform;
    private Inventory inventory;
    private bool updateSize;
    
    void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
        inventory = this.GetComponent<Inventory>();
        updateSize = true;

        if (InvSlots.sizeDelta != InvSlotsSize)
            InvSlots.sizeDelta = InvSlotsSize;

        if (rectTransform.sizeDelta != DefaultSize)
            rectTransform.sizeDelta = DefaultSize;

        if (rectTransform.localPosition != DefaultPosition)
            rectTransform.localPosition = DefaultPosition;

        switch (inventory.SortLocation)
        {
            case ItemEquipmentLocation.Chest:
                Order = 0;
                transform.SetAsFirstSibling();
                break;
            case ItemEquipmentLocation.Armor:
                Order = 1;
                transform.SetSiblingIndex(1);
                break;
            case ItemEquipmentLocation.Legs:
                Order = 2;
                transform.SetSiblingIndex(2);
                break;
            case ItemEquipmentLocation.Feet:
                Order = 2;
                transform.SetSiblingIndex(2);
                break;
            case ItemEquipmentLocation.Back:
                Order = 3;
                transform.SetAsLastSibling();
                break;
        }
    }

    void Update()
    {
        if (updateSize)
        {
            rectTransform.sizeDelta = new Vector2(DefaultSize.x, 100 + inventory.SlotSize * ((inventory.Slots + 1) / 5));
            InvSlots.sizeDelta = new Vector2(DefaultSize.x, inventory.SlotSize * ((inventory.Slots + 1) / 5));

            updateSize = false;
        }
    }

    public void SetPosition(float x, float y)
    {
        rectTransform.localPosition = new Vector2(x, y);
    }
}