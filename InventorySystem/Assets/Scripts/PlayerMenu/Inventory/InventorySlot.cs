/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: InventorySlot.cs
//	Website: www.deniszpop.co.uk
//  Note: 
//	Description: The 'InventorySlot' script is used for add, remove, drop and use items from this specific slot. 
//               It is also used to increment the number of slots which happens when you remove an item.
//               The script also contains the method to swap slots.
//               When dropping an item we update the weight.
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Stack<Item> items = new Stack<Item>();

    public Image StackImage;
    public Text StackText;
    public Image ItemImage;

    private Sprite DefaultImage;
    private InventorySlot movingSlotObject;
    private ToolTip slotToolTip;
    private Vector3 itemScale;

    private Inventory inventory;

    void Start()
    {
        slotToolTip = GetComponent<ToolTip>();
        inventory = GetComponentInParent<Inventory>();
    }

    void Update()
    {
        if (GameObject.Find("MovingSlot"))
            movingSlotObject = GameObject.Find("MovingSlot").GetComponent<InventorySlot>();

        if (!IsEmpty)
            slotToolTip.ShowToolTip(CurrentStoredItem);
    }

    public Item CurrentStoredItem
    {
        get { return items.Peek(); }
    }

    public bool IsEmpty
    {
        get { return items.Count == 0; }
    }

    public bool IsFull
    {
        get { return items.Count >= CurrentStoredItem.MaxStack; }
    }

    public bool IsAvailable
    {
        get { return CurrentStoredItem.MaxStack > items.Count; }
    }

    public void ClearSlot()
    {
        items.Clear();
        inventory.EmptySlots++;

        if (StackText != null)
        {
            StackText.text = string.Empty;
            StackImage.enabled = false;
        }


        NewItemImage(DefaultImage, new Color(0, 0, 0, 0));
    }

    public void NewItemImage(Sprite image, Color color)
    {
        ItemImage.sprite = image;
        ItemImage.color = color;
    }

    public void InsertItem(Item item)
    {
        items.Push(item);

        if (StackText != null && items.Count > 1)
        {
            StackImage.enabled = true;
            StackText.text = items.Count.ToString();
        }

        NewItemImage(item.Image, new Color(1, 1, 1, 1));
    }

    public void UseItem()
    {
        CurrentStoredItem.GetComponent<IItemInterface>().UseItem();
    }

    public void MoveItem(InventorySlot newSlot)
    {
        items.Pop();
        newSlot.items.Push(items.Pop());
    }

    public bool DropItem(Item item)
    {
        if (CurrentStoredItem.Type == item.Type && CurrentStoredItem.Name == item.Name)
        {
            Transform spawnPoint = GameObject.Find("ItemDropSpot").transform;

            GameObject itemObject = (GameObject)Instantiate(CurrentStoredItem.gameObject, spawnPoint.position, Quaternion.Euler(new Vector3(270, 0, 0)));
            itemObject.gameObject.SetActive(true);
            itemObject.name = item.Name;

            itemObject.transform.SetParent(GameObject.Find("PlayersItems").transform);
            itemObject.transform.localRotation = Quaternion.Euler(new Vector3(270, 0, 0));
            itemObject.transform.localScale = CurrentStoredItem.ItemScale;

            if (itemObject.GetComponent<Item>().Type == ItemType.Equipment)
            {
                itemObject.transform.GetChild(0).gameObject.layer = 0;
                itemObject.layer = 0;
                itemObject.tag = "Untagged";
            }

            if (!itemObject.GetComponent<ProximityDetectItems>())
                itemObject.AddComponent<ProximityDetectItems>();

            if (!itemObject.GetComponent<Rigidbody>())
                itemObject.AddComponent<Rigidbody>();

            return true;
        }

        return false;
    }

    public void RemoveItem(bool updateWeight)
    {
        items.Pop();

        if (StackText != null)
        {
            StackImage.enabled = true;
            StackText.text = items.Count > 1 ? "x" + items.Count.ToString() : string.Empty;
        }

        if (items.Count < 1)
            ClearSlot();

    }

    public void SlotSwap(InventorySlot swapSlot, Item slotItem, Item swapItem)
    {
        int currentItemsCount = items.Count;
        int swapItemsCount = swapSlot.items.Count;

        NewItemImage(swapSlot.CurrentStoredItem.Image, Color.white);

        if (StackText != null && swapItemsCount > 1)
        {
            StackImage.enabled = true;
            StackText.text = swapItemsCount.ToString();
        }

        swapSlot.NewItemImage(CurrentStoredItem.Image, Color.white);

        if (currentItemsCount > 1)
        {
            swapSlot.StackImage.enabled = true;
            swapSlot.StackText.text = currentItemsCount.ToString();
        }


        swapSlot.ClearSlot();

        for (int i = 0; i < currentItemsCount; i++)
        {
            swapSlot.InsertItem(slotItem);
        }

        ClearSlot();

        for (int i = 0; i < swapItemsCount; i++)
        {
            InsertItem(swapItem);
        }
    }

    public float GetSlotWeight
    {
        get { return CurrentStoredItem.Weight * items.Count; }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //This is used to move the entire stack from this slot.
        if (!movingSlotObject && !IsEmpty && eventData.button == PointerEventData.InputButton.Left)
        {
            MoveSlot.instance.SlotMove(CurrentStoredItem, items.Count, MoveSlot.instance.ItemIndex, transform.parent.ToString());
            ClearSlot();
        }

        //This is used to move half the stack from this slot.
        else if (!movingSlotObject && !IsEmpty && !Input.GetButton("Sprint") && eventData.button == PointerEventData.InputButton.Right)
        {
            MoveSlot.instance.SlotMove(CurrentStoredItem, items.Count / 2, 0, transform.parent.name);

            //inventory.SlotMove(this.transform.GetComponent<InventorySlot>(), CurrentStoredItem, transform.parent.root, items.Count / 2);
        }

        //This will place and item in the current slot if we are not clicking on the equipment slots.
        else if (IsEmpty && movingSlotObject && eventData.button == PointerEventData.InputButton.Left)
        {
            if(MoveSlot.Slot.CurrentStoredItem.Type != ItemType.Equipment)
            {
                for (int i = 0; i < movingSlotObject.items.Count; i++)
                {
                    InsertItem(movingSlotObject.CurrentStoredItem);
                    Proximity.instance.DestroySelectedObject();
                }

                Destroy(movingSlotObject.gameObject);
            }
        }

        //This is used to drop and item.
        else if (!movingSlotObject && !IsEmpty && Input.GetButton("Sprint") && eventData.button == PointerEventData.InputButton.Right)
        {
            DropItem(CurrentStoredItem);
            RemoveItem(true);
            slotToolTip.HideToolTip();
        }

        //This is used to use an item.
        else if (eventData.button == PointerEventData.InputButton.Middle && !IsEmpty)
        {
            UseItem();
            RemoveItem(true);
            slotToolTip.HideToolTip();
        }

        //Use to merge stacks, if the slot is not empty and the currentItemType is the same with the one in the movingSlot then merge them 1 by 1.
        //Only merge if there is enough space in the slot. If the movingSlot is empty destroy it.
        //If the types are not the same swap the slots.
        else if (!IsEmpty && movingSlotObject && eventData.button == PointerEventData.InputButton.Left && CurrentStoredItem.Type != movingSlotObject.CurrentStoredItem.Type && CurrentStoredItem.Name != movingSlotObject.CurrentStoredItem.Name)
        {
            if (CurrentStoredItem.Type != ItemType.Equipment)
            {
                SlotSwap(movingSlotObject, CurrentStoredItem, movingSlotObject.CurrentStoredItem);
            }
        }
        else if (!IsEmpty && movingSlotObject && eventData.button == PointerEventData.InputButton.Left)
        {
            for (int i = 0; i < movingSlotObject.items.Count * 2; i++)      //For some reasons the items are getting cut in half so I have to multiply it by 2.
            {
                if (CurrentStoredItem.Type == movingSlotObject.CurrentStoredItem.Type && CurrentStoredItem.Name == movingSlotObject.CurrentStoredItem.Name)
                {
                    if (IsAvailable)
                    {
                        if (!movingSlotObject.IsEmpty)
                        {
                            InsertItem(movingSlotObject.CurrentStoredItem);
                            Proximity.instance.DestroySelectedObject();
                            movingSlotObject.RemoveItem(false);
                        }
                    }
                }
                else
                {
                    if (CurrentStoredItem.Type != ItemType.Equipment)
                    {
                        SlotSwap(movingSlotObject, CurrentStoredItem, movingSlotObject.CurrentStoredItem);
                    }
                }
            }
        }
        else if (!IsEmpty && movingSlotObject && eventData.button == PointerEventData.InputButton.Right)
        {
            if (CurrentStoredItem.Type == movingSlotObject.CurrentStoredItem.Type && CurrentStoredItem.Name == movingSlotObject.CurrentStoredItem.Name)
            {
                if (IsAvailable)
                {
                    if (!movingSlotObject.IsEmpty)
                    {
                        InsertItem(movingSlotObject.CurrentStoredItem);
                        Proximity.instance.DestroySelectedObject();
                        movingSlotObject.RemoveItem(false);
                    }
                }
            }
            else
            {
                if (CurrentStoredItem.Type != ItemType.Equipment)
                {
                    SlotSwap(movingSlotObject, CurrentStoredItem, movingSlotObject.CurrentStoredItem);
                }
            }
        }
        else if (IsEmpty && movingSlotObject && eventData.button == PointerEventData.InputButton.Right)
        {
            if (!movingSlotObject.IsEmpty)
            {
                InsertItem(movingSlotObject.CurrentStoredItem);
                Proximity.instance.DestroySelectedObject();
                movingSlotObject.RemoveItem(false);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        movingSlotObject = MoveSlot.Slot;

        if (!IsEmpty && !ToolTip.SlotToolTip && !movingSlotObject)
        {
            slotToolTip.SlotHovered = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        slotToolTip.HideToolTip();
    }
}