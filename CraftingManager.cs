using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CraftingManager : MonoBehaviour
{
    private GameObject crafting;
    private GameObject craftingOutputSlot;
    private GameObject UIItemPrefab;
    private ItemManager itemManager;
    private GameObject inventory;
    private Image inventoryGrid;
    private AudioController audioController;

    private Item curItem;

    private int curItemQuantity = 0;
    
    [SerializeField] public int[] recipeLayerOne = new int[3];
    [SerializeField] public int[] recipeLayerTwo = new int[3];
    [SerializeField] public int[] recipeLayerThree = new int[3];
    void Start()
    {
        itemManager = GetComponent<ItemManager>();
        audioController = GameObject.Find("Player").transform.Find("Audio Source").GetComponent<AudioController>();
        crafting = itemManager.Inventory.transform.Find("Crafting Grid").gameObject;
        inventory = itemManager.Inventory;
        inventoryGrid = inventory.transform.Find("Grid").GetComponent<Image>();
        craftingOutputSlot = itemManager.Inventory.transform.Find("Crafting Output").gameObject;
        UIItemPrefab = itemManager.UIItemPrefab;
    }

    public IEnumerator DroppedItem(GameObject droppedObject)
    {
        yield return new WaitForSeconds(0.05f);
        var totalChildren = 0;
        for(var i = 0; i < crafting.transform.childCount; i++)
        {
            /*Debug.Log(crafting.transform.GetChild(i).childCount);*/
            if(crafting.transform.GetChild(i).childCount > 0)
            {
                totalChildren+=1;
            }
        }
        if(totalChildren == 0)
        {
            // Simple one item recipes.
        }
        // Proceed if there's at least one item in the grid
        if (totalChildren >= 1)
        {
            int[,] fullRecipe = new int[3, 3];

            for (int y = 0; y < 3; y++) // Rows
            {
                for (int x = 0; x < 3; x++) // Columns
                {
                    int index = y * 3 + x; // Calculate child index
                    if (index < crafting.transform.childCount && crafting.transform.GetChild(index).childCount > 0)
                    {
                        fullRecipe[y, x] = crafting.transform.GetChild(index).GetChild(0).GetComponent<DraggableItem>().item.itemId;
                    }
                    else
                    {
                        fullRecipe[y, x] = -1; // No item present
                    }
                }
            }
            recipeLayerOne[0] = fullRecipe[0,0];
            recipeLayerOne[1] = fullRecipe[0,1];
            recipeLayerOne[2] = fullRecipe[0,2];

            recipeLayerTwo[0] = fullRecipe[1, 0];
            recipeLayerTwo[1] = fullRecipe[1, 1];
            recipeLayerTwo[2] = fullRecipe[1, 2];

            recipeLayerThree[0] = fullRecipe[2, 0];
            recipeLayerThree[1] = fullRecipe[2, 1];
            recipeLayerThree[2] = fullRecipe[2, 2];
            // Now you can pass fullRecipe to CheckRecipe or handle it further
            CheckRecipe(fullRecipe);
        }


        if (droppedObject.GetComponent<DraggableItem>().item.itemId == 0 && totalChildren == 1)
        {
            curItem = itemManager.items[1];
            curItemQuantity = curItem.quantity; 
            // Crafting the planks

            createItemAtLocation(curItem, craftingOutputSlot);
        }
    }
    private void CheckRecipe(int[,] fullRecipe)
    {
        if(craftingOutputSlot.transform.GetChild(0).childCount == 1)
        {
            Debug.Log("Item conflict");
            Destroy(craftingOutputSlot.transform.GetChild(0).GetChild(0).gameObject);
        }
        foreach (Item item in itemManager.items)
        {
            if (CompareLayers(item, fullRecipe))
            {
                curItem = item;
                curItemQuantity = curItem.quantity;
                // Crafting the planks

                createItemAtLocation(curItem, craftingOutputSlot);
            }
        }
    }

    private bool CompareLayers(Item item, int[,] inputLayer)
    {
        // Check each layer of the recipe against the provided inputLayer
        return CompareLayer(item.recipeLayerOne, inputLayer[0, 0], inputLayer[0, 1], inputLayer[0, 2]) &&
               CompareLayer(item.recipeLayerTwo, inputLayer[1, 0], inputLayer[1, 1], inputLayer[1, 2]) &&
               CompareLayer(item.recipeLayerThree, inputLayer[2, 0], inputLayer[2, 1], inputLayer[2, 2]);
    }

    private bool CompareLayer(int[] itemLayer, int id0, int id1, int id2)
    {
        /*Debug.Log(itemLayer[0] == id0);
        Debug.Log(itemLayer[1] == id1);
        Debug.Log(itemLayer[2]== id2);*/
        return itemLayer[0] == id0 && itemLayer[1] == id1 && itemLayer[2] == id2;
    }
    public void ResetGrid()
    {
        // Populating inventory with items, add a check for if the inventory is full.
        for(var i = 0; i < curItemQuantity; i++)
        {
            for(var y = 0; y < inventoryGrid.transform.childCount; y++)
            {
                if(inventoryGrid.transform.GetChild(y).childCount == 0)
                {
                    // createItemAtLocation(curItem, inventoryGrid.transform.GetChild(y).gameObject);
                    GameObject newItem = Instantiate(UIItemPrefab, inventoryGrid.transform.GetChild(y).gameObject.transform);
                    newItem.GetComponent<DraggableItem>().item = curItem;
                    //GameObject newItem = Instantiate(UIItemPrefab, parent.transform.GetChild(0));
                    break;
                }
                else if (inventoryGrid.transform.GetChild(y).childCount > 0)
                {
                    continue;
                }
            }
        }
        // Clearing the crafting grid
        for(var i = 0; i < crafting.transform.childCount; i++)
        {
            if(crafting.transform.GetChild(i).childCount > 0)
            {
                //GameObject toDestroy = crafting.transform.GetChild(i).gameObject;
                Destroy(crafting.transform.GetChild(i).transform.GetChild(0).gameObject);
            }
        }
        audioController.playSound("craft");
        Destroy(craftingOutputSlot.transform.GetChild(0).GetChild(0).gameObject);
    }
    private void createItemAtLocation(Item item, GameObject parent)
    {
        GameObject newItem;
        if (parent.transform.Equals(craftingOutputSlot) || parent.transform.Equals(crafting))
        {
            newItem = Instantiate(UIItemPrefab, parent.transform.GetChild(0));

            parent.transform.Find("Quantity").GetComponent<TextMeshProUGUI>().text = item.quantity.ToString();

        }
        else
        {
            newItem = Instantiate(UIItemPrefab, parent.transform.GetChild(0));

        }

        // curItemQuantity = item.quantity;
        newItem.GetComponent<DraggableItem>().item = item;
    }
}
