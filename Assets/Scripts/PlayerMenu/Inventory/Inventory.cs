/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: Inventory.cs
//	Website: www.deniszpop.co.uk
//  Note: 
//	Description: The 'Inventory' script is the actual inventory which we use to add new items in the inventory.
//               The script handles the placing of the right amount of items on the specific slot.
//               The script handles the weight system in here, we update it whenever the player picks up an item., if the item the player want's to pick it's too heavy it tells the player.              
//               The script also handles the picked up items from a slot.
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public ItemEquipmentLocation SortLocation;
    public List<InventorySlot> SlotsList = new List<InventorySlot>();

    [Tooltip("Auto Assign")]
    public Image ItemIcon;

    [Tooltip("Auto Assign")]
    public Text SlotsText;

    public GameObject SlotPrefab;

    public int Slots = 0;
    public int SlotSize = 128;

    private InventorySlot clickedSlot;
    private Canvas UICanvas;
    private Sprite itemSprite;

    private int emptySlots;
    private int itemBeingMoved;

    private bool updateSlots = true;

    public int EmptySlots
    {
        get { return emptySlots; }
        set { emptySlots = value; }
    }

    public Sprite ItemSprite
    {
        get { return itemSprite; }
        set  { itemSprite = value; }
    }

    void Start()
    {
        UICanvas = GameObject.Find("InventoryUI").GetComponent<Canvas>();

        //ItemIcon.sprite = transform.FindChild("ItemIcon").GetComponent<Image>().sprite;
        //SlotsText.text = transform.FindChild("Remaining").GetComponent<Text>().text;

        emptySlots = Slots;
    }

    public void InsertSlots()
    {
        if (Slots > 0)
        {
            for (int i = 0; i < Slots; i++)
            {
                GameObject slot = (GameObject)Instantiate(SlotPrefab);

                slot.name = i + " Slot";
                slot.transform.SetParent(transform.FindChild("SlotsGroup").transform);

                slot.transform.localPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                slot.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                slot.transform.localScale = new Vector3(1, 1, 1);

                slot.GetComponent<RectTransform>().sizeDelta = new Vector2(SlotSize, SlotSize);

                SlotsList.Add(slot.GetComponent<InventorySlot>());
            }

            emptySlots = Slots;
        }
    }

    private void RemoveSlots(Transform equipmentItem)
    {
        if (SlotsList.Count > 0)
        {
            foreach (Transform slots in GameObject.Find("SlotsGroup").transform)
            {
                Destroy(slots.gameObject);
            }

            SlotsList.Clear();
            Slots = 0;
            emptySlots = Slots;
            equipmentItem.gameObject.SetActive(false);
            updateSlots = true;
        }
    }

    //public void SlotMove(InventorySlot clickedSlot, Item item, Transform parent, int itemsCount)
    //{
    //    this.clickedSlot = clickedSlot;
    //    this.itemBeingMoved = itemsCount;

    //    MoveSlot.Slot = (InventorySlot)Instantiate(MovedItemPrefab.GetComponent<InventorySlot>());
    //    MoveSlot.Slot.name = "MoveSlot.Slot";
    //    MoveSlot.Slot.transform.SetParent(parent);

    //    int calcItemsCoun = itemsCount > 0 ? itemsCount : itemsCount + 1;

    //    for (int i = 0; i < calcItemsCoun; i++)
    //    {
    //        MoveSlot.Slot.items.Push(clickedSlot.CurrentStoredItem);
    //        MoveSlot.Slot.NewItemImage(clickedSlot.CurrentStoredItem.Image, Color.white);
    //        MoveSlot.Slot.StackText.GetComponentInChildren<Text>().text = MoveSlot.Slot.items.Count.ToString();
    //        clickedSlot.RemoveItem(true);
    //    }

    //    MoveSlot.Slot.transform.localPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    //    MoveSlot.Slot.GetComponent<RectTransform>().sizeDelta = new Vector2(SlotSize, SlotSize);
    //    MoveSlot.Slot.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    //}

    public void PlaceItemsBack()
    {
        if (clickedSlot != null)
        {
            for (int i = 0; i < itemBeingMoved; i++)
            {
                clickedSlot.InsertItem(MoveSlot.Slot.CurrentStoredItem);
                MoveSlot.Slot.items.Pop();
            }

            MoveSlot.instance.DestroyMovingSlot();
        }
    }

    public bool AddItem(GameObject destroyObject, Item item)
    {
        if (item.Stackable == false)
        {
            foreach (InventorySlot slot in SlotsList)
            {
                if (slot.IsEmpty)
                {
                    slot.InsertItem(item);
                    emptySlots--;
                    destroyObject.SetActive(false);
                    return true;
                }
            }

        }
        else
        {
            foreach (InventorySlot slot in SlotsList)
            {
                if (!slot.IsEmpty)
                {
                    if (slot.CurrentStoredItem.Type == item.Type && !slot.IsFull)
                    {
                        slot.InsertItem(item);
                        destroyObject.SetActive(false);
                        return true;
                    }
                }
                else
                {
                    slot.InsertItem(item);
                    emptySlots--;
                    destroyObject.SetActive(false);
                    return true;
                }
            }
        }

        return false;
    }
}