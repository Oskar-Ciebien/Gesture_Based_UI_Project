using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CustomDefaultTrackableEventHandler : DefaultObserverEventHandler
{
    // == Public Fields ==
    public static bool TrueFalse = false;

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();

        TrueFalse = true;
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();

        TrueFalse = false;
    }
}
