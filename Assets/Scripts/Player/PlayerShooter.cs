// ------------------------
// Onur Ereren - May 2023
// ------------------------

// PlayerShooter controls the shooting mechanic.
// It needs a weapon that uses the IPlayerWeapon interface
// It does not control the workings of the weapon, and thus
// is not affiliated with the abilities.

using UnityEngine;
using WixotCase.Weaponry;
using WixotCase.DI;
using WixotCase.Utility;

namespace WixotCase.Player
{
	public class PlayerShooter : MonoBehaviour
	{
        #region REFERENCES

        private PlayerAnimator _animator;

        public IPlayerWeapon CurrentWeapon { get; private set; }
        private GameObject _currentWeaponObject;

        #endregion

        #region MONOBEHAVIOUR

        private void Start()
        {
            ResolveDependencies();
            SetupStartingWeapon();
            SetShootingAnimation();
        }

        #endregion

        #region METHODS

        #region Initialization

        private void ResolveDependencies()
        {
            CurrentWeapon = DIContainer.Instance.ResolveDefaultWeapon(out GameObject weapon);
            _currentWeaponObject = weapon;
        }

        private void SetupStartingWeapon()
        {
            _currentWeaponObject.transform.parent = transform;
            _currentWeaponObject.transform.localPosition = Vector3.zero;
        }


        private void SetShootingAnimation()
        {
            _animator = GameUtility.FindComponentOnSelf<PlayerAnimator>(transform);
            _animator.SubscribeToShootingEvent(CurrentWeapon);
        }

        #endregion

        #region Weapon Use

        public void ShootWeapon()
        {
            CurrentWeapon.Shoot();
        }

        #endregion

        #endregion

    }
}