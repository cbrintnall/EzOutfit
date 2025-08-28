using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

[HarmonyPatch(typeof(Dialog_ManageApparelPolicies), "CreateNewPolicy")]
class Dialog_ManageApparelPolicies_OverrideNew
{
    static bool Prefix(Dialog_ManageApparelPolicies __instance)
    {
        var options = new List<FloatMenuOption>()
        {
            new FloatMenuOption(
                "NewBlankEntry".Translate(),
                () => Current.Game.outfitDatabase.MakeNewOutfit()
            ),
            new FloatMenuOption(
                "CopyFromPawn".Translate(),
                () => PawnApparel.CreateCopyApparelOptions(CreateOutfitFromPawn)
            )
        };

        Find.WindowStack.Add(new FloatMenu(options));

        return false;
    }

    static void CreateOutfitFromPawn(Pawn pawn)
    {
        ApparelPolicy createdOutfit = Current.Game.outfitDatabase.MakeNewOutfit();

        createdOutfit.label = "OutfitName".Translate(pawn.Name.ToStringShort);
        createdOutfit.filter.SetDisallowAll();
        createdOutfit.filter.AllowedHitPointsPercents = pawn.outfits
            .CurrentApparelPolicy
            .filter
            .AllowedHitPointsPercents;
        createdOutfit.filter.AllowedQualityLevels = pawn.outfits
            .CurrentApparelPolicy
            .filter
            .AllowedQualityLevels;

        switch (ModSettings.TaintedDefault.Value)
        {
            case TaintedOptions.YES:
                createdOutfit.filter.SetAllow(SpecialThingFilterDefOf.AllowDeadmansApparel, true);
                break;
            case TaintedOptions.NO:
                createdOutfit.filter.SetAllow(SpecialThingFilterDefOf.AllowDeadmansApparel, false);
                break;
            case TaintedOptions.USE_PAWN:
                createdOutfit.filter.SetAllow(
                    SpecialThingFilterDefOf.AllowDeadmansApparel,
                    pawn.apparel.WornApparel.Any(apparel => apparel.WornByCorpse)
                );
                break;
        }

        foreach (Apparel apparel in pawn.apparel.WornApparel)
        {
            createdOutfit.filter.SetAllow(apparel.def, true);
        }
    }
}
