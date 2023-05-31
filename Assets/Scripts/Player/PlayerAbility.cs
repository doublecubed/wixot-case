// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Script that controls player ability manipulation
// Resolves the abilities to be used through the DIContainer
// SetupAbilities method does hardcoded selection for ability dependencies.
// I tried to use generics for this, but it was taking too long to properly implement.
// I left in the previous version with generics as comment.

using UnityEngine;
using WixotCase.DI;
using WixotCase.Weaponry;
using WixotCase.Utility;
using WixotCase.Ability;

namespace WixotCase.Player
{
	public class PlayerAbility : MonoBehaviour
	{
		#region REFERENCES

		private IAbility[] _abilities;

        [SerializeField]
        private DefaultWeapon _defaultWeapon;
        private PlayerMover _playerMover;

        #endregion

        #region VARIABLES

        private bool[] _isActive;

        [SerializeField]
        [Tooltip("Number of abilities that can be activated simultaneously")]
        private int _maxActiveAbilities;

        #endregion

        #region MONOBEHAVIOUR

        private void Awake()
        {
            GetComponentsOnSelf();
        }

        private void Start()
        {
            ResolveDependencies();
            InitializeVariables();
            SetupAbilities();
        }

        #endregion

        #region METHODS

        #region Initialization

        private void GetComponentsOnSelf()
        {
            _playerMover = GameUtility.FindComponentOnSelf<PlayerMover>(transform);
        }

        private void ResolveDependencies()
        {
            _abilities = DIContainer.Instance.ResolveAbilities();
            _defaultWeapon = DIContainer.Instance.ResolveDefaultWeapon(out GameObject weapon) as WixotCase.Weaponry.DefaultWeapon;
            _defaultWeapon.SetPlayerMover(_playerMover);
        }

        private void InitializeVariables()
        {
            _isActive = new bool[_abilities.Length];
        }


        private void SetupAbilities()
        {
            for (int i=0; i< _abilities.Length; i++)
            {
                if (_abilities[i].GetType() == typeof(AbilityFasterCharacter))
                {
                    _abilities[i].Setup(_playerMover);
                } else
                {
                    _abilities[i].Setup(_defaultWeapon);
                }
            }
        }

        /*
        private void SetupAbilities()
        {
            Debug.Log(_abilities.Length);

            for (int i = 0; i < _abilities.Length; i++)
            {

                Type abilityType = _abilities[i].GetType();
                MethodInfo setupMethod = abilityType.GetMethod("Setup");

                Type[] genericArguments = setupMethod.GetGenericArguments();

                foreach (Type genericArgument in genericArguments)
                {
                    Debug.Log("Generic Argument: " + genericArgument.Name);
                }

                Debug.Log(genericArguments.Length +  " generic arguments");
                Debug.Log(genericArguments[0].GetType());

                if (genericArguments[0] == typeof(PlayerMover))
                {
                    Debug.Log("PlayerMover type");
                    _abilities[i].Setup(_playerMover);
                }

                if (genericArguments[0] == typeof(DefaultWeapon))
                {
                    Debug.Log("DefaultWeapon type");
                    _abilities[i].Setup(_defaultWeapon);
                }

            }
        }
        */
        #endregion

        #region Ability Activation

        public void ToggleAbility(int index)
        {
            if (_isActive[index])
            {
                _abilities[index].Deactivate();
            } else
            {
                _abilities[index].Activate();
            }

            _isActive[index] = !_isActive[index];
        }

        #endregion

        #endregion

    }
}