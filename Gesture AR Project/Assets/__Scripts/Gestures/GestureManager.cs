using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestureManager : MonoBehaviour
{
    // == Private Fields ==
    private Touch touch;
    private Vector2 startingTouchPosition;
    private Vector2 endingTouchPosition;
    private Vector2 touchPosition;
    public static GestureManager instance;

    private void Awake()
    {
        // Create singleton
        GameObject[] go = GameObject.FindGameObjectsWithTag("GestureManager");

        // If there is more than one GestureManager
        if (go.Length > 1)
        {
            // Destroy it
            Destroy(this.gameObject);
        }
        else // Otherwise
        {
            // Don't destroy it
            DontDestroyOnLoad(this.gameObject);
            // Set it as Singleton
            instance = this;
        }
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game Scene")
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

                        // Only if Vuforia is tracking the image
                        if (CustomDefaultTrackableEventHandler.TrueFalse == true)
                        {
                            // Call movement on the position
                            PaddleBehaviour.Movement(touchPosition);
                        }

                        break;

                    case TouchPhase.Ended:
                        endingTouchPosition = touch.position;
                        break;
                }

                // If tapped without swipe
                if (startingTouchPosition == endingTouchPosition)
                {
                    if (SceneManager.GetActiveScene().name == "Game Scene")
                    {
                        // Start off the ball
                        BallBehaviour.StartBall();

                        print("Tap Gesture Recognised!");
                    }

                }
            }
        }
    }
}
