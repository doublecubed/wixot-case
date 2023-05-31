// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Projectile script. Handles projectile movement and destruction

using UnityEngine;
using WixotCase.Utility;

namespace WixotCase.Weaponry
{
    public class Projectile : MonoBehaviour
    {
        #region REFERENCES

        private Rigidbody2D _rigidBody;

        #endregion

        #region VARIABLES

        public Vector2 Speed { get; set; }

        #endregion

        #region MONOBEHAVIOUR

        private void Awake()
        {
            GetComponentsOnSelf();
        }

        private void Start()
        {
            StartMoving();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(this.gameObject);
        }

        #endregion

        #region METHODS

        private void GetComponentsOnSelf()
        {
            _rigidBody = GameUtility.FindComponentOnSelf<Rigidbody2D>(transform);
        }

        private void StartMoving()
        {
            _rigidBody.velocity = Speed;
        }

        #endregion

    }
}