using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public bool isBuilt;
    public Item item;
    public AudioSource audioSrc;
    void Start()
    {
        isBuilt = false;
        audioSrc = gameObject.transform.parent.Find("Audio Source").GetComponent<AudioSource>();
    }
    public void built(Item it)
    {
        isBuilt = true;
        // not being set by build conotrollers
        Debug.Log(it.itemName);
        item = it;
        audioSrc.Play();
    }
}
