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
        bool hasTainted = Find.ColonistBar.Entries
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
  }
}
#endif
