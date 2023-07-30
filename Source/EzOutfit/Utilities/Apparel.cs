using RimWorld;
using UnityEngine;

public static class ApparelUtilities
{
  public static float NormalizedHitpoints(this Apparel apparel)
  {
    return (float)apparel.HitPoints / (float)apparel.MaxHitPoints;
  }

  public static bool IsUpsetting(this Apparel apparel) => 
      (ModSettings.DropAllIncludesTainted.Value && apparel.WornByCorpse) || 
      (ModSettings.DropAllIncludesTattered.Value && apparel.NormalizedHitpoints() <= Mathf.Max(ThoughtWorker_ApparelDamaged.MinForFrayed, ThoughtWorker_ApparelDamaged.MinForTattered));
}