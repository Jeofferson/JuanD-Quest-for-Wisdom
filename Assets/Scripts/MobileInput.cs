using UnityEngine;

public class MobileInput : MonoBehaviour
{

    private const float DEAD_ZONE = 100.0f;

    public static bool tap;
    public static bool swipeLeft;
    public static bool swipeUp;
    public static bool swipeRight;
    public static bool swipeDown;

    private Vector2 swipeDelta, startTouch;

    private void Update()
    {
        // Reset all the booleans
        tap = swipeLeft = swipeRight = swipeDown = swipeUp = false;

        #region Standalone inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            startTouch = swipeDelta = Vector2.zero;
        }
        #endregion

        #region Mobile inputs
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                startTouch = Input.mousePosition;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                startTouch = swipeDelta = Vector2.zero;
            }
        }
        #endregion

        // Calculate the distance of drag or swipe
        swipeDelta = Vector2.zero;

        if (startTouch != Vector2.zero)
        {
            // Mobile inputs
            if (Input.touches.Length != 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            // Standalone inputs
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        // Check if beyond the dead zone
        if (swipeDelta.magnitude > DEAD_ZONE)
        {
            // Valid swipe
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                // Horizontal swipe
                if (x > 0)
                {
                    swipeRight = true;
                }
                else
                {
                    swipeLeft = true;
                }
            }
            else
            {
                // Vertical swipe
                if (y > 0)
                {
                    swipeUp = true;
                }
                else
                {
                    swipeDown = true;
                }
            }

            startTouch = swipeDelta = Vector2.zero;
        }
    }

    public Vector2 SwipeDelta { get { return swipeDelta; } }

}