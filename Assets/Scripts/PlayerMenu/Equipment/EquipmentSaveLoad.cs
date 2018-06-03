/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: EquipmentSaveLoad.cs
//	Website: www.deniszpop.co.uk
//	Note:
//	Description: The 'EquipmentSaveLoad' script is used for 
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class EquipmentSaveLoad : MonoBehaviour 
{
    public Transform EquipmentButtons;
    public List<Item> EquipmentItems = new List<Item>();

    private PlayerMotor playerMotor;

    void Awake()
    {
        playerMotor = FindObjectOfType<PlayerMotor>();
    }

    void Start()
    {
        if (!Directory.Exists(Application.dataPath + "/Data/" + playerMotor.Name + "/Equipment/"))
            Directory.CreateDirectory(Application.dataPath + "/Data/" + playerMotor.Name + "/Equipment/");

        LoadingEquipment();
    }

    void OnApplicationQuit()
    {
        SavingEquipment();
    }

    private void SavingEquipment()
    {
        BinaryFormatter bf = new BinaryFormatter();
        string content = string.Empty;

        foreach(Transform equipButtons in EquipmentButtons)
        {
            EquipmentSlot tempEquip = equipButtons.GetComponent<EquipmentSlot>();

            if (!equipButtons.GetComponent<EquipmentSlot>().IsEmpty)
            {

                content +="\n" + "-" + equipButtons.name + "-" + tempEquip.CurrentItem.Name + ";";
            }
        }

        FileStream file = File.Create(Application.dataPath + "/Data/" + playerMotor.Name + "/Equipment/PlayerEquipment.txt");
        bf.Serialize(file, content);
        file.Close();
    }

    private void LoadingEquipment()
    {
        BinaryFormatter bf = new BinaryFormatter();
        string equipContent = string.Empty;

        if (File.Exists(Application.dataPath + "/Data/" + playerMotor.Name + "/Equipment/PlayerEquipment.txt"))
        {
            FileStream file = File.Open(Application.dataPath + "/Data/" + playerMotor.Name + "/Equipment/PlayerEquipment.txt", FileMode.Open);
            equipContent = (string)bf.Deserialize(file);
            file.Close();
        }

        string[] content = equipContent.Split(';');

        for (int i = 0; i < content.Length - 1; i++)
        {
            string[] subContent = content[i].Split('-');

            string Location     = subContent[1];
            string Name         = subContent[2];

            foreach (Transform equipSlot in EquipmentButtons)
            {
                EquipmentSlot tempSlot = equipSlot.GetComponent<EquipmentSlot>();

                if (tempSlot.name == Location)
                {
                    for (int j = 0; j < EquipmentItems.Count; j++)
                    {
                        if(EquipmentItems[j].Name == Name)
                        {
                            tempSlot.EquipItem(EquipmentItems[j]);
                        }
                    }
                }
            }
        }

        if(PlayerMenuInput.instance != null)
            PlayerMenuInput.instance.DeactivateObjects();
    }
}