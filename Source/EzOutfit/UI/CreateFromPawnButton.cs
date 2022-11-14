using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

public static class CreateFromPawn
{
  const float BUTTON_WIDTH = 150f;

  static AccessTools.FieldRef<Dialog_ManageOutfits, Outfit> selectedOutfitRef = AccessTools.FieldRefAccess<Dialog_ManageOutfits, Outfit>("selOutfitInt");

  public static void DoButton(Dialog_ManageOutfits window) => DoCreateButton(window);

  static void DoCreateButton(Dialog_ManageOutfits window)
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
          new FloatMenuOption(pawn.Name.ToStringShort, () => CreateOutfitFromPawn(window, pawn))
        );
      }

      Find.WindowStack.Add(new FloatMenu(options));
    }
  }

  static void CreateOutfitFromPawn(Dialog_ManageOutfits dialogue, Pawn pawn)
  {
    Outfit createdOutfit = Current.Game.outfitDatabase.MakeNewOutfit();

    createdOutfit.label = "OutfitName".Translate(pawn.Name.ToStringShort);
    createdOutfit.filter.SetDisallowAll();
    createdOutfit.filter.AllowedHitPointsPercents = pawn.outfits.CurrentOutfit.filter.AllowedHitPointsPercents;
    createdOutfit.filter.AllowedQualityLevels = pawn.outfits.CurrentOutfit.filter.AllowedQualityLevels;

    switch (ModSettings.TaintedDefault.Value)
    {
      case TaintedOptions.YES:
        createdOutfit.filter.SetAllow(SpecialThingFilterDefOf.AllowDeadmansApparel, true);
        break;
      case TaintedOptions.NO:
        createdOutfit.filter.SetAllow(SpecialThingFilterDefOf.AllowDeadmansApparel, false);
        break;
      case TaintedOptions.USE_PAWN:
        createdOutfit.filter.SetAllow(SpecialThingFilterDefOf.AllowDeadmansApparel, pawn.apparel.WornApparel.Any(apparel => apparel.WornByCorpse));
        break;
    }

    foreach (Apparel apparel in pawn.apparel.WornApparel)
    {
      createdOutfit.filter.SetAllow(apparel.def, true);
    }

    selectedOutfitRef(dialogue) = createdOutfit;
  }
}