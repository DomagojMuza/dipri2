using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintTerrain : MonoBehaviour
{
    [System.Serializable]
    public class SplatHeights
    {
        public int textureIndex;
        public int startingHeight;
        public int overlap;
    }

    public SplatHeights[] splatHeights;
    // Start is called before the first frame update
    void Start()
    {
        TerrainData terrainData = Terrain.activeTerrain.terrainData;
        float[,,] splatmapData = new float[terrainData.alphamapWidth,
                                            terrainData.alphamapHeight,
                                            terrainData.alphamapLayers];
        for (int y = 0; y < terrainData.alphamapHeight; y++)
        {
            for(int x = 0; x < terrainData.alphamapWidth; x++)
            {
                float terrainHeight = terrainData.GetHeight(y, x);
                float[] splat = new float[splatHeights.Length];

                for(int i = 0; i < splatHeights.Length; i++)
                {
                    float thisHeightStart = splatHeights[i].startingHeight - splatHeights[i].overlap;

                    float nextHeightStart = 0;

                    if(i != splatHeights.Length - 1)
                    {
                        nextHeightStart = splatHeights[i + 1].startingHeight + splatHeights[i + 1].overlap;
                    }

                    if(i == splatHeights.Length - 1 && terrainHeight >= thisHeightStart)
                        splat[i] = 1;
                    else if (terrainHeight >= thisHeightStart &&
                        terrainHeight <= nextHeightStart)
                        splat[i] = 1;
                }

                for (int j = 0; j < splatHeights.Length; j++)
                {
                    splatmapData[x, y, j] = splat[j];
                }
            }
        }
        terrainData.SetAlphamaps(0, 0, splatmapData);
    }
}
