using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Item thisItem;
    public AudioSource pickupSound;
    void Start()
    {
        pickupSound = transform.Find("Audio Source").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!GameObject.Find("ItemManager").GetComponentInChildren<ItemManager>().InventoryFull())
            {
                GameObject.Find("ItemManager").GetComponentInChildren<ItemManager>().AddItem(thisItem);
                // Play a sound or give input that you picked up an item
                pickupSound.Play();
                pickupSound.transform.SetParent(null);

                Destroy(pickupSound, 3f);
                Destroy(gameObject);
            }
            
        }
    }
}
