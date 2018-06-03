/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: TabButtons.cs
//	Website: www.deniszpop.co.uk
//	Note:
//	Description: The 'TabButtons' script is used to change the page we are viewing.
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TabButtons : MonoBehaviour 
{
    public enum TabButton { MainMenu, Inventory, Skills, Crafting}
    public TabButton Tab = TabButton.MainMenu;

    [Tooltip("Used to display the button on different Layer")]
    private Canvas canvas;

    void Start()
    {
        //canvas = GetComponent<Canvas>();
    }

    public void TabClicked(string clicked)
    {
        if(clicked == "MainMenu")
        {
            PlayerMenuInput.instance.OpenMenu("MainMenu", false, true, false, false, true);
        }
        else if(clicked == "Inventory")
        {
            PlayerMenuInput.instance.OpenMenu("Inventory", true, false, false, false, true);
        }
        else if (clicked == "Skills")
        {
            PlayerMenuInput.instance.OpenMenu("Inventory", false, false, true, false, true);
        }
        else if (clicked == "Crafting")
        {
            PlayerMenuInput.instance.OpenMenu("Inventory", false, false, false, true, true);
        }
    }
}