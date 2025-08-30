using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private CraftingManager craftingManager;
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            
            if(transform.parent.name == "Crafting Output")
            {
                // Put results in inventory
                
            }
            else
            {
                GameObject dropped = eventData.pointerDrag;
                DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
                draggableItem.parentAfterDrag = transform;
                
                
            }
            
        }
        
        else
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

            GameObject current = transform.GetChild(0).gameObject;
            DraggableItem currentDraggable = current.GetComponent<DraggableItem>();

            currentDraggable.transform.SetParent(draggableItem.parentAfterDrag);
            draggableItem.parentAfterDrag = transform;
        }
        if (transform.parent.name == "Crafting Grid")
        {

            StartCoroutine(craftingManager.DroppedItem(eventData.pointerDrag));

        }
        if (transform.parent.name == "Hotbar")
        {

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        craftingManager = GameObject.Find("ItemManager").GetComponent<CraftingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
