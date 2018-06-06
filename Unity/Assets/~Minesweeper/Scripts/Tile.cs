using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Tile : MonoBehaviour
    {
        #region Variables
        public int x, y;
        public bool isMine = false; // Is the current tile a mine?
        public bool isRevealed = false; // Has the tile already been revealed?
        public bool isFlagged = false;
        [Header("Reference")]
        public Sprite[] emptySpirtes; // List of empty sprites i.e, empty, 1, 2, 3, etc...
        public Sprite[] mineSprites; // The mine sprites
        public Sprite[] flagSprites;
        private SpriteRenderer rend; // Reference to sprite renderer
        #endregion

        void Awake()
        {
            // Grab reference to sprite renderer
            rend = GetComponent<SpriteRenderer>();
        }
        void Start()
        {
            // Randomly decide if this tile is a mine - using a 5% chance
            // each Tile has a 5% chance of being a mine
            isMine = Random.value < .05f;
        }
        public void Reveal(int adjacentMines, int mineState = 0)
        {
            // Flags the tile as being revealed
            isRevealed = true;
            // Checks if tile is a mine
            if (isMine)
            {
                // Sets sprite to mine sprite
                rend.sprite = mineSprites[mineState];
            }
            else
            {
                // Sets sprite to appropriate texture based on adjacent mines
                rend.sprite = emptySpirtes[adjacentMines];
            }
        }
        public void Flag()
        {
            // If the tile has already been revealed
            if(isRevealed)
            {
                // Exit the function - You can't flag a tile that is revealed
                return;
            }
            // Toggles true and false everytime this is called
            isFlagged = !isFlagged;
            if (isFlagged)
            {
                // 0 = flag sprite
                rend.sprite = flagSprites[1];
            }
            else
            {
                //  1 = original sprite
                rend.sprite = flagSprites[0];
            }
        }
    }
}
