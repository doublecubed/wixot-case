// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Interface for handling tap input
// OnTap event is not utilized in the scope of this project.

using System;

namespace WixotCase.PlayerInput
{
    public interface ITapInput
    {
        public event Action OnTap;
        public bool IsFingerDown();
    }
}