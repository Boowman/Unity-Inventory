/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: ToolTip.cs
//	Website: www.deniszpop.co.uk
//	Note:
//	Description: The 'ToolTip' script is used to display informations of the items that we are hovering.
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    public static GameObject SlotToolTip;

    public GameObject ToolTipPrefab;

    public float WaitTime;
    public float DefaultWaitTime;

    private Canvas InventoryCanvas;
    private bool slotHovered = false;

    public bool SlotHovered
    {
        get { return slotHovered; }
        set { slotHovered = value; }
    }

    void Start()
    {
        InventoryCanvas = GameObject.Find("PlayerInfoMenu").GetComponent<Canvas>();
    }

    /// <summary>
    /// The method will instantiate a toolTip after a few seconds and change some UI elements values located in the tooltip.
    /// <para>The toolTip will spawn at the MousePosition, the right rotation and scale will be given after instantiated.</para>
    /// </summary>
    public void ShowToolTip(Item itemHovered)
    {
        if (!MoveSlot.Slot && slotHovered)
        {
            WaitTime -= Time.deltaTime;

            if (WaitTime <= 0)
            {
                SlotToolTip = (GameObject)Instantiate(ToolTipPrefab);
                SlotToolTip.name = "SlotToolTip";
                SlotToolTip.transform.SetParent(InventoryCanvas.transform);

                //Set the position of the toolTip
                Vector2 pos;
                Vector3 mousePos = Input.mousePosition;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(InventoryCanvas.transform as RectTransform, new Vector3(mousePos.x + 120, mousePos.y - 70, 0), InventoryCanvas.worldCamera, out pos);

                SlotToolTip.transform.position = InventoryCanvas.transform.TransformPoint(pos);
                SlotToolTip.transform.rotation = InventoryCanvas.transform.rotation;
                SlotToolTip.transform.localScale = new Vector3(1, 1, 1);

                //Update the tool tip with it's correct values.
                SlotToolTip.transform.FindChild("ItemImage").GetComponent<Image>().sprite       = itemHovered.Image;
                SlotToolTip.transform.FindChild("ItemName").GetComponent<Text>().text           = itemHovered.Name;
                SlotToolTip.transform.FindChild("ItemCondition").GetComponent<Text>().text      = itemHovered.Condition.ToString();
                SlotToolTip.transform.FindChild("ItemDescription").GetComponent<Text>().text    = itemHovered.Description;

                WaitTime = 0;
                slotHovered = false;
            }
        }

        if (MoveSlot.Slot)
        {
            HideToolTip();
        }
    }

    /// <summary>
    /// Looking up the GameObject called SlotToolTip and destroy it if it exists then reset some values.
    /// </summary>
    public void HideToolTip()
    {
        if(SlotToolTip)
            Destroy(GameObject.Find("SlotToolTip"));

        slotHovered = false;
        WaitTime = DefaultWaitTime;
    }
}