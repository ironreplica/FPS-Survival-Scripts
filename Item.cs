using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{

    public string itemName;
    public int itemId;
    public Sprite sprite;
    public int type;
    public int quantity = 1;
    public float damage = 0;
    public int itemDamageAccess = 0;
    // public int[,,] recipe = new int[3,3,3];
    [SerializeField] public int[] recipeLayerOne = new int[3];
    [SerializeField] public int[] recipeLayerTwo = new int[3];
    [SerializeField] public int[] recipeLayerThree = new int[3];

    public RuntimeAnimatorController itemAnimator;
    public GameObject holdableItemPrefab;
    public GameObject bluePrintPrefab;
    public GameObject placedObjectPrefab;

    public int curAmmo;
    // [HideInInspector] public int[,,] 
    /*public int quantity = 1;*/
}
