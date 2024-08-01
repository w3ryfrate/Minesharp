using Minesharp.Graphics;
using System;
using System.Collections.Generic;

namespace Minesharp.World;

public enum BlockFaces
{
    FRONT,
    BACK,
    TOP,
    BOTTOM,
    LEFT,
    RIGHT,
}
public enum Visibility
{
    SHOWN,
    HIDDEN,
}

public class Block
{
    // It's a unique Tag for this Block,
    // it's linked to the Chunk's Tag.
    public int Tag { get; }

    // It's the 3D Position Vector of the Block
    public Vector3 Position;

    // Contains the graphic data necessary
    // to draw the Block on screen
    public BlockGraphicsData GraphicsData;

    // It's the dictionary that stores whether a face should be shown
    // or hidden. All faces are hidden by default.
    public Dictionary<BlockFaces, Visibility> BlockFacesVisibility;

    // It's the Chunk of this Block
    public Chunk Chunk { get; }
    // It's the Strip of this Block
    public Strip Strip { get; private set; }

    public Block(GraphicsDevice graphics, Chunk chunk, Vector3 position, Texture2D texture)
    {
        Chunk = chunk;
        Tag = int.Parse($"{Chunk.Tag}{Chunk.BlocksInChunk.Count}");
        Position = position;
        GraphicsData = new BlockGraphicsData(this, graphics, texture);
        BlockFacesVisibility = new()
        {
            { BlockFaces.FRONT, Visibility.SHOWN },
            { BlockFaces.BACK, Visibility.SHOWN },
            { BlockFaces.TOP, Visibility.SHOWN },
            { BlockFaces.BOTTOM, Visibility.SHOWN },
            { BlockFaces.LEFT, Visibility.SHOWN },
            { BlockFaces.RIGHT, Visibility.SHOWN },
        };

        Chunk.OnChunkGeneratedEvent += OnChunkGenerated;
    }

    public void Update()
    {
        HideUnseenFaces();
    }

    public void Draw()
    {
        GraphicsData.Draw();
    }

    private void OnChunkGenerated()
    {
        Strip = Chunk.StripsContainer[(int)Position.Z];
    }

    public void ShowFace(BlockFaces face)
    {
        BlockFacesVisibility[face] = Visibility.SHOWN;
    }
    public void HideFace(BlockFaces face)
    {
        BlockFacesVisibility[face] = Visibility.HIDDEN;
    }

    public void ShowAllFaces()
    {
        foreach (BlockFaces face in BlockFacesVisibility.Keys)
        {
            BlockFacesVisibility[face] = Visibility.SHOWN;
        }
    }
    public void HideAllFaces()
    {
        foreach (BlockFaces face in BlockFacesVisibility.Keys)
        {
            BlockFacesVisibility[face] = Visibility.HIDDEN;
        }
    }

    public void HideUnseenFaces()
    {
        /*  --- TODO --
         *  Find an actual algorithm to hide those faces,
         *  this should be called in 'Update' cause we want the blocks
         *  to always know which face to hide.
         */
    }
    
    // Finds a Block at determined Position
    public static Block GetBlockAtPosition(Chunk chunk, Vector3 position, int startStripIndex = 0, int finishStripIndex = 15)
    {
        for (int n = startStripIndex; n < finishStripIndex; n++)
        {
            foreach (Block block in chunk.StripsContainer[n])
            {
                if (block.Position == position)
                {
                    return block;
                }
            }
        }

        return null;
    }

    public static bool IsAdjacent(Block b1, Block b2)
    {
        Vector3 difference = b1.Position - b2.Position;

        return (Math.Abs(difference.X) == 1 && difference.Y == 0 && difference.Z == 0) ||
               (Math.Abs(difference.Y) == 1 && difference.X == 0 && difference.Z == 0) ||
               (Math.Abs(difference.Z) == 1 && difference.X == 0 && difference.Y == 0);
    }
    public static bool IsAdjacent(Vector3 p1, Vector3 p2)
    {
        Vector3 difference = p1 - p2;

        return (Math.Abs(difference.X) == 1 && difference.Y == 0 && difference.Z == 0) ||
               (Math.Abs(difference.Y) == 1 && difference.X == 0 && difference.Z == 0) ||
               (Math.Abs(difference.Z) == 1 && difference.X == 0 && difference.Y == 0);
    }

}
