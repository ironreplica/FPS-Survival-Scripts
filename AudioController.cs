using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip punchSound;
    public AudioClip pickupSound;
    public AudioClip craftSound;
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void playSound(string soundName)
    {
        if(soundName == "punch")
        {
            audioSource.PlayOneShot(punchSound);
        }
        else if(soundName == "pickup")
        {
            audioSource.PlayOneShot(pickupSound);
        }
        else if(soundName == "craft")
        {
            audioSource.PlayOneShot(craftSound);
        }
    }
}
