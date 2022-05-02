using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
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
}
