using SharpDX.Direct2D1;
using System.Collections;
using System.Collections.Generic;

namespace Minesharp.World;

/// <summary>
/// A Strip is a subdivision of a <seealso cref="Chunk"/> that contains
/// 256 <seealso cref="Block"/>s
/// </summary>
public readonly struct Strip : IList<Block>
{
    public int Index { get; }

    public int Count => ((ICollection<Block>)_blocks).Count;

    public bool IsReadOnly => ((ICollection<Block>)_blocks).IsReadOnly;

    public Block this[int index] { get => ((IList<Block>)_blocks)[index]; set => ((IList<Block>)_blocks)[index] = value; }

    private readonly List<Block> _blocks = new();

    public Strip(List<Strip> stripsContainer, int capacity)
    {
        Index = stripsContainer.Count;
        _blocks.Capacity = capacity;
    }

    public void Update()
    {
        foreach (Block block in _blocks)
        {
            block.Update();
        }
    }

    public void Draw()
    {
        foreach (Block block in _blocks)
        {
            block.Draw();
        }
    }

    public void Add(Block block)
    {
        _blocks.Add(block);
    }
    public void Remove(Block block)
    {
        _blocks.Remove(block);
    }

    public int IndexOf(Block item)
    {
        return ((IList<Block>)_blocks).IndexOf(item);
    }

    public void Insert(int index, Block item)
    {
        ((IList<Block>)_blocks).Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        ((IList<Block>)_blocks).RemoveAt(index);
    }

    public void Clear()
    {
        ((ICollection<Block>)_blocks).Clear();
    }

    public bool Contains(Block item)
    {
        return ((ICollection<Block>)_blocks).Contains(item);
    }

    public void CopyTo(Block[] array, int arrayIndex)
    {
        ((ICollection<Block>)_blocks).CopyTo(array, arrayIndex);
    }

    bool ICollection<Block>.Remove(Block item)
    {
        return ((ICollection<Block>)_blocks).Remove(item);
    }

    public IEnumerator<Block> GetEnumerator()
    {
        return _blocks.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
