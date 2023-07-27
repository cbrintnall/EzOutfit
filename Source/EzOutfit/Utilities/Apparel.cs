﻿using RimWorld;
using UnityEngine;

public static class ApparelUtilities
{
  public static float NormalizedHitpoints(this Apparel apparel)
  {
    return (float)apparel.HitPoints / (float)apparel.MaxHitPoints;
  }

  public static bool IsUpsetting(this Apparel apparel) => apparel.WornByCorpse || apparel.NormalizedHitpoints() <= Mathf.Max(ThoughtWorker_ApparelDamaged.MinForFrayed, ThoughtWorker_ApparelDamaged.MinForTattered);
}