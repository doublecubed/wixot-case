// ------------------------
// Onur Ereren - May 2023
// ------------------------

// DefaultWeapon is the only implementation of IPlayerWeapon interface in the game.
// It contains the stats that abilities can manipulate

using System;
using System.Collections;
using UnityEngine;
using WixotCase.Player;

namespace WixotCase.Weaponry
{
	public class DefaultWeapon : MonoBehaviour, IPlayerWeapon
	{
		#region REFERENCES

		[SerializeField]
		private GameObject _projectilePrefab;

        [SerializeField]
        private PlayerMover _mover;

        #endregion

        #region VARIABLES

        // Shooting Speed - Rate of Fire
        [SerializeField]
        [Tooltip("in seconds per round")]
        private float _rateOfFire;

        // Projectile Speed
        [SerializeField]
        [Tooltip("in meters per second")]
        private float _projectileSpeed;

        // Multishot stats
        [SerializeField]
        [Tooltip("in degrees")]
        private float _multiShotSecondAngle;

        // DoubleShot stats
        [SerializeField]
        [Tooltip("time between two consecutive shots, in seconds")]
        private float _doubleShotInterval;

        // Internal Variables
        private bool _isCoolingDown;

        private float _rateOfFireModifier;
        private const float _defaultRateOfFireModifier = 1f;

        private float _projectileSpeedModifier;
        private const float _defaultProjectileSpeedModifier = 1f;
        
        private bool _multiShotEnabled;
        private bool _doubleShotEnabled;

        #endregion

        #region EVENTS

        public event Action OnShotFired;

        #endregion

        #region MONOBEHAVIOUR

        private void Start()
        {
            SetInternalVariables();
        }

        #endregion

        #region METHODS

        #region Initialization

        public void SetPlayerMover(PlayerMover playerMover)
        {
            _mover = playerMover;
        }

        private void SetInternalVariables()
        {
            _rateOfFireModifier = _defaultRateOfFireModifier;
            _projectileSpeedModifier = _defaultProjectileSpeedModifier;
            _multiShotEnabled = false;
            _doubleShotEnabled = false;
        }

        #endregion

        #region Interface

        public void Shoot()
		{
            if (_isCoolingDown) return;
            _isCoolingDown = true;

            OnShotFired?.Invoke();

            StartCoroutine(ReleaseProjectiles(0f, _mover.Facing));

            if (_multiShotEnabled)
            {
                float angleOne = _mover.Facing * _multiShotSecondAngle;
                float angleTwo = _mover.Facing * (_multiShotSecondAngle + 90f);

                StartCoroutine(ReleaseProjectiles(angleOne, _mover.Facing));
                StartCoroutine(ReleaseProjectiles(angleTwo, _mover.Facing));
            }

            StartCoroutine(CooldownCountdown());
		}

        #endregion

        #region Weapon Specific (Abilities)

        public void ToggleMultiShot()
        {
            _multiShotEnabled = !_multiShotEnabled;
        }

        public void ToggleDoubleShot()
        {
            _doubleShotEnabled = !_doubleShotEnabled;
        }

        public void ActivateFasterProjectile(float newModifier)
        {
            _projectileSpeedModifier = newModifier;
        }

        public void DeactivateFasterProjectile()
        {
            _projectileSpeedModifier = _defaultProjectileSpeedModifier;
        }

        public void ActivateFasterRateOfFire(float newModifier)
        {
            _rateOfFireModifier = newModifier;
        }

        public void DeactivateFasterRateOfFire()
        {
            _rateOfFireModifier = _defaultRateOfFireModifier;
        }

        #endregion

        #endregion

        #region COROUTINES

        private IEnumerator CooldownCountdown()
        {
            yield return new WaitForSeconds(_rateOfFire * _rateOfFireModifier);
            _isCoolingDown = false;
        }

        private IEnumerator ReleaseProjectiles(float angle, int direction)
        {
            int numberOfProjectiles = _doubleShotEnabled ? 2 : 1;

            for (int i = 0; i < numberOfProjectiles; i++)
            {
                GameObject projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
                Projectile projectileScript = projectile.GetComponent<Projectile>();

                Quaternion rotationAmount = Quaternion.Euler(0f, 0f, angle);

                projectileScript.Speed = rotationAmount * (Vector2.right * direction * _projectileSpeed * _projectileSpeedModifier);

                yield return new WaitForSeconds(_doubleShotInterval);
            }
        }

        #endregion
    }
}