using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlantData
{
    string GetSaveId();
    float GetGrowthState();
    void DestroyPlant();
}
