using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnCraftItem : MonoBehaviour, IPointerClickHandler
{
    private GameObject itemManager;
    private CraftingManager craftingManager;
    private void Start()
    {
        itemManager = GameObject.Find("ItemManager");
        craftingManager = itemManager.GetComponent<CraftingManager>();
    }
    public void OnPointerClick(PointerEventData eventData)
    { 
        craftingManager.ResetGrid();
    }
}
