// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Rate Of Fire ability, makes bullets move twice as fast.
// Script requires a DefaultWeapon script,  but this is not checked/enforced (it should be).

using WixotCase.Weaponry;

namespace WixotCase.Ability
{
    public class AbilityFasterRateOfFire: IAbility
    {
        #region REFERENCES

        private DefaultWeapon _weaponScript;

        #endregion

        #region VARIABLES

        private const float _rateOfFireModifier = 0.5f;

        #endregion

        #region METHODS

        public void Setup(object weapon)
        {
            _weaponScript = weapon as DefaultWeapon;
        }

        public void Activate()
        {
            _weaponScript.ActivateFasterRateOfFire(_rateOfFireModifier);
        }

        public void Deactivate()
        {
            _weaponScript.DeactivateFasterRateOfFire();
        }



        #endregion
    }
}