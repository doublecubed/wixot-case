// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Interface for the Gameplay module.
// Enforces presence of basic game flow mechanics.


namespace WixotCase.Gameplay
{
    public interface IGameFlow
    {
        public void StartGame();

        public void TogglePauseGame();

        public void StopGame();

    }
}