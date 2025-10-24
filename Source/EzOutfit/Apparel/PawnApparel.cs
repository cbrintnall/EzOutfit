using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

public static class PawnApparel
{
    public static void CreateCopyApparelOptions(Action<Pawn> onCreate)
    {
        List<FloatMenuOption> options = new List<FloatMenuOption>();

        var pawns = Find.ColonistBar.Entries.Select(entry => entry.pawn).ToArray();

        foreach (Pawn pawn in pawns)
        {
            options.Add(new FloatMenuOption(pawn.Name.ToStringShort, () => onCreate(pawn)));
        }

        Find.WindowStack.Add(new FloatMenu(options));
    }

    public static void DropAllTainted()
    {
        var pawns = Find.ColonistBar.Entries.Select(entry => entry.pawn).ToArray();

        var dropQueue = pawns.Select(
            pawn =>
                Tuple.Create(pawn, pawn.apparel.WornApparel.Where(apparel => apparel.IsUpsetting()))
        );

        foreach (var pairing in dropQueue)
        {
            var pawn = pairing.Item1;
            var items = pairing.Item2.ToList();

            foreach (var apparel in items)
            {
                if (pairing.Item1.apparel.TryDrop(apparel))
                {
                    Log.Message(
                        $"Pawn {pawn.Name.ToStringShort} successfully dropped {apparel.Label}"
                    );
                }
                else
                {
                    Log.Error($"Pawn {pawn.Name.ToStringShort} failed to drop {apparel.Label}");
                }
            }
        }
    }

    public static bool AnyPawnIsUpset() =>
        Find.ColonistBar.Entries
            .Select(entry => entry.pawn)
            .Any(
                pawn =>
                    pawn.apparel.WornApparel
                        .Where(apparel => apparel.IsUpsetting())
                        .FirstOrDefault() != null
            );
}
