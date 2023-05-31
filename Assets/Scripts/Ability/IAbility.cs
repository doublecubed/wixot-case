// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Interface for abilities
// Each ability may require presence of additional scripts
// based on the stats they affect, but this does not need 
// to be presented in the interface.

namespace WixotCase.Ability
{
	public interface IAbility
	{
		public void Setup(object requiredScript);

		public void Activate();

		public void Deactivate();

	}
}