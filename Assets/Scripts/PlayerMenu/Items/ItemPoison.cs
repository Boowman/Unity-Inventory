/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: ItemPoison.cs
//	Website: www.deniszpop.co.uk
//  Note: 
//	Description: The 'ItemPoison' script is used for 
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class ItemPoison : Item, IItemInterface
{
    PlayerHealth playerHealth;

    void Start()
    {
        ItemScale = GetComponent<Transform>().localScale;
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    public void UseItem()
    {
        playerHealth.TakeHealth = 35;
    }
}