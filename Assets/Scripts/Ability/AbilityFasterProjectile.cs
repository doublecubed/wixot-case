// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Faster projectile ability, makes projectiles move twice as fast.
// Script requires a DefaultWeapon script,  but this is not checked/enforced (it should be).


using WixotCase.Weaponry;

namespace WixotCase.Ability
{
	public class AbilityFasterProjectile : IAbility
	{
		#region REFERENCES

		private DefaultWeapon _weaponScript;

		#endregion

		#region VARIABLES

		private bool _isActive;

		private const float _projectileSpeedModifier = 2.0f;

		#endregion

        #region METHODS

        public void Setup(object weapon)
        {
            _weaponScript = weapon as DefaultWeapon;
        }

        public bool IsActive()
		{
			return _isActive;
		}

		public void Activate()
		{
			_weaponScript.ActivateFasterProjectile(_projectileSpeedModifier);
		}

		public void Deactivate()
		{
			_weaponScript.DeactivateFasterProjectile();
		}

		#endregion

	}
}