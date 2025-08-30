using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingState : MonoBehaviour
{
    [SerializeField] private string _saveId;
    // Dictionary of child num, and the id to go along with it
    private Dictionary<int, int> _wallControllers = new Dictionary<int, int>();

    public Dictionary<int, int> GetAllWallControllers()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            
            if (transform.GetChild(i).GetComponent<WallController>())
            {
                if(transform.GetChild(i).GetComponent<WallController>().item == null)
                {
                    _wallControllers[i] = -1;
                }
                else
                {
                    _wallControllers[i] = transform.GetChild(i).GetComponent<WallController>().item.itemId;
                }
            }
            
        }
        return _wallControllers;
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
}
