/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: MoveSlot.cs
//	Website: www.deniszpop.co.uk
//	Note:
//	Description: The 'MoveSlot' script is to move around items we clicked on.
//               If we want to change the item position or place it in the inventory we use the method SlotMove.
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveSlot : MonoBehaviour 
{
    public static MoveSlot instance;
    public static InventorySlot Slot;

    public GameObject SlotPrefab;
    public Canvas InventoryCanvas;
    public int SlotSize = 128;

    private int itemIndex = -1;
    private string parentName = "";

    public int ItemIndex
    {
        get { return itemIndex; }
        set { itemIndex = value; }
    }

    public string ParentName
    {
        get { return parentName; }
        set { parentName = value; }
    }

    void Start()
    {
        if (instance == null)
            instance = this;
    }

    void Update()
    {
        if (Slot != null)
        {
            UpdateSlotPosition();

            if (Slot.IsEmpty)
                DestroyMovingSlot();
        }
    }

    /// <summary>
    /// This will move the slot around based on the mouse position.
    /// </summary>
    void UpdateSlotPosition()
    {
        Vector2 pos;
        Vector3 mousePos = Input.mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(InventoryCanvas.transform as RectTransform, new Vector3(mousePos.x, mousePos.y - Slot.GetComponent<RectTransform>().sizeDelta.y * 0.5f, 0), InventoryCanvas.worldCamera, out pos);

        Slot.transform.position = InventoryCanvas.transform.TransformPoint(pos);
        Slot.transform.rotation = InventoryCanvas.transform.rotation;
    }

    /// <summary>
    /// This method instantiates a slot with informations collected from the clicked item.
    /// <para>We are setting the name and the text if the item picked has more than 1 item inside.</para>
    /// <para>If we picked a item from the proximity window remove it from there.</para>
    /// </summary>
    public void SlotMove(Item item, int itemsCount, int itemIndex, string parentName) // DisplayItemSlot clickedSlot, 
    {
        this.itemIndex = itemIndex;
        this.parentName = parentName;

        Slot = (InventorySlot)Instantiate(SlotPrefab.GetComponent<InventorySlot>());
        Slot.name = "MovingSlot";
        Slot.transform.SetParent(InventoryCanvas.transform);

        for (int i = 0; i < itemsCount; i++)
        {
            Slot.items.Push(item);
            Slot.NewItemImage(item.Image, Color.white);

            if (itemsCount > 1)
            {
                Slot.StackImage.enabled = true;
                Slot.StackText.text = Slot.items.Count.ToString();
            }

            Proximity.instance.RemoveDisplayItem(item, itemIndex);
        }

        Slot.GetComponent<RectTransform>().sizeDelta = new Vector2(SlotSize, SlotSize);
        Slot.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    public void SlotMove(Item item) // DisplayItemSlot clickedSlot, 
    {
        Slot = (InventorySlot)Instantiate(SlotPrefab.GetComponent<InventorySlot>());
        Slot.name = "MovingSlot";
        Slot.transform.SetParent(InventoryCanvas.transform);

        Slot.items.Push(item);
        Slot.NewItemImage(item.Image, Color.white);

        Slot.GetComponent<RectTransform>().sizeDelta = new Vector2(SlotSize, SlotSize);
        Slot.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    /// <summary>
    /// Look up the object MovingSlot and destroy it, after that reset the itemIndex to -1.
    /// </summary>
    public void DestroyMovingSlot()
    {
        Destroy(GameObject.Find("MovingSlot"));
        this.itemIndex = -1;
        this.parentName = "";
    }
}