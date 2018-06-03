/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: TabButtonColor.cs
//	Website: www.deniszpop.co.uk
//	Note:
//	Description: The 'TabButtonColor' script is used to set the color of the tab once the gameObject is disabled. 
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TabButtonColor : MonoBehaviour 
{
    public Button button;

    void OnEnable()
    {
        ColorBlock btnColor = button.colors;

        btnColor.normalColor = Color.white;
        button.colors = btnColor;
    }

    void OnDisable()
    {
        ColorBlock btnColor = button.colors;

        btnColor.normalColor = Color.grey;
        button.colors = btnColor;
    }
}