using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper2D
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Tile : MonoBehaviour
    {
        // Store x and y coordiante in array for later use
        public int x = 0;
        public int y = 0;
        public bool isMine = false; // is current tile a mine?
        public bool isRevealed = false; // Has the tile already been revealed/
        [Header("References")]
        public Sprite[] emptySprites; // List of empty sprites i.e, empty, 1,2,3,4, etc...
        public Sprite[] mineSprites; // The mine sprites

        private SpriteRenderer rend; // Reference to sprite renderer

        
        void Awake()
        {
            // Grab reference to sprite renderer
            rend = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            // Detach text element from the block
            /*textElement.transform.SetParent(null);*/
            // Randomly decides if it's a mine or not
            isMine = Random.value < .05f;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Reveal(int adjacentMines, int mineState = 0)
        {
            // Flags the tile as beingt revealed
            isRevealed = true;
            // Check if tile is a mine
            if (isMine)
            {
                // Sets sprite to mine sprite
                rend.sprite = mineSprites[mineState];
            }
            else
            {
                // Set sprite to appropriate texture based on adjacent mines
                rend.sprite = emptySprites[adjacentMines];
            }
        }
    }
}