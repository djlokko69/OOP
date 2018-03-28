using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace Asteroids
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance = null; // static is global

        public Text scoreText;

        private int score = 0;

        // Use this for initialization
        void Start()
        {
            // if an instance of GameManager hasn't been created
            if (Instance == null)
            {
               // set to first instance of GameManager
                Instance = this;
            }
            else
            {
                // destroy any other instance of GameManager
                Destroy(gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {
            scoreText.text = "Score: " + score.ToString();
        }

        public void AddScore(int scoreToAdd)
        {
            score += scoreToAdd;
        }
    }
}
