using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileGenerator : MonoBehaviour
{
	public GameObject tileObject;
	public GameObject playerObject;
	public float offsetY;
	public float offsetX;
	public float horizontalOffset = 4;
	public float speed = 0.1f;

	public int tileBufferSize;
	public GameObject[] topTiles;
	public GameObject[] bottomTiles;

	private float topY;
	private float bottomY;


	

	void Start () {
		this.topTiles = new GameObject[tileBufferSize];
		this.bottomTiles = new GameObject[tileBufferSize];
		this.topY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
		this.bottomY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
		
		this.initWorld();
	}

	void Update ()
	{
		for(int i=0;i < tileBufferSize; i++) {
			topTiles[i].transform.position -= new Vector3(speed, 0, 0);
		}
		for(int i=0;i < tileBufferSize; i++) {
			bottomTiles[i].transform.position -= new Vector3(speed, 0, 0);
		}

		GameObject topFirstTile = topTiles[0];
		float topFirstTileWidth = topFirstTile.GetComponent<BoxCollider2D>().size.x;
		
		if (isOffscreen(topFirstTile)) {
			Vector3 newPos = topTiles[tileBufferSize - 1].transform.position + new Vector3(offsetX, 0, 0);
			topFirstTile.transform.position = newPos;
			topTiles[0] = topFirstTile;
			
			shiftIndexes(ref topTiles);
		}

		GameObject bottomFirstTile = bottomTiles[0];
		float bottomFirstTileWidth = bottomFirstTile.GetComponent<BoxCollider2D>().size.x;
		
		if (isOffscreen(bottomFirstTile)) {
			Vector3 newPos = bottomTiles[tileBufferSize - 1].transform.position + new Vector3(offsetX, 0, 0);
			bottomFirstTile.transform.position = newPos;
			bottomTiles[0] = bottomFirstTile;
			
			shiftIndexes(ref bottomTiles);
		}
	}

	void shiftIndexes(ref GameObject[] tiles) {
		GameObject[] tmp = new GameObject[tileBufferSize];
		for(int i=1;i < tileBufferSize; i++) {
			tmp[i-1] = tiles[i];
		}
		tmp[tileBufferSize - 1] = tiles[0];
		tiles = tmp;
	}

	bool isOffscreen(GameObject tile) {
		Vector3 pos = tile.transform.position;
		pos.x += tile.GetComponent<BoxCollider2D>().size.x / 2;
		float screenX = Camera.main.WorldToScreenPoint(pos).x;

		if (screenX < tile.GetComponent<BoxCollider2D>().size.x)
			return true;

		return false;
	}

	void initWorld() {
		for (int i=0;i < tileBufferSize;i++) {
			GameObject tile = GameObject.Instantiate(tileObject, new Vector3(i * offsetX - horizontalOffset, topY - offsetY, 0), Quaternion.identity) as GameObject;
			topTiles[i] = tile;
		}

		for (int i=0;i < tileBufferSize;i++) {
			GameObject tile = GameObject.Instantiate(tileObject, new Vector3(i * offsetX - horizontalOffset, bottomY + offsetY, 0), Quaternion.Euler(new Vector3(0, 0, -180))) as GameObject;
			bottomTiles[i] = tile;
		}
	}
}
