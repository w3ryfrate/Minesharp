using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Minesharp.World;

public class Chunk
{
    // It's a unique Tag for this Chunk
    public int Tag { get; }

    public const int CHUNK_LENGTH = 16;
    public const int CHUNK_WIDTH = 16;
    public const int CHUNK_HEIGHT = 16;
    public const int BLOCKS_AMOUNT = CHUNK_WIDTH * CHUNK_HEIGHT * CHUNK_WIDTH;

    public event Action OnChunkGeneratedEvent;

    public List<Block> BlocksInChunk;

    // This List contains the Lists of tstrips of blocks.
    // The whole purpose of this List is to check by strip and not to
    // iterate the whole Chunk at once.
    public List<Strip> StripsContainer = new(16);

    private GraphicsDevice _graphicsDevice;

    public Chunk(GraphicsDevice graphicsDevice)
    {
        Tag = 0;


        GenerateStrips();

        BlocksInChunk = new(BLOCKS_AMOUNT);
        _graphicsDevice = graphicsDevice;
        OnChunkGeneratedEvent += OnFinishedGeneration;
    }

    public void GenerateChunk()
    {
        for (int x = 0; x < CHUNK_LENGTH; x++)
        {
            for (int y = 0; y < CHUNK_HEIGHT; y++)
            {
                for (int z = 0; z < CHUNK_WIDTH; z++)
                {
                    Block block = new(_graphicsDevice, this, new(x, y, z), Game1._DIRT_TEX);
                    BlocksInChunk.Add(block);
                    
                    switch (block.Position.Z)
                    {
                        case 0:
                            StripsContainer[0].Add(block);
                            break;
                        case 1:
                            StripsContainer[1].Add(block);
                            break;
                        case 2:
                            StripsContainer[2].Add(block);
                            break;
                        case 3:
                            StripsContainer[3].Add(block);
                            break;
                        case 4:
                            StripsContainer[4].Add(block);
                            break;
                        case 5:
                            StripsContainer[5].Add(block);
                            break;
                        case 6:
                            StripsContainer[6].Add(block);
                            break;
                        case 7:
                            StripsContainer[7].Add(block);
                            break;
                        case 8:
                            StripsContainer[8].Add(block);
                            break;
                        case 9:
                            StripsContainer[9].Add(block);
                            break;
                        case 10:
                            StripsContainer[10].Add(block);
                            break;
                        case 11:
                            StripsContainer[11].Add(block);
                            break;
                        case 12:
                            StripsContainer[12].Add(block);
                            break;
                        case 13:
                            StripsContainer[13].Add(block);
                            break;
                        case 14:
                            StripsContainer[14].Add(block);
                            break;
                        case 15:
                            StripsContainer[15].Add(block);
                            break;
                    }
                }
            }
        }

        OnChunkGeneratedEvent?.Invoke();
    }

    public void Draw()
    {
        DrawAllStrips();
    }

    public void Update()
    {
        UpdateAllStrips();
    }

    private void OnFinishedGeneration()
    {
        // Gets called once when the generation is complete
    }

    private void GenerateStrips()
    {
        // Gets called in init and initializes all of the strips
        for (int n = 0; n < StripsContainer.Capacity; n++)
        {
            StripsContainer.Add(new(StripsContainer, 256));
        }
    }

    private void DrawAllStrips()
    {
        for (int n = 0; n < StripsContainer.Count; n++)
        {
            StripsContainer[n].Draw();
        }
    }
    private void UpdateAllStrips()
    {
        for (int n = 0; n < StripsContainer.Count; n++)
        {
            StripsContainer[n].Update();
        }
    }
}
