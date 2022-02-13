﻿namespace Otakulore.Core.AniList;

public class AniPaginationOptions
{

    public int Index { get; }
    public int Size { get; }

    public AniPaginationOptions(int index = 1, int size = 50)
    {
        Index = index;
        Size = size;
    }

}