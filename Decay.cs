using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decay : MonoBehaviour
{
    public float seconds = 10f;
    private void Start()
    {
        StartCoroutine(destroyAfterSeconds());
    }
    private IEnumerator destroyAfterSeconds()
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
