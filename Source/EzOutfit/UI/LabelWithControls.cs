using HarmonyLib;
using RimWorld;
using System;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

#if V1_4
public class PawnColumnWorker_LabelWithControls : PawnColumnWorker_Label
{
  const float RectDim = 24f;

  public override void DoHeader(Rect rect, PawnTable table)
  {
    Rect dropRect = new Rect(rect.x, rect.y, RectDim, RectDim);
    bool hasTainted = Find.ColonistBar
      .Entries
      .Select(entry => entry.pawn)
      .SelectMany(pawn => pawn.apparel.WornApparel)
      .Any(apparel => apparel.IsUpsetting());

    if (Mouse.IsOver(dropRect))
    {
      string taintedTooltipText = "DropTainted".Translate();

      if (!hasTainted)
      {
        taintedTooltipText += $" ({"NoTainted".Translate()})";
      }

      TooltipHandler.TipRegion(dropRect, taintedTooltipText);
    }

    Color buttonColor = hasTainted ? Color.white : Color.gray;
    Color hoverColor = hasTainted ? GenUI.MouseoverColor : buttonColor;

    if (Widgets.ButtonImage(dropRect, TexButton.Drop, buttonColor, hoverColor, hasTainted))
    {
      DropAllTainted();
    }

    Rect headerRect = new Rect(rect.x, rect.y, rect.width - RectDim, rect.height);

    base.DoHeader(headerRect, table);
  }

  private void DropAllTainted()
  {
    var pawns = Find.ColonistBar
      .Entries
      .Select(entry => entry.pawn)
      .ToArray();

    var dropQueue = pawns
      .Select(pawn => Tuple.Create(pawn, pawn.apparel.WornApparel.Where(apparel => apparel.IsUpsetting())));

    foreach(var pairing in dropQueue)
    {
      var pawn = pairing.Item1;
      var items = pairing.Item2.ToList();

      foreach(var apparel in items)
      {
        if (pairing.Item1.apparel.TryDrop(apparel))
        {
          Log.Message($"Pawn {pawn.Name.ToStringShort} successfully dropped {apparel.Label}");
        }
        else
        {
          Log.Error($"Pawn {pawn.Name.ToStringShort} failed to drop {apparel.Label}");
        }
      }
    }
  }
}
#endif