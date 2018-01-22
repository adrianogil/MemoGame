using System.Collections.Generic;

public class MemoItemLogic : IMemoItemActivate
{
    public const int MAX_ITEMS = 9;

    private List<ProceduralMemoItem> frontFacingItems = null;

    public void Activate(ProceduralMemoItem memoItem, MemoFace facing)
    {
        if (frontFacingItems == null)
        {
            frontFacingItems = new List<ProceduralMemoItem>();
        }

        if (facing == MemoFace.Front)
        {
            frontFacingItems.Add(memoItem);
        }

        if (frontFacingItems.Count >= 2)
        {
            for (int i = frontFacingItems.Count-1; i >= 0; i--)
            {
                frontFacingItems[i].SwapFacingDelayed(1f);
                frontFacingItems.Remove(frontFacingItems[i]);
            }
        }
    }

}