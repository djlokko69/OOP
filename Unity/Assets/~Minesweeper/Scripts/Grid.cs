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
        #region SpawnTile
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
        #endregion
        #region GenerateTiles
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
        #endregion
        void Start()
        {
            GenerateTiles();
        }
        public int GetAdjacentMineCount(Tile tile)
        {
            // Set count to 0
            int count = 0;
            // loop through all the adjacent tiles on the x
            for (int x = -1; x <= 1; x++)
            {
                // loop through all the adjacent tiles on the y
                for (int y = -1; y <= 1; y++)
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
       
        void FlagTile(Tile selected)
        {
            selected.Flag();
        }

        #region Update
        void Update()
        {
            // Ray cast from the camera using the mouse Position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            /// did the raycast hit something?
            if (hit.collider != null)
            {
                // is the thing we hit a 'Tile'?
                Tile hitTile = hit.collider.GetComponent<Tile>();
                if (hitTile != null)
                {
                    // Check if Mouse Button is pressed
                    if (Input.GetMouseButtonDown(0))
                    {
                        // perform game logic with selected tiles
                        SelectTile(hitTile);
                    }
                    // Check if Mouse Button is pressed
                    if (Input.GetMouseButtonDown(1))
                    {

                        // perform game logic with selected tiles
                        FlagTile(hitTile);
                    }
                }
            }
        }
        #endregion
        #region FFuncover
        void FFuncover(int x, int y, bool[,] visited)
        {
            // is x and y within bounds of the grid?
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                // have these coordinates been visited?
                if (visited[x, y])
                    return;
                // reveal tile in that x and y coordinate
                Tile tile = tiles[x, y];
                int adjacentMiles = GetAdjacentMineCount(tile);
                tile.Reveal(adjacentMiles);

                //if there were no adjacent mines around that tile
                if (adjacentMiles == 0)
                {
                    // this tile has been visited
                    visited[x, y] = true;
                    // visit all other tiles around this tile
                    FFuncover(x - 1, y, visited);
                    FFuncover(x + 1, y, visited);
                    FFuncover(x, y - 1, visited);
                    FFuncover(x, y + 1, visited);
                }
            }
        }
        #endregion
        #region UncoverMines
        void UncoverMines(int mineState = 0)
        {
            // Loop through 2D array
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Tile tile = tiles[x, y];
                    // check if tile is a mine
                    if (tile.isMine)
                    {
                        int adjacentMines = GetAdjacentMineCount(tile);
                        tile.Reveal(adjacentMines, mineState);
                    }
                }
            }
        }
        #endregion
        #region NoMoreEmptyTiles
        bool NoMoreEmptyTiles()
        {
            // set empty tile count to zero
            int emptyTileCount = 0;
            // loop through 2D array
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Tile tile = tiles[x, y];
                    // if tile is not revealed and not a mine
                    if (!tile.isRevealed && !tile.isMine)
                    {
                        // we found an empty tile!
                        emptyTileCount += 1;
                    }
                }
            }
            // if there are empty tiles - return true
            // if there are no empty tiles - return false
            return emptyTileCount == 0;
        }
        #endregion
        #region SelectTile
        void SelectTile(Tile selected)
        {
            // If the selected tile is flagged
            if (selected.isFlagged)
            {
                // Exit the function
                return;
            }
            int adjacentMines = GetAdjacentMineCount(selected);
            selected.Reveal(adjacentMines);

            // is the selected tile a mine?
            if (selected.isMine)
            {
                // uncover all mines - with default loss state '0'
                UncoverMines();
                // lose
            }
            // otherwise, are there no mines around this tile?
            else if (adjacentMines == 0)
            {
                int x = selected.x;
                int y = selected.y;
                // then use flood fill to uncover all adjacent mines
                FFuncover(x, y, new bool[width, height]);
            }
            // are there no more empty tiles in the game at this point?
            if (NoMoreEmptyTiles())
            {
                // Uncover all mines - with the win state '1'
                UncoverMines(1);
            }
        }
        #endregion
    }
}
