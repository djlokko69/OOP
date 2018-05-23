using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper
{
    public class Grid : MonoBehaviour
    {
        #region Variables
        public GameObject tilePrefab;
        public int width = 10, height = 10;
        public float spacing = .155f;

        private Tile[,] tiles;
        #endregion

        // Functionality for spawning tiles
        Tile SpawnTile(Vector3 pos)
        {
            // Clone tile Prefab
            GameObject clone = Instantiate(tilePrefab);
            // Edit it's properties
            clone.transform.position = pos;
            Tile currentTile = clone.GetComponent<Tile>();
            // Return it
            return currentTile;
        }
        // Spawns tiles in a grid like pattern
        void GenerateTiles()
        {
            // Create a new 2D array of size width x height
            tiles = new Tile[width, height];
            // Loop through the entire tile list
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Note: Part 2 goes here
                    // Store halfSize for later use
                    Vector2 halfSize = new Vector2(width * 0.5f, height * 0.5f);
                    // Pivot tiles around grid
                    Vector2 pos = new Vector2(x - halfSize.x, y - halfSize.y);
                    // Offset
                    Vector2 offset = new Vector2(.5f, .5f);
                    pos += offset;

                    // Apply spacing
                    pos *= spacing;
                    // Spawn the tile using spawn function made earlier
                    Tile tile = SpawnTile(pos);
                    // Attach newly spawned tile to self (transform)
                    tile.transform.SetParent(transform);
                    // Store it's array coordinates within itself for future reference
                    tile.x = x;
                    tile.y = y;
                    // Store tile in array at those coordinates
                    tiles[x, y] = tile;
                }
            }
        }
        void Start()
        {
            GenerateTiles();
        }
        public int GetAdjacentMineCount(Tile tile)
        {
            // Set count to 0
            int count = 0;
            // loop through all the adjacent tiles on the x
            for (int x = 0; x <= 1; x++)
            {
                // loop through all the adjacent tiles on the y
                for (int y = 0; y <= 1; y++)
                {
                    // Calculate which adjacent tile to look at
                    int desiredX = tile.x + x;
                    int desiredY = tile.y + y;
                    // Calculate if the desired x & y is within bounds
                    if (desiredX < 0 || desiredX >= width || desiredY < 0 || desiredY >= height)
                    {
                        continue;
                    }
                    // Select current tile
                    Tile currentTile = tiles[desiredX, desiredY];
                    // Check if that is a mine
                    if (currentTile.isMine)
                    {
                        // Increment count by 1
                        count++;
                    }
                }
            }
            // Remember to return the count
            return count;
        }
        void SelectATile()
        {
            // Generate a ray from camera with mouse position
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Perform raycast
            RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);
            // If the mouse hit something
            if (hit.collider != null)
            {
                // Try getting a Tile component from the thing we hit
                Tile hitTile = hit.collider.GetComponent<Tile>();
                // Check if the thing it hit was a tile
                if (hitTile != null)
                {
                    // Get a count of all mines around the hit tile
                    int adjacentMines = GetAdjacentMineCount(hitTile);
                    // Reveal what that hit tile is
                    hitTile.Reveal(adjacentMines);
                }
            }
        }
        void Update()
        {
            // Check if Mouse Button is pressed
            if (Input.GetMouseButtonDown(0))
            {
                // Run the method for selecting tiles
                SelectATile();
            }

        }
    }
}
