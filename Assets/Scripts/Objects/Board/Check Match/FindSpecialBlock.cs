using System.Collections.Generic;
using System.Linq;

public class FindSpecialBlock
{

    public static Dictionary<TileController, ESpecialType> FindSpecialBlocks(HashSet<TileController> matches,
        BoardGrid boardGrid)
    {
        var specials = new Dictionary<TileController, ESpecialType>();

        // Group matches by row and column
        var rowMatches = matches.GroupBy(t => new { t.tileIndex.x, t.potion.potionType });
        var colMatches = matches.GroupBy(t => new { t.tileIndex.y, t.potion.potionType });

        List<TileController> lst = new List<TileController>();
        #region Check Bomb
        foreach (var row in rowMatches.Where(g => g.Count() >= 3))
        {
            foreach (var col in colMatches.Where(g => g.Count() >= 3))
            {
                var intersection = row.Intersect(col);
                if (intersection.Any() && !row.Any(lst.Contains) && !col.Any(lst.Contains))
                {
                    lst.AddRange(row);
                    lst.AddRange(col);

                    TileController tile = intersection.First();
                    specials[tile] = ESpecialType.Explosion;
                }
            }
        }
        #endregion

        #region Check Lightning
        foreach (var row in rowMatches.Where(g => g.Count() >= 5))
        {
            bool isLineOnly = !colMatches.Any(col =>
                col.Count() >= 3 && col.Intersect(row).Any());

            if (isLineOnly && !row.Any(lst.Contains))
            {
                lst.AddRange(row);

                var middleTile = row.OrderBy(t => t.tileIndex.x).ElementAt(row.Count() / 2);
                if (row.Contains(boardGrid.selectedTile))
                    middleTile = boardGrid.selectedTile;
                else if (row.Contains(boardGrid.swappedTile))
                    middleTile = boardGrid.swappedTile;
                specials[middleTile] = ESpecialType.Lightning;
            }
        }

        foreach (var col in colMatches.Where(g => g.Count() >= 5))
        {
            bool isLineOnly = !rowMatches.Any(row =>
                row.Count() >= 3 && row.Intersect(col).Any());

            if (isLineOnly && !col.Any(lst.Contains))
            {
                lst.AddRange(col);

                var middleTile = col.OrderBy(t => t.tileIndex.y).ElementAt(col.Count() / 2);
                if (col.Contains(boardGrid.selectedTile))
                    middleTile = boardGrid.selectedTile;
                else if (col.Contains(boardGrid.swappedTile))
                    middleTile = boardGrid.swappedTile;
                specials[middleTile] = ESpecialType.Lightning;
            }
        }
        #endregion

        #region Check swipe
        foreach (var row in rowMatches.Where(g => g.Count() == 4))
        {
            bool isLineOnly = !colMatches.Any(col =>
                col.Count() >= 3 && col.Intersect(row).Any());

            if (isLineOnly && !row.Any(lst.Contains))
            {
                lst.AddRange(row);

                var middleTile = row.OrderBy(t => t.tileIndex.x).ElementAt(row.Count() / 2);
                if (row.Contains(boardGrid.selectedTile))
                    middleTile = boardGrid.selectedTile;
                else if (row.Contains(boardGrid.swappedTile))
                    middleTile = boardGrid.swappedTile;

                int potionIndex = UnityEngine.Random.Range(1, 3);
                specials[middleTile] = (ESpecialType)potionIndex;
            }
        }

        foreach (var col in colMatches.Where(g => g.Count() == 4))
        {
            bool isLineOnly = !rowMatches.Any(row =>
                row.Count() >= 3 && row.Intersect(col).Any());

            if (isLineOnly && !col.Any(lst.Contains))
            {
                lst.AddRange(col);

                var middleTile = col.OrderBy(t => t.tileIndex.y).ElementAt(col.Count() / 2);
                if (col.Contains(boardGrid.selectedTile))
                    middleTile = boardGrid.selectedTile;
                else if (col.Contains(boardGrid.swappedTile))
                    middleTile = boardGrid.swappedTile;

                int potionIndex = UnityEngine.Random.Range(1, 3);
                specials[middleTile] = (ESpecialType)potionIndex;
            }
        }
        #endregion

        return specials;
    }
}
