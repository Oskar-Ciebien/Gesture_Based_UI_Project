using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CustomDefaultTrackableEventHandler : DefaultObserverEventHandler
{
    // == Public Fields ==
    public static bool TrueFalse = false;

    // Connected with Image
    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();

        TrueFalse = true;
    }

    // Lost connection with Image
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();

        TrueFalse = false;
    }
}
