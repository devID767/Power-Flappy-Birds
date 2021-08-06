using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SwipeDirection
{
    Up,
    Down,
    Left,
    Right,
    None
}
public struct SwipeData
{
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public SwipeDirection Direction;
}

public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerDownPosition = Vector2.zero;
    private Vector2 fingerUpPosition = Vector2.zero;

    [SerializeField]
    private bool detectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private float minDistanceForSwipe = 20f;

    public static event Action<SwipeData> OnSwipe = delegate { };

    private void Update()
    {
        if (GameStatus.Current == GameStatusEnum.IsPlaying)
        {
            foreach (Touch touch in Input.touches)
            {
                if (Settings.SlideControl)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        fingerUpPosition = touch.position;
                        fingerDownPosition = touch.position;
                    }

                    if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
                    {
                        fingerDownPosition = touch.position;
                        DetectSwipe();
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        fingerDownPosition = touch.position;
                        DetectSwipe();
                    }
                }
                else
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                        {
                            fingerUpPosition = touch.position;
                            fingerDownPosition = touch.position;
                            SendSwipe(SwipeDirection.None);
                        }
                    }
                }
            }
        }
    }

    private void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            if (IsVerticalSwipe())
            {
                var direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                SendSwipe(direction);
            }
            else
            {
                var direction = fingerDownPosition.x - fingerUpPosition.x > 0 ? SwipeDirection.Right: SwipeDirection.Left;
                SendSwipe(direction); 
            }
            fingerUpPosition = fingerDownPosition;
        }
        else if (detectSwipeOnlyAfterRelease)
        {
            SendSwipe(SwipeDirection.None);
            fingerUpPosition = fingerDownPosition;
        }
    }

    private bool IsVerticalSwipe()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }

    private void SendSwipe(SwipeDirection direction)
    {
        SwipeData swipeData = new SwipeData()
        {
            Direction = direction,
            StartPosition = fingerDownPosition,
            EndPosition = fingerUpPosition
        };
        OnSwipe(swipeData);
    }
}
