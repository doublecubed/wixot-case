// ------------------------
// Onur Ereren - May 2023
// ------------------------

// PlayerController is the entry point for the Player bundle.
// It relays the commands coming from GameDriver to movement or shooting scripts.
// Player bundle consist of PlayerController, PlayerMover, PlayerShooter,
// PlayerAnimator and PlayerAbility
// These components are tightly coupled.

using UnityEngine;
using WixotCase.Utility;

namespace WixotCase.Player
{
	public class PlayerController : MonoBehaviour, IPlayerControls
	{
		#region REFERENCES

		private PlayerMover _mover;
		private PlayerShooter _shooter;
		private PlayerAnimator _animator;

		#endregion

        #region MONOBEHAVIOUR

        private void Awake()
        {
			GetComponentsOnSelf();
        }

        #endregion

        #region METHODS

        #region Initialization

		private void GetComponentsOnSelf()
		{
			_mover = GameUtility.FindComponentOnSelf<PlayerMover>(transform);
			_shooter = GameUtility.FindComponentOnSelf<PlayerShooter>(transform);
			_animator = GameUtility.FindComponentOnSelf<PlayerAnimator>(transform);
		}

        #endregion

        #region Interface
        public void Move()
		{
			_mover.StartMovement();
            _animator.ToggleWalking(true);
		}

		public void Stop()
		{
			_mover.StopMovement();
            _animator.ToggleWalking(false);
		}
		
		public void SwitchDirection(Vector2 direction)
		{
			_mover.SwitchDirection(direction);
		}

		public void Shoot()
		{
			_shooter.ShootWeapon();
		}

        #endregion


        #endregion

    }
}