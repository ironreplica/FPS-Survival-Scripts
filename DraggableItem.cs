using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;
    private int quantity;
    private Image image;
    private TextMeshProUGUI quantityText;
    private CraftingManager craftingManager;
    [HideInInspector] public Transform parentAfterDrag;
  
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
           
            if(transform.parent.transform.parent.name == "Crafting Output")
            {
                Debug.Log("Fire reset");
                craftingManager.ResetGrid();
            }
            /* Check crafting grid */
            else
            {
                parentAfterDrag = transform.parent;
                transform.SetParent(transform.root);
                transform.SetAsLastSibling();
                image.raycastTarget = false;
            }
            
        }
       
        /*if(eventData.button == PointerEventData.InputButton.Right)
        {
            GameObject NewItemIcon = Instantiate(GameObject.Find("ItemManager").GetComponent<ItemManager>().UIItemPrefab);
            NewItemIcon.GetComponent<DraggableItem>().item = item;

            parentAfterDrag = transform.parent;
            NewItemIcon.transform.SetParent(transform.root);

            NewItemIcon.transform.SetAsLastSibling();
            
            NewItemIcon.GetComponent<Image>().raycastTarget = false;
            // Take half 
            Debug.Log("half of item");
            if(quantity % 2 == 0)
            {
                Item newItem = item;
                NewItemIcon.GetComponent<DraggableItem>().UpdateQuantity(-(quantity / 2));
                UpdateQuantity(-(quantity / 2));
            }
        }*/
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
    public void UpdateQuantity(int add)
    {
        /*quantity += add;
        quantityText.text = quantity.ToString();*/
    }
    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        image.sprite = item.sprite;
        craftingManager = GameObject.Find("ItemManager").GetComponent<CraftingManager>();
        // Adding the default quantity.
        /*quantity = item.quantity;*/
        /*quantityText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        quantityText.text = quantity.ToString();*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
