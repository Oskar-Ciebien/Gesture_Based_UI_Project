using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    private Touch touch;
    private Vector2 startingTouchPosition;
    private Vector2 endingTouchPosition;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startingTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    endingTouchPosition = touch.position;
                    break;
            }

            if (startingTouchPosition == endingTouchPosition)
            {
                // if (touch.fingerId == 0)
                // {
                BallBehaviour.StartBall();
                // }

                print("Tap Gesture Recognised!");
            }
        }
    }
}
