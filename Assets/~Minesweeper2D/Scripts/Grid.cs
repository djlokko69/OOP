using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper2D
{
    public class Grid : MonoBehaviour
    {
        public GameObject tilePrefab;
        public int width = 10;
        public int height = 10;
        public float spacing = .155f;

        private Tile[,] tiles;

        // Use this for initialization
        void Start()
        {
            // Generate tiles on startup
            GenerateTiles();
        }

        // Update is called once per frame
        void Update()
        {
            // Let mouseRay = Camera ScreenPointToRay mousePosition
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Let hit = Physics2D Raycast from mouse ray
            RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);
            // If hit collider != null
            if (hit.collider != null)
            {
                // Let hitTile = hit collider's Tile component
                Tile hitTile = hit.collider.GetComponent<Tile>();
                // If hitTile != null
                if (hitTile != null)
                {
                    // LET adjacentMines = GetAdjacentMineCountAt hitTile
                    int adjacentMines = GetAdjacentMineCountAt(hitTile);
                    // CALL hitTale.Reveal(adjacentMines)
                    hitTile.Reveal(adjacentMines);
                }
        }
        }

        // Functionality for spawning tiles
        Tile SpawnTile(Vector3 pos)
        {
            // Clone tile prefab
            GameObject clone = Instantiate(tilePrefab);
            clone.transform.position = pos;// position tile
            Tile currentTile = clone.GetComponent<Tile>();// Get Tile component
            return currentTile;// return it
        }

        // Spawns tiles in a grid-like pattern
        void GenerateTiles()
        {
            // Create new 2D array of size width x height
            tiles = new Tile[width, height];

            // Loop through the entire tile list
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // >> Part 2 goes here <<
                    // Store half size for later use
                    Vector2 halfsize = new Vector2(width / 2, height / 2);
                    // Pivot tiles around Grid
                    Vector2 pos = new Vector2(x - halfsize.x,
                                              y - halfsize.y);
                    // Apply spacing
                    pos *= spacing;

                    // Spawn the tile
                    Tile tile = SpawnTile(pos);
                    // Attach newly spawned tile to
                    tile.transform.SetParent(transform);
                    // Store it's array coordinates within itself for future reference
                    tile.x = x;
                    tile.y = y;
                    // Store tile in array at those coordinates
                    tiles[x, y] = tile;
                }
            }
        }

        // Count adjacent mines at element
        public int GetAdjacentMineCountAt(Tile t)
        {
            int count = 0;
            // Loop through all elements and each axis go between -1 to 1
            for (int x = -1; x < 1; x++)
            {
                for (int y = -1; y < 1; y++)
                {
                    // Calculate desired coordinates from ones attained
                    int desiredX = t.x + x;
                    int desiredY = t.y + y;
                    // Select current tile
                    Tile currentTile = tiles[desiredX, desiredY];
                    // Check if that tile is a mine
                    if (currentTile.isMine)
                    {
                        // Increment count by 1
                        count++;
                    } 
                }
            }
            // Remember to return the count!!
            return count;
        }


    }
}
