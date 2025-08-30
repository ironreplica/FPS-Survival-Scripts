using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class HealthController : MonoBehaviour
{
    [SerializeField] public GameObject particles;
    [SerializeField] private string _saveId;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float curHealth;
    public GameObject itemDrop;
    [SerializeField] private bool isBroken;
    public int accessLevel = 0; // Required access level by item, 0 being fists
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
    public bool IsBroken
    {
        get
        {
            return isBroken;
        }
        set
        {
            isBroken = value;
        }
    }

    void Start()
    {
        curHealth = maxHealth;
    }
    private void Awake()
    {
        if (IsBroken)
        {     
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;    
        }
    }

    public void TakeDamage(float damage, int access)
    {
        // Find a way to keep this entry and file and "destroy" the object while not actually destroying this script and an empty
        if (access == accessLevel)
        {
            // Non tree objects
            curHealth -= damage;
            if (curHealth <= 0)
            {
                GameObject itemObj = Instantiate(itemDrop, null, true);
                itemObj.transform.position = new Vector3(transform.position.x, itemObj.transform.position.y, transform.position.z);
                /*if (CompareTag("Tree"))
                {
                    
                }*/
                IsBroken = true;
                GetComponent<Collider>().enabled = false;
                GetComponent<MeshRenderer>().enabled = false;
                itemObj.transform.SetParent(null);
                /*Destroy(gameObject);*/
            }
        }
        else if(accessLevel == 0)
        {
            // Tree
            curHealth -= damage;
            if (curHealth <= 0)
            {
                GameObject itemObj = Instantiate(itemDrop, null, true);
                itemObj.transform.position = new Vector3(transform.position.x, itemObj.transform.position.y, transform.position.z);
                // itemObj.transform.SetParent(null);
            
                if (CompareTag("Tree"))
                {
                    IsBroken = true;
                    IsBroken = true;
                    GetComponent<CapsuleCollider>().enabled = false;
                    GetComponent<MeshRenderer>().enabled = false;
                }
                /*Destroy(gameObject);*/

            }
        }
        if (gameObject.CompareTag("Enemy"))
        {
            if (gameObject.transform.Find("Canvas").transform.Find("Slider").GetComponent<Slider>())
            {
                gameObject.GetComponent<ZombieController>().curTarget = GameObject.Find("Player");
                gameObject.transform.Find("Canvas").transform.Find("Slider").GetComponent<Slider>().value = curHealth;
                if(curHealth <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    
}
