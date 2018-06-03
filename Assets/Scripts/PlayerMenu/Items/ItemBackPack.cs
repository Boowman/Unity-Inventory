/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: ItemBackPack.cs
//	Website: www.deniszpop.co.uk
//	Note:
//	Description: The 'ItemBackPack' script is used for 
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class ItemBackPack : Item, IItemInterface
{
    void Start()
    {
        ItemScale = GetComponent<Transform>().localScale;
    }

    public void UseItem()
    {
        foreach (Transform itemObject in GameObject.Find("Back").transform)
        {
            Item detectItem = itemObject.GetComponent<Item>();

            if (detectItem.Type == ItemType.Equipment && detectItem.EquipmentLocation == ItemEquipmentLocation.Back)
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