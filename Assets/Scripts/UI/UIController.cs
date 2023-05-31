// ------------------------
// Onur Ereren - May 2023
// ------------------------

// UIController controls the UI functions.
// Turns panels on and off,
// As well as activating/deactivating of ability buttons.


using UnityEngine;
using UnityEngine.UI;

namespace WixotCase.UI
{
	public class UIController : MonoBehaviour
	{
		#region REFERENCES

		[SerializeField]
		private GameObject _menuPanel;

		[SerializeField]
		private GameObject _gamePanel;

		[SerializeField]
		private Button[] _buttons;

		private Image[] _buttonImages;

		#endregion

		#region VARIABLES

		[SerializeField]
		private Color _enabledButtonColor;
		[SerializeField]
		private Color _disabledButtonColor;
		[SerializeField]
		private Color _activeButtonColor;

		private int _numberOfActiveButtons;
		[SerializeField]
		private int _maxActiveButtons;

		private bool[] _buttonActive;

        #endregion

        #region MONOBEHAVIOUR

        private void Awake()
        {
            GetReferencesOnSelf();
        }

        private void Start()
        {
            _maxActiveButtons = 3;
			_buttonActive = new bool[_buttons.Length];
			DisplayMenuPanel();
        }

        #endregion

        #region METHODS

        #region Initialization

        private void GetReferencesOnSelf()
		{
			Debug.Log(_buttons.Length);
			_buttonImages = new Image[_buttons.Length];

			for (int i=0; i < _buttons.Length; i++)
			{
				Debug.Log(_buttons[i]);
                _buttonImages[i] = _buttons[i].GetComponent<Image>();
			}
		}

		

        #endregion

        #region Ability Button Manipulation

        public void ToggleButton(int index)
		{
			if (!_buttonActive[index])
			{
				ActivateButton(index);
				_buttonActive[index] = true;
				_numberOfActiveButtons++;
			} else
			{
				DeactivateButton(index);
				_buttonActive[index] = false;
				_numberOfActiveButtons--;
			}

			CheckMaxButtonActivation();
		}

		private void CheckMaxButtonActivation()
		{
			if (_numberOfActiveButtons >= _maxActiveButtons)
			{
				DisableInactiveButtons();
			} else
			{
				EnableDisabledButtons();
			}
		}

		private void DisableInactiveButtons()
		{
			for (int i=0; i<_buttons.Length; i++)
			{
				if (!_buttonActive[i])
				{
                    DisableButton(i);
                }
			}
		}

		private void EnableDisabledButtons()
		{
            for (int i = 0; i < _buttons.Length; i++)
            {
                if (!_buttons[i].enabled)
                {
					_buttons[i].enabled = true;
					EnableButton(i);
                }
            }
        }

	
		private void EnableButton(int index)
		{
			_buttonImages[index].color = _enabledButtonColor;
			_buttons[index].enabled = true;
		}

		private void DisableButton(int index)
		{
			_buttonImages[index].color = _disabledButtonColor;
			_buttons[index].enabled = false;
		}

		private void ActivateButton(int index)
		{
			_buttonImages[index].color = _activeButtonColor;
		}

		private void DeactivateButton(int index)
		{
			_buttonImages[index].color = _enabledButtonColor;
		}

		#endregion

		#region Panel Manipulation

		public void DisplayGamePanel()
		{
			_gamePanel.SetActive(true);
			_menuPanel.SetActive(false);
		}

		public void DisplayMenuPanel()
		{
			_gamePanel.SetActive(false);
			_menuPanel.SetActive(true);
		}

		#endregion

		#endregion

	}
}