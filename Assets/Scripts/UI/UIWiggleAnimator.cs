// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Wiggles a UI element up and down with a Sine function


using UnityEngine;

namespace WixotCase.UI
{
	public class UIWiggleAnimator : MonoBehaviour
	{

		#region VARIABLES

		private Vector2 _startingPosition;

		[SerializeField]
		[Tooltip("in pixels on screen")]
		private float _wiggleDistance;

		[SerializeField]
		[Tooltip("Frequency of the sine function")]
		private float _wiggleSpeed;

        #endregion

        #region MONOBEHAVIOUR

        private void Start()
        {
            _startingPosition = transform.position;
        }

		private void Update()
		{
			transform.position = _startingPosition + Vector2.up * Mathf.Sin(_wiggleSpeed * Time.time) * _wiggleDistance;
		}

        #endregion

    }
}