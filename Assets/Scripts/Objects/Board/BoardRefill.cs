using UnityEngine;

public class BoardRefill
{
    BoardGrid boardGrid;
    GameObject[,] potionGrid;
    GameObject[,] tileGrid;
    int width, height;

    public BoardRefill(BoardGrid boardGrid)
    {
        this.boardGrid = boardGrid;
    }

    public void InitRefill()
    {
        this.potionGrid = boardGrid.potionGrid;
        this.tileGrid = boardGrid.tileGrid;
        this.width = boardGrid.gridWidth;
        this.height = boardGrid.gridHeight;
    }

    public void MoveDown()
    {
        if (potionGrid == null || tileGrid == null)
            InitRefill();

        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                if (potionGrid[w, h] == null)
                {
                    for (int ny = h + 1; ny < height; ny++)
                    {
                        if (potionGrid[w, ny] != null)
                        {
                            potionGrid[w, h] = potionGrid[w, ny];
                            //Debug.Log($"{potionGrid[w, h]}: [{w},{h}]");
                            boardGrid.PrintValue(w, h);
                            potionGrid[w, ny] = null;

                            potionGrid[w, h].GetComponent<PotionController>()
                                .SetPotionIndex(new Vector2Int(w, h));

                            Vector3 targetPos = tileGrid[w, h].transform.position;
                            potionGrid[w, h].GetComponent<PotionController>()
                                .Move(targetPos);

                            break;
                        }
                    }
                }
            }
        }
    }
}