using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breakout
{
    public class DestroyBricks : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        void OnCollisionEnter2d(Collision2D other)
        {
            if (other.gameObject.CompareTag("Brick"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
