// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Interface for controlling the player character
// Includes the character movement, direction switching
// and shooting commands

using UnityEngine;

namespace WixotCase.Player
{
    public interface IPlayerControls
    {
        public void Move();

        public void Stop();

        public void SwitchDirection(Vector2 direction);

        public void Shoot();

    }
}