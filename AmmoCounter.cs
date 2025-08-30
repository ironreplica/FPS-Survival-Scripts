using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AmmoCounter : MonoBehaviour
{
    public void SetAmmo(int cur, int max)
    {
        Debug.Log(cur + "/" + max);
        GetComponent<TextMeshProUGUI>().text = cur + "/" + max;
    }
}
