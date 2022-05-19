using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Common.Collections;

namespace Otakulore.Core;

public class IncrementalSource<T> : IIncrementalSource<T>
{

    private readonly Func<int, int, IEnumerable<T>>? _function;
    private readonly Func<int, int, Task<IEnumerable<T>>>? _asyncFunction;

    public IncrementalSource(Func<int, int, IEnumerable<T>> function)
    {
        _function = function;
    }

    public IncrementalSource(Func<int, int, Task<IEnumerable<T>>> function)
    {
        _asyncFunction = function;
    }

    public async Task<IEnumerable<T>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = new())
    {
        if (_function != null)
            return _function.Invoke(pageIndex, pageSize);
        if (_asyncFunction != null)
            return await _asyncFunction.Invoke(pageIndex, pageSize);
        return Array.Empty<T>();
    }

}