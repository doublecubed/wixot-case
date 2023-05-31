// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Faster walk ability, makes character walk twice as fast.
// Script requires a PlayerMover script, but this is not checked/enforced (it should be).


using WixotCase.Player;

namespace WixotCase.Ability
{
	public class AbilityFasterCharacter : IAbility
	{
        #region REFERENCES

        private PlayerMover _playerMover;

        #endregion

        #region VARIABLES

        private const float _fasterWalkSpeedModifier = 2f;

        #endregion

        #region METHODS

        public void Setup(object mover)
        {
            _playerMover = mover as PlayerMover;
        }

        public void Activate()
        {
            _playerMover.ActivateFasterWalk(_fasterWalkSpeedModifier);
        }

		public void Deactivate()
		{
            _playerMover.DeactivateFasterWalk();
		}

		#endregion

	}
}