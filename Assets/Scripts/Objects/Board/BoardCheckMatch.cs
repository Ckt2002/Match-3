using System.Collections.Generic;
using UnityEngine;

public class BoardCheckMatch
{
    GameObject[,] potionGrid;
    int width, height;

    public BoardCheckMatch()
    {
    }

    public void Setup(BoardGrid boardGrid)
    {
        potionGrid = boardGrid.potionGrid;
        width = boardGrid.gridWidth;
        height = boardGrid.gridHeight;
    }

    public List<PotionController> FindHorizontalMatches(int w, int h)
    {
        List<PotionController> matches = new List<PotionController>();
        matches.Add(potionGrid[w, h].GetComponent<PotionController>());

        // Left
        for (int i = w - 1; i >= 0; i--)
        {
            if (potionGrid[i, h].GetComponent<PotionController>().potionType ==
                potionGrid[w, h].GetComponent<PotionController>().potionType)
                matches.Add(potionGrid[i, h].GetComponent<PotionController>());
            else
                break;
        }

        // Right
        for (int i = w + 1; i < width; i++)
        {
            if (potionGrid[i, h].GetComponent<PotionController>().potionType ==
                potionGrid[w, h].GetComponent<PotionController>().potionType)
                matches.Add(potionGrid[i, h].GetComponent<PotionController>());
            else
                break;
        }

        return matches.Count >= 3 ? matches : new List<PotionController>();
    }

    public List<PotionController> FindVerticalMatches(int w, int h)
    {
        List<PotionController> matches = new List<PotionController>();
        matches.Add(potionGrid[w, h].GetComponent<PotionController>());

        // Down
        for (int i = h - 1; i >= 0; i--)
        {
            if (potionGrid[w, i].GetComponent<PotionController>().potionType ==
                potionGrid[w, h].GetComponent<PotionController>().potionType)
                matches.Add(potionGrid[w, i].GetComponent<PotionController>());
            else
                break;
        }

        // Up
        for (int i = h + 1; i < height; i++)
        {
            if (potionGrid[w, i] == null)
                Debug.Log($"Catch null {i}");

            if (potionGrid[w, i].GetComponent<PotionController>().potionType ==
                potionGrid[w, h].GetComponent<PotionController>().potionType)
                matches.Add(potionGrid[w, i].GetComponent<PotionController>());
            else
                break;
        }

        return matches.Count >= 3 ? matches : new List<PotionController>();
    }
}
