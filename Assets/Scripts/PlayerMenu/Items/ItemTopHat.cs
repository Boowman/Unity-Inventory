/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: ItemTopHat.cs
//	Website: www.deniszpop.co.uk
//	Note:
//	Description: The 'ItemTopHat' script is used for 
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections.Generic;

public class ItemTopHat : Item, IItemInterface
{
    public void UseItem()
    {
        foreach (Transform itemObject in GameObject.Find("Head").transform)
        {
            Item detectItem = itemObject.GetComponent<Item>();

            if (detectItem.Type == ItemType.Equipment && detectItem.EquipmentLocation == ItemEquipmentLocation.Head)
            {
                if (itemObject.gameObject.activeSelf != true)
                {
                    ActivateItem(detectItem, itemObject);
                }
                else
                {
                    itemObject.gameObject.SetActive(false);
                    ActivateItem(detectItem, itemObject);
                }
            }
        }
    }

    void ActivateItem(Item item, Transform itemObject)
    {
        if (item.Name == GetComponent<Item>().Name)
        {
            if (itemObject.gameObject.activeSelf != true)
            {
                itemObject.gameObject.SetActive(true);
            }
        }
    }
}