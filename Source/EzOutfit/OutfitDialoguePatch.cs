using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using System.Linq;
using System.Collections.Generic;

[HarmonyPatch(typeof(Dialog_ManageOutfits))]
[HarmonyPatch(nameof(Dialog_ManageOutfits.DoWindowContents))]
static class Dialog_ManageOutfits_OutfitDialogue_Patch 
{
  const float BUTTON_WIDTH = 150f;

  static void Postfix()
  {
    Rect createRect = new Rect((BUTTON_WIDTH * 3) + 30f, 0f, BUTTON_WIDTH, 35f);
    TextAnchor? overrideTextAnchor3 = new TextAnchor?();

    var pawns = Find.ColonistBar
      .Entries
      .Select(entry => entry.pawn)
      .ToArray();

    if (Widgets.ButtonText(createRect, "Create".Translate(), overrideTextAnchor: overrideTextAnchor3))
    {
      List<FloatMenuOption> options = new List<FloatMenuOption>();

      foreach (Pawn pawn in pawns)
      {
        options.Add(
          new FloatMenuOption(pawn.Name.ToStringShort, () => CreateOutfitFromPawn(pawn))
        );
      }

      Find.WindowStack.Add(new FloatMenu(options));
    }
  }

  static void CreateOutfitFromPawn(Pawn pawn)
  {
    Outfit createdOutfit = Current.Game.outfitDatabase.MakeNewOutfit();

    createdOutfit.label = "OutfitName".Translate(pawn.Name.ToStringShort);
    createdOutfit.filter.SetDisallowAll();

    foreach(Apparel apparel in pawn.apparel.WornApparel)
    {
      createdOutfit.filter.SetAllow(apparel.def, true);
    }
  }
}