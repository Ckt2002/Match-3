using UnityEngine;

class TilePooling : MonoBehaviour, ITilePooling
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform tileContainer;

    public TileController[,] Create(int width, int height)
    {
        TileController[,] tiles = new TileController[width, height];

        for (int w = 0; w < width; w++)
            for (int h = 0; h < height; h++)
                tiles[w, h] = Instantiate(tilePrefab, tileContainer)
                    .GetComponent<TileController>();

        return tiles;
    }
}