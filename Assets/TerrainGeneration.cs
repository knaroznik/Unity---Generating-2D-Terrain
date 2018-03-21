﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour {

	public GameObject tilePrefab;
	public Sprite waterSprite;
	public Sprite grassSprite;
	public Sprite sandSprite;
	public Sprite stoneSprite;

	private int size = 400;
	private int seed;

	private float[,] elevation;

	// Use this for initialization
	void Start () {
		seed = Random.Range (0, 10000);
		elevation = new float[size, size];
		generateTerrain ();
		showTerrain ();
	}

	void createTile(int x, int y, Sprite sprite){
		GameObject obj = Instantiate (tilePrefab, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
		obj.GetComponent<SpriteRenderer> ().sprite = sprite;
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
				createTile(x - size/2, y - size/2,biome(elevation[y,x]));
			}
		}
	}

	Sprite biome(float e){
		if (e < 0.68)
			return waterSprite;
		else if (e < 0.78)
			return sandSprite;
		else if (e > 0.9)
			return stoneSprite;
		else return grassSprite;
	}
}
