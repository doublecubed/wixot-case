// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Interface for handling swipe input

using System;
using UnityEngine;

namespace WixotCase.PlayerInput
{
    public interface ISwipeInput
    {
        public event Action<Vector2> OnSwipe;
    }
}