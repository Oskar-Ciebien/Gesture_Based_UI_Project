using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMusic : MonoBehaviour
{
    // == Singleton ==
    public static BGMusic BGInstance;

    private void Awake()
    {
        if (BGInstance != null && BGInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        BGInstance = this;
        DontDestroyOnLoad(this);
    }


    public AudioSource _audio;
}
