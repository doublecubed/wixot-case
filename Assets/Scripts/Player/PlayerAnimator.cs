// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Controls the player Animator.

using UnityEngine;
using WixotCase.Utility;
using WixotCase.Weaponry;

namespace WixotCase.Player
{
	public class PlayerAnimator : MonoBehaviour
	{
        #region REFERENCES

        private Animator _animator;

        #endregion

        #region MONOBEHAVIOUR

        private void Awake()
        {
            GetComponentsOnSelf();
        }


        #endregion

        #region METHODS

        private void GetComponentsOnSelf()
        {
            _animator = GameUtility.FindComponentOnChildren<Animator>(transform);
        }

        public void SubscribeToShootingEvent(IPlayerWeapon weapon)
        {
            weapon.OnShotFired += Shoot;
        }

        public void ToggleWalking(bool state)
		{
			_animator.SetBool("walking", state);
		}

		public void Shoot()
		{
			_animator.SetTrigger("shoot");
		}

		#endregion

	}
}