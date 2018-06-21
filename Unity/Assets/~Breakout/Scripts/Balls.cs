using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breakout
{
    public class Balls : MonoBehaviour
    {
        public float speed = 5f;// Speed that ball travels

        private Vector3 velocity;// Velocity of the ball (direction X Speed)

        // Fires off Ballz in a given direction 
        public void Fire(Vector3 direction)
        {
            // Calculate Velocity
            velocity = direction * speed;
        }

        // Detect Collisions 
        void OnCollisionEnter2D(Collision2D other)
        {
            // Grab contact point of Collision
            ContactPoint2D contact = other.contacts[0];
            // Calculate the reflection point of the balls using velocity &contact normal
            Vector3 reflect = Vector3.Reflect(velocity, contact.normal);
            // Calculate new velocity from reflection multiply by the same speed (velocity.magnitude)
            velocity = reflect.normalized * velocity.magnitude;

            
        }
        private void Update()
        {
            // Moves ball using velocity & deltaTime
            transform.position += velocity * Time.deltaTime;
        }
    }
}
