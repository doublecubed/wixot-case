// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Resolves player input for both "Tap & hold" and "Swipe" actions
// Combines two separate interfaces for tap and swipe

using System;
using UnityEngine;

namespace WixotCase.PlayerInput
{
    public class InputReceiver : MonoBehaviour, ISwipeInput, ITapInput
    {
        #region VARIABLES

        #region Inspector Variables

        [SerializeField]
        private SwipeDetectionMode _swipeDetectionMode;

        [SerializeField]
        [Tooltip("In seconds")]
        private float _swipeDetectionTime;

        [SerializeField]
        [Tooltip("In seconds")]
        private float _tapDetectionTime;

        [SerializeField]
        [Range(0f,1f)]
        [Tooltip("In screen width percentage")]
        private float _swipeDetectionPercentage;

        [SerializeField]
        [Tooltip("in centimeters")]
        private float _exactPhysicalDistance;

        [SerializeField]
        [Tooltip("In pixels")]
        private float _swipeDetectionDelta;

        #endregion

        #region Internal Variables

        private bool _fingerDown;
        private Vector2 _fingerTouchPosition;
        private float _fingerTouchTime;

        #endregion


        #endregion

        #region EVENTS

        public event Action<Vector2> OnSwipe;
        public event Action OnTap;

        #endregion

        #region MONOBEHAVIOUR

        private void Start()
        {
            CalculateSwipeDistanceInPixels();
        }

        private void Update()
        {
            ResolveFingerPosition();   
        }

        #endregion

        #region METHODS

        private void ResolveFingerPosition()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _fingerDown = true;
                _fingerTouchPosition = Input.mousePosition;
                _fingerTouchTime = Time.time;
            }

            if (Input.GetMouseButtonUp(0) &&  _fingerDown)
            {
                _fingerDown = false;

                Vector2 currentFingerPosition = Input.mousePosition;
                Vector2 fingerDelta = currentFingerPosition - _fingerTouchPosition;

                float fingerDownTime = Time.time - _fingerTouchTime;

                ResolveTap(fingerDelta.magnitude, fingerDownTime);
                ResolveSwipe(fingerDelta, fingerDownTime);
            }
        }

        private void ResolveTap(float fingerMoveDelta, float fingerDownTime)
        {
            if (fingerMoveDelta < _swipeDetectionDelta && fingerDownTime < _tapDetectionTime)
            {
                OnTap?.Invoke();
            }
        }

        private void ResolveSwipe(Vector2 fingerMoveDelta, float fingerDownTime)
        {
            if (fingerMoveDelta.magnitude > _swipeDetectionDelta && fingerDownTime < _swipeDetectionTime) 
            {
                Vector2 normalizedDelta = fingerMoveDelta.normalized;

                OnSwipe?.Invoke(normalizedDelta);
            }
        }

        private void CalculateSwipeDistanceInPixels()
        {
            if (_swipeDetectionMode == SwipeDetectionMode.ExactPixels)
            {
                return;
            }

            if (_swipeDetectionMode == SwipeDetectionMode.ScreenRatio)
            {
                _swipeDetectionDelta = Screen.width * _swipeDetectionPercentage;
            }

            if (_swipeDetectionMode == SwipeDetectionMode.PhysicalDistance)
            {
                float physicalScreenWidthInInches = Screen.width / Screen.dpi;
                float physicalScreenWidthInCm = physicalScreenWidthInInches * 2.54f;
                float physicalRatio = _exactPhysicalDistance / physicalScreenWidthInCm;

                _swipeDetectionDelta = Screen.width * physicalRatio;
            }

        }

        public bool IsFingerDown()
        {
            if (_fingerDown)
            {
                float fingerDownDuration = Time.time - _fingerTouchTime;

                return (fingerDownDuration > _tapDetectionTime) && (fingerDownDuration > _swipeDetectionTime);
            }

            return false;
        }

        #endregion

        #region ENUMS

        public enum SwipeDetectionMode
        {
            ScreenRatio,
            PhysicalDistance,
            ExactPixels
        }

        #endregion
    }
}