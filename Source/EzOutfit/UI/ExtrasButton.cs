using RimWorld;
using UnityEngine;
using Verse;

public static class ExtrasButton
{
  const float BUTTON_WIDTH = 60f;
  const float BASE_BUTTON_WIDTH = 150f;

  public static bool DoButton(Dialog_ManageOutfits window)
  {
    Rect createRect = new Rect((BASE_BUTTON_WIDTH * 3) + 120f, 40f, BUTTON_WIDTH, 35f);
    TextAnchor? overrideTextAnchor3 = new TextAnchor?();

    return Widgets.ButtonText(createRect, "...", overrideTextAnchor: overrideTextAnchor3);
  }
}