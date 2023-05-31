// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Performs character movement by setting rigidbody velocity

using UnityEngine;
using WixotCase.Utility;

namespace WixotCase.Player
{
	public class PlayerMover : MonoBehaviour
	{
		#region REFERENCES

        [SerializeField]
        private SpriteRenderer _renderer;       // Placed in the inspector
        private Rigidbody2D _rigidbody;

        #endregion

        #region VARIABLES

        [SerializeField]
        private float _baseWalkSpeed;
        private float _walkSpeedModifier;
        private const float _defaultWalkSpeedModifier = 1f;

        public int Facing { get; private set; }

        #endregion

        #region MONOBEHAVIOUR

        private void Awake()
        {
            GetComponentsOnSelf();
        }

        private void Start()
        {
            InitializeVariables();
        }


        #endregion

        #region METHODS

        #region Initialization

        private void GetComponentsOnSelf()
        {
            _rigidbody = GameUtility.FindComponentOnSelf<Rigidbody2D>(transform);
        }

        private void InitializeVariables()
        {
            Facing = 1;
            _renderer.flipX = false;
            _walkSpeedModifier = _defaultWalkSpeedModifier;
        }


        #endregion

        #region Movement

        public void StartMovement()
        {
            _rigidbody.velocity = Facing * Vector2.right * _baseWalkSpeed * _walkSpeedModifier;
        }

        public void StopMovement()
        {
            _rigidbody.velocity = Vector2.zero;
        }

        public void SwitchDirection(Vector2 direction)
        {
            if (direction.x > 0f && Facing == -1)
            {
                Facing = 1;
                _renderer.flipX = false;
            }

            if (direction.x < 0f && Facing == 1)
            {
                Facing = -1;
                _renderer.flipX = true;
            }
        }

        #endregion

        #region Abilities

        public void ActivateFasterWalk(float newModifier)
        {
            _walkSpeedModifier = newModifier;
        }

        public void DeactivateFasterWalk()
        {
            _walkSpeedModifier = _defaultWalkSpeedModifier;
        }

        #endregion

        #endregion

    }
}