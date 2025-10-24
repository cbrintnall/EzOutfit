using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

// [HarmonyPatch(typeof(PawnColumnWorker_Outfit), "DoHeader")]
// TODO: Add this functionality in post 1.6 update
#if (V1_5 || V1_6)
public class PawnColumnWorker_Outfit_DoDropAllTainted
{
    static float BUTTON_OFFSET = 8.0f;
    static Rect BUTTON_POSITION = new Rect(30f, 5f, 20f, 20f);
    static Color BUTTON_BASE_COLOR = Color.white;
    static Color BUTTON_DISABLED_COLOR = new Color(1.0f, 1.0f, 1.0f, 0.25f);

    static bool Prefix(Rect rect, PawnTable table)
    {
        bool pawnUpset = PawnApparel.AnyPawnIsUpset();
        BUTTON_POSITION.x = rect.x - BUTTON_POSITION.size.x - BUTTON_OFFSET;

        if (
            Widgets.ButtonImage(
                BUTTON_POSITION,
                TexButton.Drop,
                pawnUpset ? BUTTON_BASE_COLOR : BUTTON_DISABLED_COLOR,
                pawnUpset,
                "DropTainted".Translate()
            ) && pawnUpset
        )
        {
            PawnApparel.DropAllTainted();
        }

        return true;
    }
}
#endif
