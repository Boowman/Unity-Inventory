/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: InventorySaveLoad.cs
//	Website: www.deniszpop.co.uk
//  Note: 
//	Description: The 'InventorySaveLoad' script is used to save the players inventory.
//               The script handles saving the inventory when the player leaves the game.
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class InventorySaveLoad : MonoBehaviour 
{
    public List<Item> items = new List<Item>();
    public Transform Equipment;
    private PlayerMotor playerMotor;
    private Inventory inventory;

    void Awake()
    {
        playerMotor = FindObjectOfType<PlayerMotor>();
    }

    void Start()
    {
        if (!Directory.Exists(Application.dataPath + "/Data/" + playerMotor.Name + "/Inventory"))
            Directory.CreateDirectory(Application.dataPath + "/Data/" + playerMotor.Name + "/Inventory");

        inventory = GetComponent<Inventory>();

        LoadingInventory();
    }

    void OnApplicationQuit()
    {
        SavingInventory();
    }

    public void SavingInventory()
    {
        BinaryFormatter bf = new BinaryFormatter();
        string content = string.Empty;

        for(int i = 0; i < inventory.SlotsList.Count; i++)
        {
            if(!inventory.SlotsList[i].IsEmpty)
            {
                InventorySlot tempSlot = inventory.SlotsList[i].GetComponent<InventorySlot>();

                content += "\n" + "-" + tempSlot.CurrentStoredItem.Name + "-" + tempSlot.items.Count.ToString() + "-" + i + ";";
            }
        }

        FileStream file = File.Create(Application.dataPath + "/Data/" + playerMotor.Name + "/Inventory/" + transform.name + ".txt");
        bf.Serialize(file, content);
        file.Close();
    }

    public void LoadingInventory()
    {
        BinaryFormatter bf = new BinaryFormatter();
        string invContent = string.Empty;

        if(File.Exists(Application.dataPath + "/Data/" + playerMotor.Name + "/Inventory/" + transform.name + ".txt"))
        {
            FileStream file = File.Open(Application.dataPath + "/Data/" + playerMotor.Name + "/Inventory/" + transform.name + ".txt", FileMode.Open);
            invContent = (string)bf.Deserialize(file);
            file.Close();
        }

        string[] content = invContent.Split(';');

        for (int i = 0; i < content.Length - 1; i++)
        {
            string[] subContent = content[i].Split('-');

            string itemName = subContent[1].Replace(" (Item)", "");                         //Item Name
            int itemAmount = int.Parse(subContent[2]);                                      //Number of items
            int slotNumber = int.Parse(subContent[3]);                                      //Slot Number

            for (int x = 0; x < items.Count; x++)
            {
                for (int y = 0; y < itemAmount; y++)
                {
                    InventorySlot tempSlot = inventory.SlotsList[slotNumber].GetComponent<InventorySlot>();

                    if (items[x].Name == itemName)
                    {
                        tempSlot.InsertItem(items[x]);
                    }
                }
            }
        }

        Debug.Log("Loaded");
    }
}