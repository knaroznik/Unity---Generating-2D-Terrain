using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGeneration : MonoBehaviour {

	public TileBase waterSprite;
	public TileBase grassSprite;
	public TileBase sandSprite;
	public TileBase stoneSprite;

	public Tilemap map;

	private int size = 500;
	private int seed;

	private float[,] elevation;

	// Use this for initialization
	void Start () {
		seed = Random.Range (0, 10000);
		elevation = new float[size, size];
		generateTerrain ();
		showTerrain ();
	}

	void generateTerrain(){
		for (int y = 0; y < size; y++) {
			for (int x = 0; x < size; x++) {
				float nx = (float)x / (float)size - 0.5f + seed;
				float ny = (float)y / (float)size - 0.5f + seed;
				float e =    1f * Mathf.PerlinNoise(1 * nx, 1 * ny)
					+  0.5f * Mathf.PerlinNoise(2 * nx, 2 * ny)
					+ 0.25f * Mathf.PerlinNoise(4 * nx, 2 * ny);
				elevation [y,x] = Mathf.Pow (e, 2.14f);
			}
		}
	}

	void showTerrain(){
		for (int y = 0; y < size; y++) {
			for (int x = 0; x < size; x++) {
				map.SetTile (new Vector3Int (x-size/2, y-size/2, 0), biome (elevation [x, y]));
			}
		}
	}

	TileBase biome(float e){
		if (e < 0.68)
			return waterSprite;
		else if (e < 0.78)
			return sandSprite;
		else if (e > 0.9)
			return stoneSprite;
		else return grassSprite;
	}
}
