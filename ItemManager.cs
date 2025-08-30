using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour,IDataPersistence
{
    // These are the items that are accessable to the player, NOT the players current items.
    public Item[] items;
    public List<Item> curItems = new List<Item>();
    public GameObject UIItemPrefab;
    public GameObject hotBarParentObj;
    public GameObject Inventory;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    } 
    public void AddItem(Item item)
    {

            bool itemAdded = false;
            curItems.Add(item);
            GameObject grid = Inventory.transform.Find("Grid").gameObject;
            for(var i = 0; i < grid.transform.childCount; i++)
            {
            if (grid.transform.GetChild(i).transform.childCount == 0)
            {
                GameObject newUiItem = Instantiate(UIItemPrefab, grid.transform.GetChild(i));
                newUiItem.GetComponent<DraggableItem>().item = item;
                itemAdded = true;
                break;
            }
            }
            if (!itemAdded)
            {
                Debug.LogError("Inventory is full.");
            }
            
        /*}*/
    }
    public bool InventoryFull()
    {
        GameObject grid = Inventory.transform.Find("Grid").gameObject;
        for (var i = 0; i < grid.transform.childCount; i++)
        {
            if (grid.transform.GetChild(i).transform.childCount == 0)
            {

                return false;
            }
        }
        return true;
    }

    public void LoadData(GameData data)
    {
        GameObject grid = Inventory.transform.Find("Grid").gameObject;
        for (var i = 0; i < grid.transform.childCount; i++)
        {
            if (data.inventory[i] != -1)
            {
                GameObject newItem = Instantiate(UIItemPrefab);
                newItem.transform.parent = grid.transform.GetChild(i);
                newItem.GetComponent<DraggableItem>().item = items[data.inventory[i]];
                newItem.transform.localScale = new Vector3(1, 1, 1);
            }
            /*else if (grid.transform.GetChild(i).GetChild(0))
            {
                slotsToSave[i] = grid.transform.GetChild(i).GetChild(0).GetComponent<DraggableItem>().item.itemId;
            }*/
        }
        for (var i = 0; i < hotBarParentObj.transform.childCount; i++)
        {
            if (data.hotBar[i] != -1)
            {
                GameObject newItem = Instantiate(UIItemPrefab);
                newItem.transform.parent = hotBarParentObj.transform.GetChild(i);
                newItem.GetComponent<DraggableItem>().item = items[data.hotBar[i]];
                newItem.transform.localScale = new Vector3(1, 1, 1);
            }
            /*else if (grid.transform.GetChild(i).GetChild(0))
            {
                slotsToSave[i] = grid.transform.GetChild(i).GetChild(0).GetComponent<DraggableItem>().item.itemId;
            }*/
        }
    }

    public void SaveData(ref GameData data)
    {
        GameObject grid = Inventory.transform.Find("Grid").gameObject;
        int[] slotsToSave = new int[18];
        for(var i = 0; i < grid.transform.childCount; i++)
        {
            if(grid.transform.GetChild(i).transform.childCount == 0)
            {
                slotsToSave[i] = -1;
            }
            else if (grid.transform.GetChild(i).GetChild(0))
            {
                slotsToSave[i] = grid.transform.GetChild(i).GetChild(0).GetComponent<DraggableItem>().item.itemId;
            }
        }
        data.inventory = slotsToSave;

        int[] hotbarSlotsToSave = new int[3];
        for(var i = 0; i < hotBarParentObj.transform.childCount; i++)
        {
            if (hotBarParentObj.transform.GetChild(i).transform.childCount == 0)
            {
                hotbarSlotsToSave[i] = -1;
            }
            else if (hotBarParentObj.transform.GetChild(i).GetChild(0))
            {
                hotbarSlotsToSave[i] = hotBarParentObj.transform.GetChild(i).GetChild(0).GetComponent<DraggableItem>().item.itemId;
            }
        }

        data.hotBar = hotbarSlotsToSave;
    }
}
