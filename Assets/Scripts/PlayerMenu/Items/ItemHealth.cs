/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: ItemHealth.cs
//	Website: www.deniszpop.co.uk
//  Note: 
//	Description: The 'ItemHealth' script is used for 
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class ItemHealth : Item, IItemInterface
{
    PlayerHealth playerHealth;

    void Start()
    {
        ItemScale = GetComponent<Transform>().localScale;
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    public void UseItem()
    {
        playerHealth.GiveHealth = 50;
    }
}