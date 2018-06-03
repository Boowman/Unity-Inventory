/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: InventoryWindow.cs
//	Website: www.deniszpop.co.uk
//	Note:
//	Description: The 'InventoryWindow' script is display all the items that has storage capacity on them. 
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryWindow : MonoBehaviour
{
    public static InventoryWindow instance;

    public Transform Equipment;
    public Transform InvEquipments;
    public GameObject ItemInventory;

    public List<EquipmentStorage> equipStorageList = new List<EquipmentStorage>();

    private bool updateEquipment = true;

    public bool UpdateEquipment
    {
        get { return updateEquipment; }
        set { updateEquipment = value; }
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    void Update()
    {
        if (UpdateEquipment == true)
        {
            foreach (Transform equipInv in InvEquipments)
            {
                InventoryWindowResize tempEquipWin = equipInv.GetComponent<InventoryWindowResize>();

                switch (tempEquipWin.Order)
                {
                    case 0:
                        tempEquipWin.transform.localPosition = new Vector2(0, 0);
                        break;
                    case 1:
                        if (InvEquipments.GetChild(0).gameObject.activeSelf == false)
                            tempEquipWin.transform.localPosition = new Vector2(0, 0);

                        else if (InvEquipments.GetChild(0).gameObject.activeSelf == true)
                            tempEquipWin.transform.localPosition = new Vector2(0, InvEquipments.GetChild(0).localPosition.y - InvEquipments.GetChild(0).GetComponent<RectTransform>().sizeDelta.y - 20);

                        break;
                    case 2:
                        if (InvEquipments.GetChild(0).gameObject.activeSelf == false)
                            tempEquipWin.transform.localPosition = new Vector2(0, 0);

                        else if (InvEquipments.GetChild(0).gameObject.activeSelf == true && InvEquipments.GetChild(1).gameObject.activeSelf == false)
                            tempEquipWin.transform.localPosition = new Vector2(0, InvEquipments.GetChild(0).localPosition.y - InvEquipments.GetChild(0).GetComponent<RectTransform>().sizeDelta.y - 20);

                        else if (InvEquipments.GetChild(1).gameObject.activeSelf == true)
                            tempEquipWin.transform.localPosition = new Vector2(0, InvEquipments.GetChild(1).localPosition.y - InvEquipments.GetChild(1).GetComponent<RectTransform>().sizeDelta.y - 20);
                        break;
                    case 3:

                        if (InvEquipments.GetChild(0).gameObject.activeSelf == false && InvEquipments.GetChild(1).gameObject.activeSelf == false && InvEquipments.GetChild(2).gameObject.activeSelf == false)
                        {
                            tempEquipWin.transform.localPosition = new Vector2(0, 0);
                            Debug.Log("First");
                        }

                        else if (InvEquipments.GetChild(0).gameObject.activeSelf == true && InvEquipments.GetChild(1).gameObject.activeSelf == false && InvEquipments.GetChild(2).gameObject.activeSelf == false)
                        {
                            tempEquipWin.transform.localPosition = new Vector2(0, InvEquipments.GetChild(0).localPosition.y - InvEquipments.GetChild(0).GetComponent<RectTransform>().sizeDelta.y - 20);
                        }

                        else if (InvEquipments.GetChild(1).gameObject.activeSelf == true && InvEquipments.GetChild(2).gameObject.activeSelf == false)
                        {
                            tempEquipWin.transform.localPosition = new Vector2(0, InvEquipments.GetChild(1).localPosition.y - InvEquipments.GetChild(1).GetComponent<RectTransform>().sizeDelta.y - 20);
                            Debug.Log("Third");
                        }

                        else if (InvEquipments.GetChild(2).gameObject.activeSelf == true && InvEquipments.GetChild(3).gameObject.activeSelf == false)
                        {
                            tempEquipWin.transform.localPosition = new Vector2(0, InvEquipments.GetChild(2).localPosition.y - InvEquipments.GetChild(2).GetComponent<RectTransform>().sizeDelta.y - 20);
                            Debug.Log("Fourth");
                        }

                        else if (InvEquipments.GetChild(3).gameObject.activeSelf == true)
                        {
                            tempEquipWin.transform.localPosition = new Vector2(0, InvEquipments.GetChild(3).localPosition.y - InvEquipments.GetChild(3).GetComponent<RectTransform>().sizeDelta.y - 20);
                            Debug.Log("Fifth");
                        }

                        break;
                }
            }

            UpdateEquipment = false;
        }
    }

    public void AddEquipmentList(EquipmentStorage equip)
    {
        if (!equipStorageList.Contains(equip))
        {
            equipStorageList.Add(equip);

            AddInventoryStorage(equip);

            UpdateEquipment = true;
        }
    }

    public void RemoveEquipmentList(EquipmentStorage equip)
    {
        equipStorageList.Remove(equip);

        RemoveInventoryStorage(equip);
    }

    private void AddInventoryStorage(EquipmentStorage equipStorage)
    {
        foreach (Transform invChild in InvEquipments)
        {
            if (equipStorage.GetComponent<Item>().EquipmentLocation.ToString() == invChild.name)
            {
                invChild.gameObject.SetActive(true);
                invChild.name = equipStorage.name;

                invChild.GetComponent<Inventory>().ItemSprite = equipStorage.GetComponent<Item>().Image;
                invChild.GetComponent<Inventory>().Slots = equipStorage.SlotsAmount;
                invChild.GetComponent<Inventory>().SortLocation = equipStorage.GetComponent<Item>().EquipmentLocation;
                invChild.GetComponent<Inventory>().InsertSlots();
            }
        }
    }

    private void RemoveInventoryStorage(EquipmentStorage equipStorage)
    {
        foreach (Transform equipWindow in InvEquipments)
        {
            if (equipWindow.name == equipStorage.name)
            {
                equipWindow.name = equipStorage.GetComponent<Item>().EquipmentLocation.ToString();
                equipWindow.gameObject.SetActive(false);

                foreach(Transform invSlots in equipWindow.transform.FindChild("SlotsGroup"))
                {
                    Destroy(invSlots.gameObject);
                }
            }
        }
    }
}