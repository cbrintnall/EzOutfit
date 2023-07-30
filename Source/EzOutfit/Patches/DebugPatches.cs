using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

[HarmonyPatch(typeof(DebugToolsPawns), nameof(DebugToolsPawns.PawnGearDevOptions))]
public static class DebugPatch_Gear
{
  public static void Postfix(Pawn pawn, ref List<FloatMenuOption> __result)
  {
    __result.Add(
      new FloatMenuOption(
        "Taint random apparel",
        () =>
        {
          var fieldRef = AccessTools.FieldRefAccess<Apparel, bool>("wornByCorpseInt");
          Apparel chosenApparel = pawn.apparel.WornApparel.Where(apparel => !apparel.WornByCorpse).RandomElement();
          fieldRef(chosenApparel) = true;
        }
      )
    );

    __result.Add(
      new FloatMenuOption(
        "Damage random to worn",
        () =>
        {
          pawn.apparel.WornApparel.RandomElement().TakeDamage(
            new DamageInfo(DamageDefOf.Deterioration, 50f)
          );
        }
      )
    );
  }
}