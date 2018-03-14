using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class PlayerController : MonoBehaviour
    {
        public Moving movement;
        public Shooting shoot;

        #region Unity Functions
        // Update is called once per frame
        void Update()
        {
            Shoot();
            Movement();
        }
        #endregion

        #region Custom Functions
        // Handles Shooting Functionality
        void Shoot()
        {
            // If Space is pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Fire!
                shoot.Fire(transform.up);
            }
        }
        void Movement()
        {
            float inputV = Input.GetAxis("Vertical");
            float inputH = Input.GetAxis("Horizontal");
            // Check if up arrow is pressed
            if (inputV > 0)
            {
                // Accelerate player
                movement.Accelerate(transform.up);
            }
            // Rotate depending on what inputH direction is
            //movement.Rotate(inputH);

            // if right arrow is pressed
            if (Input.GetKey(KeyCode.RightArrow))
            {
                movement.RotateRight();
            }
            // rotate right

            // if left arrow is pressed
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movement.RotateLeft();
            }
            // rotate left
        }
        #endregion
    }
}
