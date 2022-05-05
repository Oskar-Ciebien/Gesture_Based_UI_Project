using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMusic : MonoBehaviour
{
    public static BGMusic BGInstance;

    public AudioSource _audio;


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

  

}
