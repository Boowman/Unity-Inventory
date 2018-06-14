/////////////////////////////////////////////////////////////////////////////////
//
//	Name: DeniszPop
//  ScriptName: PlayerMenuInput.cs
//	Website: www.deniszpop.co.uk
//	Note:
//	Description: The 'PlayerMenuInput' script is a temp script.
//
//              © DeniszPop. All Rights Reserved.
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class PlayerMenuInput : MonoBehaviour 
{
    public static PlayerMenuInput instance;

    public GameObject Tablet;
    public GameObject UICamera;

    public GameObject InventoryUI;
    public GameObject MainMenuUI;
    public GameObject SkillsUI;
    public GameObject CraftingUI;

    private MouseLook mouseLook;
    private Inventory[] inventory;

    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        mouseLook = GetComponent<MouseLook>();
        inventory = FindObjectsOfType <Inventory>();
    }

    void Update()
    {
        OpenMenu("Inventory", true, false, false, false, false);
        OpenMenu("MainMenu", false, true, false, false, false);
    }

    public void OpenMenu(string Key, bool inv, bool main, bool skills, bool craft, bool noKeyNedded)
    {
        if(Input.GetButtonDown(Key) || noKeyNedded == true)
        {
            if (Tablet.activeSelf == true && noKeyNedded == false)
            {
                Proximity.instance.Clear();

                Tablet.SetActive(false);
                InventoryUI.SetActive(false);
                MainMenuUI.SetActive(false);
                SkillsUI.SetActive(false);
                CraftingUI.SetActive(false);

                if (MoveSlot.Slot)
                {
                    foreach(Inventory invScript in inventory)
                    {
                        invScript.PlaceItemsBack();
                    }
                }
            }
            else
            {
                DeactivateObjects();

                Tablet.SetActive(true);
                InventoryUI.SetActive(inv);
                MainMenuUI.SetActive(main);
                SkillsUI.SetActive(skills);
                CraftingUI.SetActive(craft);

                if (InventoryUI.activeSelf == true)
                {
                    UICamera.SetActive(true);
                }

                Camera.main.GetComponent<Transform>().localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }
    }

    public void DeactivateObjects()
    {
        Tablet.SetActive(false);
        UICamera.SetActive(false);
        InventoryUI.SetActive(false);
        MainMenuUI.SetActive(false);
        SkillsUI.SetActive(false);
        CraftingUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Items")
        {
            foreach (Inventory invScript in inventory)
            {
                if (invScript.EmptySlots > 0)
                {
                    invScript.AddItem(other.gameObject, other.gameObject.GetComponent<Item>());
                    Proximity.instance.RemoveDisplayItem(other.GetComponent<Item>(), other.GetComponent<ProximityDetectItems>().index);
                }
            }
        }
    }
}