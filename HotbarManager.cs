using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class HotbarManager : MonoBehaviour
{
    private KeyCode num1;
    private KeyCode num2;
    private KeyCode num3;

    private int activeItem;
    private int itemType;
    private GameObject hotBar;
    public GameObject IKHandObject;
    public Color activeColor;
    private Color defaultColor;
    private HandController handController;
    private float defaultDmg;
/*    private GameObject hands;*/
    private GameObject holdingItem;
    private GameObject itemParent;
    public GameObject itemSlot;
    public GameObject weaponSlot;
    private GameObject bluePrint;
    private Item curItem;
    public bool isSnapping;
    /*  ITEM TYPES
     * Type 0: Inventory only item, not holdable
     * Type 1: Iventory and holdable, has use such as melee
     * Type 2: Foundation, and buidables not requiring a foundation
     * Type 3: Foundation ONLY buildables
     */
    void Start()
    {
        IKHandObject.GetComponent<IKHands>().ResetWeight();
        isSnapping = false;
        activeItem = 0;
        handController = GameObject.Find("Player").GetComponent<HandController>();
        defaultDmg = handController.handDmg;
        itemParent = GameObject.Find("Main Camera");
        num1 = KeyCode.Alpha1;
        num2 = KeyCode.Alpha2;
        num3= KeyCode.Alpha3;
        hotBar = GameObject.Find("Hotbar");
        defaultColor = hotBar.transform.GetChild(0).GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSnapping)
        {
            Destroy(bluePrint);
        }
        if (bluePrint == null && !isSnapping && curItem != null && itemType == 2)
        {
            bluePrint = Instantiate(curItem.bluePrintPrefab);
        }
        if (Input.GetKeyDown(num1))
        {
            
            if (activeItem == 1)
            {
                activeItem = 0;
            }
            else
            {
                activeItem = 1;
            }
            EquipItem();
        }
        else if (Input.GetKeyDown(num2))
        {
            

            if (activeItem == 2)
            {
                activeItem = 0;
            }
            else
            {
                activeItem = 2;
            }
            EquipItem();
        }
        else if (Input.GetKeyDown(num3))
        {

            if (activeItem == 3)
            {
                activeItem = 0;
            }
            else
            {
                activeItem = 3;
            }
            EquipItem();

        }
    }
    private void EquipItem()
    {
        handController.GetComponent<FoundationBuildingBlueprints>().canBuild = false;
        handController.canAttack = true;
        handController.animator.SetBool("HideHands", false);
        handController.handDmg = defaultDmg;
        handController.itemDamageAccess = 0;
        if (bluePrint != null)
        {
            Destroy(bluePrint.gameObject);
        }
        if (holdingItem != null)
        {
            Destroy(holdingItem.gameObject);
        }
        if (activeItem == 0)
        {
            // Un Equip item
            handController.emptyHand();
            Destroy(holdingItem);
            hotBar.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
            hotBar.transform.GetChild(1).GetComponent<Image>().color = defaultColor;
            hotBar.transform.GetChild(2).GetComponent<Image>().color = defaultColor;
        }
        else if(activeItem == 1)
        {
             hotBar.transform.GetChild(0).GetComponent<Image>().color = activeColor;
             hotBar.transform.GetChild(1).GetComponent<Image>().color = defaultColor;
               hotBar.transform.GetChild(2).GetComponent<Image>().color = defaultColor;
        }
        else if (activeItem == 2)
        {
             hotBar.transform.GetChild(1).GetComponent<Image>().color = activeColor;
             hotBar.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
             hotBar.transform.GetChild(2).GetComponent<Image>().color = defaultColor;
        }
        else if (activeItem == 3)
        {
             hotBar.transform.GetChild(2).GetComponent<Image>().color = activeColor;
             hotBar.transform.GetChild(1).GetComponent<Image>().color = defaultColor;
             hotBar.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
        }
        if(activeItem != 0)
        {
            if (hotBar.transform.GetChild(activeItem - 1).childCount > 0)
            {
                itemType = hotBar.transform.GetChild(activeItem - 1).GetChild(0).GetComponent<DraggableItem>().item.type;
                /*Debug.Log(hotBar.transform.GetChild(activeItem - 1).GetChild(0).GetComponent<DraggableItem>().item.name);*/
                if (itemType == 1)
                {
                    curItem = hotBar.transform.GetChild(activeItem - 1).GetChild(0).GetComponent<DraggableItem>().item;
                    // Do something to equip the item
                    holdingItem = Instantiate(curItem.holdableItemPrefab, itemSlot.transform);
                    handController.changeItems(curItem.itemAnimator);
                    handController.handDmg = curItem.damage;
                    handController.itemDamageAccess = curItem.itemDamageAccess;
                }
                else if (itemType == 2)
                {
                    handController.canAttack = false;
                    curItem = hotBar.transform.GetChild(activeItem - 1).GetChild(0).GetComponent<DraggableItem>().item;
                    // Do something to equip the item
                    handController.animator.SetBool("HideHands", true);
                    bluePrint = Instantiate(curItem.bluePrintPrefab);
                    if (curItem.itemId == 5)
                    {
                        handController.GetComponent<FoundationBuildingBlueprints>().canBuild = true;
                        handController.GetComponent<FoundationBuildingBlueprints>().curBuild = "Floor";
                        handController.GetComponent<FoundationBuildingBlueprints>().CurItem = curItem;
                        handController.GetComponent<FoundationBuildingBlueprints>().buildPrefab = curItem.placedObjectPrefab;


                    }
                    else
                    {
                        handController.GetComponent<FoundationBuildingBlueprints>().curBuild = "Other";
                    }
                    /*holdingItem = Instantiate(curItem.holdableItemPrefab, itemParent.transform);
                    handController.changeItems(curItem.itemAnimator);*/
                }
                else if (itemType == 3)
                {
                    // Do something to equip the item
                    curItem = hotBar.transform.GetChild(activeItem - 1).GetChild(0).GetComponent<DraggableItem>().item;
                    handController.animator.SetBool("HideHands", true);
                    // Remove this and add functionality to build other build types
                    handController.canAttack = false;
                    handController.GetComponent<FoundationBuildingBlueprints>().curBuild = "Wall";
                    handController.GetComponent<FoundationBuildingBlueprints>().CurItem = curItem;
                    handController.GetComponent<FoundationBuildingBlueprints>().canBuild = true;
                    handController.GetComponent<FoundationBuildingBlueprints>().buildPrefab = curItem.placedObjectPrefab;

                    /*bluePrint = Instantiate(curItem.bluePrintPrefab);*/
                    /*holdingItem = Instantiate(curItem.holdableItemPrefab, itemParent.transform);
                    handController.changeItems(curItem.itemAnimator);*/
                }
                else if (itemType == 4)
                {
                        curItem = hotBar.transform.GetChild(activeItem - 1).GetChild(0).GetComponent<DraggableItem>().item;
                        // Do something to equip the item
                        holdingItem = Instantiate(curItem.holdableItemPrefab, weaponSlot.transform);
                    holdingItem.GetComponent<WeaponController>().MainCamera = itemParent;
                    holdingItem.GetComponent<WeaponController>().gunItem = curItem;
                    holdingItem.GetComponent<WeaponController>().InitializeWeapon();
                        handController.changeItems(curItem.itemAnimator);
                        handController.handDmg = curItem.damage;
                        IKHandObject.GetComponent<IKHands>().currentWeapon = holdingItem.GetComponent<WeaponController>();
                        handController.itemDamageAccess = curItem.itemDamageAccess;
                }
                
                /*Debug.Log("Item found");*/
            }

        }
        /*if (hotBar.transform.GetChild(activeItem).childCount > 1)
        {
            Debug.Log(hotBar.transform.GetChild(activeItem).GetChild(0).transform.name);
        }*/
    }
    public void InstantiateBlueprint()
    {
        if (!bluePrint.activeInHierarchy || bluePrint == null)
        {
            bluePrint = Instantiate(curItem.bluePrintPrefab);
        }
    }
    public void ResetHands()
    {
        IKHandObject.GetComponent<IKHands>().ResetWeight();
        handController.GetComponent<FoundationBuildingBlueprints>().canBuild = false;
        curItem = null;
        handController.animator.SetBool("HideHands", false);
        if (bluePrint != null)
        {
            Destroy(bluePrint.gameObject);
        }
        handController.emptyHand();
        Destroy(holdingItem);
        hotBar.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
        hotBar.transform.GetChild(1).GetComponent<Image>().color = defaultColor;
        hotBar.transform.GetChild(2).GetComponent<Image>().color = defaultColor;
        Destroy(hotBar.transform.GetChild(activeItem - 1).GetChild(0).gameObject);
        activeItem = 0;
    }
}
