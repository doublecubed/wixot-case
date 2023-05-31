// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Camera controller that scrolls the camera left and right with the player
// Stops when it arrives at level boundaries, and lets the player continue


using UnityEngine;

namespace WixotCase.Gameplay
{
	public class CameraController : MonoBehaviour
	{
		#region REFERENCES

		[SerializeField]
		private Transform _playerTransform;

        #endregion

        #region VARIABLES

        [SerializeField]
        private float _levelBoundLeft;
        [SerializeField]
        private float _levelBoundRight;

        private Vector3 _defaultCameraPosition;
        private float _cameraViewHalfWidth;
        private float _cameraLeftLimit;
        private float _cameraRightLimit;

        #endregion

        #region MONOBEHAVIOUR

        private void Start()
        {
            InitializeVariables();
        }

        private void LateUpdate()
        {
            UpdateCameraPosition();
        }

        #endregion

        #region METHODS

        private void InitializeVariables()
        {
            _defaultCameraPosition = transform.position;
            _cameraViewHalfWidth = GetComponent<Camera>().orthographicSize * 0.5f;

            _cameraLeftLimit = _levelBoundLeft + _cameraViewHalfWidth;
            _cameraRightLimit = _levelBoundRight - _cameraViewHalfWidth;
        }

        private void UpdateCameraPosition()
        {
            float cameraXPosition = Mathf.Clamp(_playerTransform.position.x, _cameraLeftLimit, _cameraRightLimit);

            Vector3 finalPosition = new Vector3(cameraXPosition, _defaultCameraPosition.y, _defaultCameraPosition.z);

            transform.position = finalPosition;
        }

        #endregion

    }
}