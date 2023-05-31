// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Multishot ability, makes DefaultWeapon fire diagonal bullets in both directions.
// Script requires a DefaultWeapon script,  but this is not checked/enforced (it should be).


using WixotCase.Weaponry;

namespace WixotCase.Ability
{
    public class AbilityMultiShot : IAbility
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
            _weaponScript.ToggleMultiShot();
        }

        public void Deactivate()
        {
            _weaponScript.ToggleMultiShot();
        }



        #endregion
    }
}