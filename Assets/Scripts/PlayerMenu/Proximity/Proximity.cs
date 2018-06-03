/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: Proximity.cs
//	Website: www.deniszpop.co.uk
//  Note: 
//	Description: The 'Proximity' script is used to detect items that are within my reach.
//               It contains a Clear method which will reset the index and delete all the displayed items.
//               It contains a DisplayItem method which is run when an item is in our players reach range.
//               It contains a RemoveDisplayedItem which is run when the items is not in our range anymore.
//               
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Proximity : MonoBehaviour
{
    public static Proximity instance;
    public static GameObject ItemsDisplayed;

    public List<Item> Items = new List<Item>();
    public GameObject SlotPrefab;

    public int index = -1;
    public int SlotSize = 64;

    private int selectedObjectIndex;
    private GameObject selectedGameObject;

    void Start()
    {
        if (instance == null)
            instance = this;
    }

    //****If we have errors check this out as the index might have to be reset
    //void Update()
    //{
    //    if (index < -1)
    //        index = -1;

    //    if (transform.childCount == 1 && index == -1)
    //        Destroy(transform.GetChild(0).gameObject);

    //    //Reset the index
    //    if (transform.FindChild("ProximityGroup").childCount <= 0)
    //        index = -1;
    //}

    /// <summary>
    /// This method will clear the proximity window, it will destroy all 'ProximityGroup' object children.
    /// <para>We are also setting index to -1. we clear the Items list.</para>
    /// <para>If we were moving an item we destroy the MovingSlot Object and we also hide the ToolTip if we opened that.</para>
    /// </summary>
    public void Clear()
    {
        if (transform.FindChild("ProximityGroup").childCount > 0)
        {
            foreach (Transform gameObj in transform.FindChild("ProximityGroup"))
            {
                Destroy(gameObj.gameObject);
            }

            index = -1;
            Items.Clear();
        }

        if(MoveSlot.Slot)
            MoveSlot.instance.DestroyMovingSlot();

        if (ToolTip.SlotToolTip)
            Destroy(GameObject.Find("SlotToolTip"));
            
    }

    /// <summary>
    /// The method is used to display items within our reach area.
    /// <para>The name and the image of the image we are displaying will be changed with the items that we can reach.</para>
    /// </summary>
    public void DisplayItem(Item item)
    {
        GameObject DisplayedItem = (GameObject)Instantiate(SlotPrefab);

        DisplayedItem.name = item.Name;
        DisplayedItem.transform.SetParent(transform.FindChild("ProximityGroup"));

        DisplayedItem.transform.localPosition = new Vector3(0, 0, 0);
        DisplayedItem.transform.localScale = new Vector3(1, 1, 1);
        DisplayedItem.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

        DisplayedItem.transform.FindChild("Image").GetComponent<Image>().sprite = item.Image;

        if (DisplayedItem)
            Items.Add(item);
    }

    /// <summary>
    /// The method will used to remove items from our proximity view.
    /// <para>It will remove an item from the list and it will destroy the item with a specific index.</para>
    /// </summary>
    public void RemoveDisplayItem(Item itemObject, int itemIndex)
    {
        if (transform.FindChild("ProximityGroup").childCount > 0 && itemIndex != -1)
        {
            selectedObjectIndex = itemObject.GetComponent<ProximityDetectItems>().objectIndex;
            selectedGameObject = itemObject.gameObject;

            if (transform.FindChild("ProximityGroup").GetChild(itemIndex).gameObject.activeSelf == true)
            {
                transform.FindChild("ProximityGroup").GetChild(itemIndex).gameObject.SetActive(false);
                transform.FindChild("ProximityGroup").GetChild(itemIndex).GetComponent<ProximitySlot>().ItemIndex = itemIndex;
                Items.Remove(itemObject);
            }
        }
    }

    /// <summary>
    /// This method will deactivate a gameObject with the specific index.
    /// <para>The index will be determined based on the item we picked, once the item has been placed in the inventory or used we deactivated it we reset the index.</para>
    /// <para>We will also destroy the MovingSlot object and SlotToolTip if any of them are displayed.</para>
    /// </summary>
    public void DestroySelectedObject()
    {
        if (selectedGameObject != null && selectedObjectIndex != -1)
        {
            //Destroy(selectedGameObject);
            selectedGameObject.SetActive(false);
            selectedGameObject.GetComponent<ProximityDetectItems>().index = -1;
        }

        if (MoveSlot.Slot)
            Destroy(GameObject.Find("MovingSlot"));

        if (ToolTip.SlotToolTip)
            Destroy(GameObject.Find("SlotToolTip"));

        if(MoveSlot.instance)
            MoveSlot.instance.ParentName = "";

        selectedGameObject = null;
    }
}