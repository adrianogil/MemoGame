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

        // Can't activate twice for same element in a row
        if (frontFacingItems.Contains(memoItem))
        {
            return;
        }

        if (facing == MemoFace.Front)
        {
            frontFacingItems.Add(memoItem);
        }

        if (frontFacingItems.Count >= 2)
        {
            if (frontFacingItems[0].Matches(frontFacingItems[1]))
            {
                frontFacingItems.Remove(frontFacingItems[1]);
                frontFacingItems.Remove(frontFacingItems[0]);
            } else
            {
                for (int i = frontFacingItems.Count-1; i >= 0; i--)
                {
                    frontFacingItems[i].SwapFacingDelayed(1f);
                    frontFacingItems.Remove(frontFacingItems[i]);
                }
            }


        }
    }

}