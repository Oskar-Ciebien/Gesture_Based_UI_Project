using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    // == Private Fields ==
    private Touch touch;
    private Vector2 startingTouchPosition;
    private Vector2 endingTouchPosition;
    private Vector2 touchPosition;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            // Types of touch phases
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startingTouchPosition = touch.position;
                    break;

                case TouchPhase.Moved:
                    touchPosition = touch.deltaPosition;

                    // Call movement on the position
                    PaddleBehaviour.Movement(touchPosition);
                    break;

                case TouchPhase.Ended:
                    endingTouchPosition = touch.position;
                    break;
            }

            // If tapped without swipe
            if (startingTouchPosition == endingTouchPosition)
            {
                // if (touch.fingerId == 0)
                // {
                // Start off the ball
                BallBehaviour.StartBall();
                // }

                print("Tap Gesture Recognised!");
            }
        }
    }
}
