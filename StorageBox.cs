using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageBox : MonoBehaviour
{
    /* Different types:
     * Type 1: Public storage
     * Type 2: Acessable only to player who built it
     * Type 3: Large private storage
     * Type 4: Large public storage
     */
    public int type = 1;
    private PlayerMovement playerMovement;
    public Item[] slots = new Item[24];
    [SerializeField] private string _saveId;
    public GameObject UIItemPrefab;
    public GameObject grid;
    private bool hasItems;
    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        grid = playerMovement.inventory.transform.Find("Container").transform.Find("Grid").gameObject;
        hasItems = false;
    }
    public string SaveId
    {
        get
        {
            if (string.IsNullOrEmpty(_saveId))
            {
                _saveId = System.Guid.NewGuid().ToString();
            }
            return _saveId;
        }
        set { _saveId = value; }
    }
    public void OpenStorage()
    {
        
        playerMovement.OpenContainer(gameObject);
        for(var i = 0; i < slots.Length; i++)
        {
            if(slots[i] != null)
            {
                hasItems = true;
                break;
            }
            else
            {
                continue;
            }
        }
        if(hasItems)
        {
          
            for (var i = 0; i < grid.transform.childCount; i++)
            {
   

                if (slots[i] != null)
                {
              

                    GameObject item = Instantiate(UIItemPrefab, grid.transform.GetChild(i));
                    item.GetComponent<DraggableItem>().item = slots[i];
                }
            }

        }
        

    }
    /* Does not recognize when item is removed */
    public void CloseStorage()
    {
     
        for (var i = 0; i < grid.transform.childCount; i++)
        {
            
            if(grid.transform.GetChild(i).childCount == 1)
            {
                slots[i] = grid.transform.GetChild(i).GetChild(0).GetComponent<DraggableItem>().item;
                Destroy(grid.transform.GetChild(i).GetChild(0).gameObject);
                continue;
            }
            else if(grid.transform.GetChild(i).childCount == 0)
            {
                slots[i] = null;
            }
        }
        
    }

    /*public void LoadData(GameData data)
    {
        throw new System.NotImplementedException();
    }

    public void SaveData(ref GameData data)
    {
        *//*if (!data.containers.ContainsKey(SaveId))
        {
            data.containers[SaveId] = new GameData.SafeData(transform.position);
        }*//*

        GameData.SafeData safeData = new GameData.SafeData(transform.position);

        
        int[] slotsToSave = new int[24];
        for (var i = 0; i < slots.Length; i++)
        {
            if(slots[i] == null)
            {
                safeData.Slots[i] = -1;
            }
            else
            {
                safeData.Slots[i] = slots[i].itemId;
            }
        }

        if (!data.containers.ContainsKey(SaveId))
        {
            data.containers[SaveId] = safeData;
        }
    }*/
}
