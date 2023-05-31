// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Barebones dependency injection container.
// Customized to resolve which abilities will be used in the game.
// Also creates an instance of DefaultWeapon to be further referenced by other scripts.
// DIContainer is run before all other Monobehaviours in the game so it will properly register all dependencies.

using System;
using System.Collections.Generic;
using UnityEngine;
using WixotCase.Ability;
using WixotCase.Gameplay;
using WixotCase.Player;
using WixotCase.PlayerInput;
using WixotCase.Weaponry;

namespace WixotCase.DI
{
	public class DIContainer : MonoBehaviour
	{
		#region REFERENCES

		// Singleton declaration
		public static DIContainer Instance;

        // Input Module
        private ITapInput _tapInput;
        private ISwipeInput _swipeInput;

		// Gameplay Module
		private IGameFlow _gameFlow;

		// Player Module
		private IPlayerControls _playerControls;

        // Weaponry Module
        private IAbility[] _abilitiesUsed;

		private IPlayerWeapon _startingWeapon;
		private GameObject _weaponObject;

		[SerializeField]
		private GameObject _defaultWeaponPrefab; 
		//Since default weapon is a prefab, it is only referenced as a GameObject



		#endregion

		#region VARIABLES

		private Dictionary<Type, object> _dependencies = new Dictionary<Type, object>();

		#endregion
		
		#region MONOBEHAVIOUR
		
		private void Awake()
		{
			HandleInstance();

			SetupInput();
			SetupGameDriver();
			SetupPlayerController();
			SetupStartingWeapon();
			SetupAbilities();
		}

        #endregion

        #region METHODS

        #region Register & Resolve

		private void HandleInstance()
		{
			if (Instance != null && Instance != this) Destroy(this.gameObject);

			Instance = this;
		}

        public void Register<T>(T implementation)
		{
			_dependencies[typeof(T)] = implementation;
		}

		public T Resolve<T>()
		{
			object implementation;
			if (_dependencies.TryGetValue(typeof(T), out implementation))
			{
				return (T)implementation;
			}

			throw new Exception($"Dependency of type {typeof(T)} is not registered.");
		}

		public IAbility[] ResolveAbilities()
		{
			return _abilitiesUsed;
		}

		public IPlayerWeapon ResolveDefaultWeapon(out GameObject weaponObject)
		{
			weaponObject = _weaponObject;
			return _startingWeapon;
		}

		#endregion

		#region Dependency Setup

		private void SetupInput()
		{
			InputReceiver inputReceiver = FindObjectOfType<InputReceiver>();

			_tapInput = inputReceiver;
			Register<ITapInput>(_tapInput);

			_swipeInput = inputReceiver;
			Register<ISwipeInput>(_swipeInput);
		}

		private void SetupGameDriver()
		{
			IGameFlow gameFlow = FindObjectOfType<GameDriver>();

			_gameFlow = gameFlow;
			Register<IGameFlow>(_gameFlow);
		}

		private void SetupPlayerController()
		{
			IPlayerControls playerControls = FindObjectOfType<PlayerController>();

			_playerControls = playerControls;
			Register<IPlayerControls>(_playerControls);
		}

		private void SetupAbilities()
		{
			_abilitiesUsed = new IAbility[5];
			_abilitiesUsed[0] = new AbilityMultiShot();
			_abilitiesUsed[1] = new AbilityDoubleShot();
			_abilitiesUsed[2] = new AbilityFasterRateOfFire();
			_abilitiesUsed[3] = new AbilityFasterProjectile();
			_abilitiesUsed[4] = new AbilityFasterCharacter();			 
		}

		private void SetupStartingWeapon()
		{
			GameObject weapon = Instantiate(_defaultWeaponPrefab);
			_weaponObject = weapon;
			_startingWeapon = weapon.GetComponent<IPlayerWeapon>();

		}

		#endregion

		#endregion
	}
}