using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource Audio;

    public AudioClip Bounce;

    public AudioClip Clash;

    public static SFXManager sfxInstance;

    private void Awake()
    {
        if(sfxInstance != null && sfxInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        sfxInstance = this;
        DontDestroyOnLoad(this);
    }


}
