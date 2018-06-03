/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: Item.cs
//	Website: www.deniszpop.co.uk
//  Note: 
//	Description: The 'Item' script is used to set some default information about an item.
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public enum ItemEquipmentLocation { None, Front, Back, Armor, Head, Chest, Legs, Feet, HandLeft, HandRight, HandsBoth };
public enum ItemType { None, Food, Attachment, Equipment, Weapon, Prop, Ingredients };
public enum ItemCondition { Good, Damaged };

public class Item : MonoBehaviour
{
    public ItemEquipmentLocation EquipmentLocation  = ItemEquipmentLocation.None;
    public ItemCondition Condition                  = ItemCondition.Good;
    public ItemType Type                            = ItemType.None;

    public Sprite Image;

    public string Name = "Item";
    
    [Multiline]
    public string Description = "Max 198 characters.";

    [Range(0, 10)][Tooltip("Weight In Kilograms.")]
    public float Weight = 0;

    [Range(1, 64)][Tooltip("Maximum amount that can stored in one slot of the same type.")]
    public int MaxStack = 5;

    public bool Stackable = false;

    public Vector3 ItemScale;

    //[Tooltip("Select the mesh you want this item to have")]
    //public Mesh Mesh;

    //[Tooltip("Select the texture that we apply to the model")]
    //public Texture ItemTexture;

    ////Variables used to store values from other scripts. To be able to destroy the object we have to destroy.
    //[HideInInspector]
    //public int ItemSlots;

    //void Start()
    //{
    //    GetComponentInChildren<MeshFilter>().mesh = Mesh;
    //    GetComponentInChildren<MeshRenderer>().material.mainTexture = ItemTexture;
    //}

    //public void SetStats(Item item, bool EquipingItem)
    //{
    //    this.Type                   = item.Type;
    //    this.EquipmentLocation      = item.EquipmentLocation;
    //    this.Condition              = item.Condition;
    //    this.Image                  = item.Image;
    //    this.Name                   = item.Name;
    //    this.Description            = item.Description;
    //    this.Weight                 = item.Weight;
    //    this.MaxStack               = item.MaxStack;
    //    this.Stackable              = item.Stackable;
    //    this.Mesh                   = item.Mesh;
    //    this.ItemTexture            = item.ItemTexture;

    //    CheckComponent<ItemBackPack>(gameObject.GetComponentInChildren<ItemBackPack>(), ItemEquipmentLocation.Back, ItemType.Equipment, item, EquipingItem);
    //}

    //void CheckComponent<T>(T ItemScript, ItemEquipmentLocation location, ItemType itemType, Item item, bool UpdateItem) where T : Component
    //{
    //    if (EquipmentLocation == location && Type == itemType)
    //    {
    //        gameObject.AddComponent<T>();

    //        if (typeof(T).ToString() == typeof(ItemBackPack).ToString())
    //        {
    //            ItemBackPack currentItem = gameObject.GetComponentInChildren<ItemBackPack>();

    //            currentItem.Type                    = item.Type;
    //            currentItem.EquipmentLocation       = item.EquipmentLocation;
    //            currentItem.Condition               = item.Condition;
    //            currentItem.Image                   = item.Image;
    //            currentItem.Name                    = item.Name;
    //            currentItem.Description             = item.Description;
    //            currentItem.Weight                  = item.Weight;
    //            currentItem.MaxStack                = item.MaxStack;
    //            currentItem.Stackable               =  item.Stackable;
    //            currentItem.Mesh                    = item.Mesh;
    //            currentItem.ItemTexture             = item.ItemTexture;

    //            if (UpdateItem == true)
    //            {
    //                ItemBackPack pickedUpItem = item.gameObject.GetComponentInChildren<ItemBackPack>();

    //                if (pickedUpItem)
    //                    ItemSlots = pickedUpItem.Slots;
    //            }

    //            if (currentItem)
    //                currentItem.Slots = item.ItemSlots;
    //        }

    //        Destroy(gameObject.GetComponent<Item>());
    //    }
    //}
}