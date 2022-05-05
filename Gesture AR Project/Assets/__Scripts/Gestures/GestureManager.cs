using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestureManager : MonoBehaviour
{
    // == Public Fields ==
    public GestureManager gestureManager;

    // == Private Fields ==
    private Touch touch;
    private Vector2 startingTouchPosition;
    private Vector2 endingTouchPosition;
    private Vector2 touchPosition;

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
            gestureManager = this;
        }
    }

    void Update()
    {
        // If on Game Scene
        if (SceneManager.GetActiveScene().name == "Game Scene")
        {
            if (CustomDefaultTrackableEventHandler.TrueFalse == true)
            {
                if (Input.touchCount > 0)
                {
                    touch = Input.GetTouch(0);

                    // Types of touch phases
                    switch (touch.phase)
                    {
                        // Touch Began
                        case TouchPhase.Began:
                            //Set starting position
                            startingTouchPosition = touch.position;
                            break;

                        // Swiping Gesture
                        case TouchPhase.Moved:
                            // Update the touch position
                            touchPosition = touch.deltaPosition;

                            // Only if Vuforia is tracking the image
                            if (CustomDefaultTrackableEventHandler.TrueFalse == true)
                            {
                                // Call movement on the position
                                PaddleBehaviour.Movement(touchPosition);
                            }
                            break;

                        // Touch Ended
                        case TouchPhase.Ended:
                            // Set the ending touch position
                            endingTouchPosition = touch.position;
                            break;
                    }

                    // If tapped without swipe - (Tap Gesture)
                    if (startingTouchPosition == endingTouchPosition)
                    {
                        // Start off the ball
                        BallBehaviour.StartBall();

                        // print("Tap Gesture Recognised!");
                    }
                }
            }
        }
    }
}
