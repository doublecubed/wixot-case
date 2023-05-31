// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Controls the game flow. IGameFlow interface provides basic game flow functions.
// Receives input and controls the player
// "Autoshoot" is enabled as the only choice, it is
// equivalent to the player pressing down a "shoot" button
// continuously. Further shooting mechanics are not implemented

using UnityEngine;
using WixotCase.DI;
using WixotCase.Player;
using WixotCase.PlayerInput;

namespace WixotCase.Gameplay
{
	public class GameDriver : MonoBehaviour, IGameFlow
	{
		#region REFERENCES

		private ITapInput _tapInput;
		private ISwipeInput _swipeInput;

		private IPlayerControls _playerController;

		#endregion

		#region VARIABLES

		[SerializeField]
		private bool _autoShoot;

		public bool IsRunning { get; private set; }

        #endregion

        #region MONOBEHAVIOUR
		
        private void Start()
        {
			ResolveDependencies();
			SubscribeToEvents();
			InitializeVariables();
		}

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IsRunning = !IsRunning;
            }

            if (!IsRunning) return;

            ResolvePlayerMovement();

			ResolvePlayerShooting();


        }

        #endregion

        #region METHODS

        #region Initialization

		private void ResolveDependencies()
		{
			_tapInput = DIContainer.Instance.Resolve<ITapInput>();
			_swipeInput = DIContainer.Instance.Resolve<ISwipeInput>();

			_playerController = DIContainer.Instance.Resolve<IPlayerControls>();
		}

        private void InitializeVariables()
		{
			IsRunning = false;
		}

		private void SubscribeToEvents()
		{
			_swipeInput.OnSwipe += SwitchPlayerDirection;
		}
		
        #endregion

        #region Game Flow

        public void StartGame()
		{
			IsRunning = true;
		}

		public void TogglePauseGame()
		{
			IsRunning = !IsRunning;
		}

		public void StopGame()
		{
			IsRunning = false;
		}

		#endregion

		#region Gameplay

		private void ResolvePlayerMovement()
		{
			if (_tapInput.IsFingerDown())
			{
				_playerController.Move();
			} else
			{
				_playerController.Stop();
			}
		}

		private void SwitchPlayerDirection(Vector2 direction) 
		{
			if (!IsRunning) return;

			_playerController.SwitchDirection(direction);
		}

		private void ResolvePlayerShooting()
		{
			if (!_autoShoot) return;

			_playerController.Shoot();
		}

		#endregion

		#endregion

	}
}