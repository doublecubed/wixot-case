// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Interface for player weapons

using System;

namespace WixotCase.Weaponry
{
	public interface IPlayerWeapon
	{
		public event Action OnShotFired;

		public void Shoot();
	}
}