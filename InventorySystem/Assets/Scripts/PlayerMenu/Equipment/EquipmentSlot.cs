/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: EquipmentSlot.cs
//	Website: www.deniszpop.co.uk
//	Note:
//	Description: The 'EquipmentSlot' script is used to drop the item on a specific body location to equip the item.
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    public Transform EquipmentObject;
    public GameObject DropItem;

    private Stack<Item> item = new Stack<Item>();

    public bool IsEmpty
    {
        get { return item.Count == 0; }
    }

    public Item CurrentItem
    {
        get { return item.Peek(); }
    }

    public void InsertItem(Item item)
    {
        this.item.Push(item);
    }

    public void ClearSlot()
    {
        item.Clear();
    }

    private void RemoveItem()
    {
        item.Pop();
    }

    private void UseItem()
    {
        CurrentItem.GetComponent<IItemInterface>().UseItem();
    }

    public void EquipItem(Item item)
    {
        for (int i = 0; i < EquipmentObject.childCount; i++)
        {
            foreach (Transform child in EquipmentObject.GetChild(i))
            {
                if (item.Type == ItemType.Equipment)
                {
                    if (child.parent.name == transform.name.Replace("_UI", "") && IsEmpty)
                    {
                        if (child.GetComponent<Item>().Type == item.Type)
                        {
                            if (child.GetComponent<Item>().Name == item.Name)
                            {
                                InsertItem(item);
                                UseItem();

                                if (Proximity.instance)
                                    Proximity.instance.DestroySelectedObject();
                            }
                        }
                    }
                }
            }
        }
    }

    private void SwapEquipItem()
    {
        foreach (Transform child in EquipmentObject)
        {
            if (MoveSlot.Slot.CurrentStoredItem.Type == ItemType.Equipment)
            {
                if (child.parent.name == transform.name.Replace("_UI", ""))
                {
                    if (child.GetComponent<Item>().Type == MoveSlot.Slot.CurrentStoredItem.Type)
                    {
                        if (child.GetComponent<Item>().EquipmentLocation == MoveSlot.Slot.CurrentStoredItem.EquipmentLocation)
                        {
                            InsertItem(MoveSlot.Slot.CurrentStoredItem);
                            UseItem();
                            Proximity.instance.DestroySelectedObject();
                        }
                    }
                }
            }
        }
    }

    private void DeEquipItem(Item item)
    {
        Transform spawnPoint = GameObject.Find("ItemDropSpot").transform;

        GameObject itemObject = (GameObject)Instantiate(CurrentItem.gameObject, spawnPoint.position, Quaternion.Euler(new Vector3(270, 0, 0)));
        itemObject.gameObject.SetActive(true);
        itemObject.name = item.Name;

        itemObject.transform.SetParent(GameObject.Find("PlayersItems").transform);
        itemObject.transform.localRotation = Quaternion.Euler(new Vector3(270, 0, 0));
        itemObject.transform.localScale = CurrentItem.ItemScale;

        for (int i = 0; i < EquipmentObject.childCount; i++)
        {
            foreach (Transform child in EquipmentObject.GetChild(i))
            {
                if (CurrentItem.Type == ItemType.Equipment)
                {
                    if (child.parent.name == transform.name.Replace("_UI", "") && !IsEmpty)
                    {
                        if (child.GetComponent<Item>().Name == CurrentItem.Name)
                        {
                            if (child.GetComponent<Item>().Type == CurrentItem.Type)
                            {
                                child.gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }

        this.item.Pop();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (MoveSlot.Slot && IsEmpty && eventData.button == PointerEventData.InputButton.Left)
        {
            InventoryWindow.instance.UpdateEquipment = true;
            EquipItem(MoveSlot.Slot.CurrentStoredItem);
        }
        else if(MoveSlot.Slot && !IsEmpty && eventData.button == PointerEventData.InputButton.Left)
        {
            InventoryWindow.instance.UpdateEquipment = true;
            DeEquipItem(CurrentItem);
            SwapEquipItem();
        }
        else if (!IsEmpty && eventData.button == PointerEventData.InputButton.Right)
        {
            InventoryWindow.instance.UpdateEquipment = true;
            DeEquipItem(CurrentItem);
        }
    }
}

/* Drop and Equip destroyed item;

    void EquipItem()
    {
        for (int i = 0; i < EquipmentObject.childCount; i++)
        {
            foreach (Transform child in EquipmentObject.GetChild(i))
            {
                if (MoveSlot.Slot.CurrentStoredItem.Type == ItemType.Equipment)
                {
                    if (child.parent.name == transform.name.Replace("_UI", "") && IsEmpty)
                    {
                        if (child.GetComponent<Item>().Type == MoveSlot.Slot.CurrentStoredItem.Type)
                        {
                            if (child.GetComponent<Item>().Name == MoveSlot.Slot.CurrentStoredItem.Name)
                            {
                                InsertItem();
                                UseItem();
                                CurrentItem.SetStats(MoveSlot.Slot.CurrentStoredItem, true);
                                Proximity.instance.DestroySelectedObject();
                            }
                        }
                    }
                }
            }
        }
    }

    void DeEquipItem(Item item)
    {
        Transform spawnPoint = GameObject.Find("ItemDropSpot").transform;

        GameObject itemObject = (GameObject)Instantiate(DropItem, spawnPoint.position, Quaternion.Euler(new Vector3(270, 0, 0)));
        itemObject.gameObject.SetActive(true);
        itemObject.name = item.Name;

        itemObject.transform.SetParent(GameObject.Find("PlayersItems").transform);
        itemObject.transform.localRotation = Quaternion.Euler(new Vector3(270, 0, 0));
        itemObject.transform.localScale = new Vector3(1, 1, 1);

        for (int i = 0; i < EquipmentObject.childCount; i++)
        {
            foreach (Transform child in EquipmentObject.GetChild(i))
            {
                if (CurrentItem.Type == ItemType.Equipment)
                {
                    if (child.parent.name == transform.name.Replace("_UI", "") && !IsEmpty)
                    {
                        if (child.GetComponent<Item>().Name == CurrentItem.Name)
                        {
                            if (child.GetComponent<Item>().Type == CurrentItem.Type)
                            {
                                child.gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }

        itemObject.GetComponentInChildren<Item>().SetStats(CurrentItem, false);
        this.item.Pop();
    }
*/

/* Drop and Equip deactivated items;

    void EquipItem()
    {
        for (int i = 0; i < EquipmentObject.childCount; i++)
        {
            foreach (Transform child in EquipmentObject.GetChild(i))
            {
                if (MoveSlot.Slot.CurrentStoredItem.Type == ItemType.Equipment)
                {
                    if (child.parent.name == transform.name.Replace("_UI", "") && IsEmpty)
                    {
                        if (child.GetComponent<Item>().Type == MoveSlot.Slot.CurrentStoredItem.Type)
                        {
                            if (child.GetComponent<Item>().Name == MoveSlot.Slot.CurrentStoredItem.Name)
                            {
                                InsertItem();
                                UseItem();
                                Proximity.instance.DestroySelectedObject();
                            }
                        }
                    }
                }
            }
        }
    }

    void DeEquipItem(Item item)
    {
        Transform spawnPoint = GameObject.Find("ItemDropSpot").transform;

        GameObject itemObject = (GameObject)Instantiate(CurrentItem.gameObject, spawnPoint.position, Quaternion.Euler(new Vector3(270, 0, 0)));
        itemObject.gameObject.SetActive(true);
        itemObject.name = item.Name;

        itemObject.transform.SetParent(GameObject.Find("PlayersItems").transform);
        itemObject.transform.localRotation = Quaternion.Euler(new Vector3(270, 0, 0));
        itemObject.transform.localScale = new Vector3(1, 1, 1);

        for (int i = 0; i < EquipmentObject.childCount; i++)
        {
            foreach (Transform child in EquipmentObject.GetChild(i))
            {
                if (CurrentItem.Type == ItemType.Equipment)
                {
                    if (child.parent.name == transform.name.Replace("_UI", "") && !IsEmpty)
                    {
                        if (child.GetComponent<Item>().Name == CurrentItem.Name)
                        {
                            if (child.GetComponent<Item>().Type == CurrentItem.Type)
                            {
                                child.gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }

        this.item.Pop();
    }
*/