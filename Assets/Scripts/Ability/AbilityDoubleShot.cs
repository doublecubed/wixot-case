// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Doubleshot ability, makes DefaultWeapon firing two consecutive bullets.
// Script requires a DefaultWeapon script,  but this is not checked/enforced (it should be).


using WixotCase.Weaponry;

namespace WixotCase.Ability
{
	public class AbilityDoubleShot : IAbility
	{
		#region REFERENCES

		private DefaultWeapon _weaponScript;

		#endregion

		#region METHODS

		public void Setup(object weapon)
		{
			_weaponScript = weapon as DefaultWeapon;
		}

		public void Activate()
		{
			_weaponScript.ToggleDoubleShot();
		}

		public void Deactivate()
		{
			_weaponScript.ToggleDoubleShot();
		}

		#endregion

	}
}