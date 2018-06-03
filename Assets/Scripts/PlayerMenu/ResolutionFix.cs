/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: ResolutionFix.cs
//	Website: www.deniszpop.co.uk
//  Note: 
//	Description: The 'ResolutionFix' script is used for fixing some UI elements that are not resizing.
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResolutionFix : MonoBehaviour 
{
    public CanvasScaler GameCanvas;
    public RectTransform InvGUI;
    public Vector2 InvRect = new Vector2(180,132);
    public int InvColumns = 5;
    public int TitleHeight = 35;

    private Inventory inventory;

    void Awake()
    {
        GameCanvas.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
    }

    void Start()
    {
        GameCanvas.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        inventory = FindObjectOfType<Inventory>();
        CreateLayout();
    }

    void Update()
    {
        CreateLayout();
    }

    void CreateLayout()
    {
        int slots = inventory.Slots;
        int slotSize = inventory.SlotSize;
        int rows = ((slots - 1) / InvColumns) + 1;

        InvRect.y = TitleHeight + (rows * slotSize);

        InvGUI.sizeDelta = InvRect;
    }

}