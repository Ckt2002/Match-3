using System.Collections.Generic;
using System.Linq;

public class FindSpecialBlock
{
    public static Dictionary<TileController, ESpecialType> FindSpecialBlocks(HashSet<TileController> matches)
    {
        var specials = new Dictionary<TileController, ESpecialType>();

        // Group matches by row and column
        var rowMatches = matches.GroupBy(t => new { t.tileIndex.x, t.potion.potionType });
        var colMatches = matches.GroupBy(t => new { t.tileIndex.y, t.potion.potionType });

        foreach (var row in rowMatches.Where(g => g.Count() >= 3))
        {
            foreach (var col in colMatches.Where(g => g.Count() >= 3))
            {
                var intersection = row.Intersect(col);
                if (intersection.Any())
                {
                    TileController tile = intersection.First();
                    tile.GetSpecialType(ESpecialType.Explosion);
                    specials[tile] = ESpecialType.Explosion;
                }
            }
        }

        foreach (var row in rowMatches.Where(g => g.Count() >= 4))
        {
            bool isLineOnly = !colMatches.Any(col =>
                col.Count() >= 3 && col.Intersect(row).Any());

            if (isLineOnly)
            {
                var middleTile = row.OrderBy(t => t.tileIndex.x).ElementAt(row.Count() / 2);
                middleTile.GetSpecialType(ESpecialType.H);
                specials[middleTile] = ESpecialType.H;
            }
        }

        foreach (var col in colMatches.Where(g => g.Count() >= 4))
        {
            bool isLineOnly = !rowMatches.Any(row =>
                row.Count() >= 3 && row.Intersect(col).Any());

            if (isLineOnly)
            {
                var middleTile = col.OrderBy(t => t.tileIndex.y).ElementAt(col.Count() / 2);
                middleTile.GetSpecialType(ESpecialType.V);
                specials[middleTile] = ESpecialType.V;
            }
        }

        return specials;
    }
}
