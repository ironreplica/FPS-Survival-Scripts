using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaplingController : MonoBehaviour, IPlantData
{
    public GameObject treePrefab;
    // Change this value to something longer.
    [SerializeField] private float totalGrowTime = 60f;
    private float curGrowTime;
    [SerializeField] private string _saveId;

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
    // Update is called once per frame
    void Update()
    {
        curGrowTime -= Time.deltaTime;
        Debug.Log(curGrowTime);
        if (curGrowTime <= 0)
        {
            growUp();
        }
    }
    private void growUp()
    {
        // Create tree
        // Destroy
    }

    public float GetGrowthState()
    {
        return curGrowTime;
    }

    public void DestroyPlant()
    {
        Destroy(gameObject);
    }

    public string GetSaveId()
    {
        return SaveId;
    }
}
