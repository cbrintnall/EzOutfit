using UnityEngine;
using Verse;

public class ExtrasWindow : Window
{
    public override Vector2 InitialSize => new Vector2(700f, 700f);

    public ExtrasWindow()
    {
        this.doCloseX = true;
        this.doCloseButton = true;
        this.forcePause = true;
        this.closeOnClickedOutside = true;
        this.absorbInputAroundWindow = true;
    }

    public override void DoWindowContents(Rect inRect) { }

    public override void PostClose()
    {
        base.PostClose();
    }
}
