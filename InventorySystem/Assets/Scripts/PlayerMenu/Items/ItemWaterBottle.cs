/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: ItemWaterBottle.cs
//	Website: www.deniszpop.co.uk
//  Note: 
//	Description: The 'ItemWaterBottle' script is used for 
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class ItemWaterBottle : Item, IItemInterface
{
    Sprinting playerSprint;

    void Start()
    {
        ItemScale = GetComponent<Transform>().localScale;
        playerSprint = FindObjectOfType<Sprinting>();
    }

    void Update()
    {
        playerSprint = FindObjectOfType<Sprinting>();
    }

    void IItemInterface.UseItem()
    {
        if (playerSprint.IncreaseStamina < 100)
            playerSprint.IncreaseStamina = 15;
    }
}