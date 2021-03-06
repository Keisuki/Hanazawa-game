﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	static string loadString;
	static int generatorId = 1;
	public AudioClip[] playSounds;
	public AudioClip swapSound;
	public AudioClip newGameSound;
	Game engine;
	int width;
	int height;
	float zoomFactor = 1;
	public GameObject tilePrefab;
	public List<Sprite> tileTextures; //By hashcode;
	public Sprite canPlace;
	public Sprite cannotPlace;
	public List<GameObject> edgeTextures;
	public Tile[,] tiles;
	bool[,] previousCP;
	bool[,] hasPlaced;

	// Use this for initialization
	void Start () {
		engine = new Game ();
		width = 44;
		height = 44;
		if (loadString == null) {
			engine.initialise (width, height, PieceGenerator.fromId(generatorId));
			Stats.addAttempt (generatorId);
		} else {
			engine.initialise (loadString);
			generatorId = engine.getGeneratorId ();
		}
		tiles = new Tile[width, height];
		previousCP = new bool[width, height];
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				tiles [x, y] = createTile (x, y);
			}
		}
		GameObject q;
		for (int x = 0; x < width; x++) {
			q = Instantiate (edgeTextures [0], transform);
			q.transform.localPosition = new Vector3 (x, height, 0);
			q = Instantiate (edgeTextures [3], transform);
			q.transform.localPosition = new Vector3 (x, -1, 0);
		}
		for (int y = 0; y < height; y++) {
			q = Instantiate (edgeTextures [1], transform);
			q.transform.localPosition = new Vector3 (-1, y, 0);
			q = Instantiate (edgeTextures [2], transform);
			q.transform.localPosition = new Vector3 (width, y , 0);
		}
		q = Instantiate (edgeTextures [4], transform);
		q.transform.localPosition = new Vector3 (-1, height, 0);
		q = Instantiate (edgeTextures [5], transform);
		q.transform.localPosition = new Vector3 (width, height, 0);
		q = Instantiate (edgeTextures [6], transform);
		q.transform.localPosition = new Vector3 (-1, -1, 0);
		q = Instantiate (edgeTextures [7], transform);
		q.transform.localPosition = new Vector3 (width, -1, 0);
		hasPlaced = new bool[width, height];
		ProcessAllEvents (false);
		playSound (newGameSound);
	}



	// Update is called once per frame
	void Update () {
		
	}

	void UpdateCPTiles()
	{
		bool[,] placeable = engine.getPlaceablePositions ();
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				if (hasPlaced [x, y] == false) {
					if (placeable [x, y] != previousCP[x,y]) {
						if (placeable [x, y]) {
							tiles [x, y].SetTexture (canPlace);
						} else {
							tiles [x, y].SetTexture (cannotPlace);
						}
					}
				}
			}
		}
		previousCP = placeable;
	}

	bool ProcessEvent(bool withSound = true)
	{
		GameEvent evt = engine.getQueuedEvent ();
		if (evt == null) {
			return false;
		}
		if (evt is TileChangedEvent) {
			TileChangedEvent sEvt = (TileChangedEvent)evt;
			tiles [sEvt.x, sEvt.y].SetTexture (tileTextures [sEvt.currentPiece.GetHashCode ()]);
			hasPlaced [sEvt.x, sEvt.y] = true;
			if (withSound) {
				int randomNumber = Random.Range (0, playSounds.Length);

				AudioClip clip = playSounds [randomNumber];
				Debug.Log (randomNumber.ToString () + ", " + clip.ToString ());
				playSound (clip);
			}
			autoSave ();
			Counter.UpdateValue (engine.getPieceCount (), engine.getRemainingArrowCount ());
		}
		if (evt is CurrentTileChangedEvent) {
			CurrentTileChangedEvent sEvt = (CurrentTileChangedEvent)evt;
			GameObject.Find ("TileCurrent").GetComponent<Tile> ().SetTexture (tileTextures [sEvt.newCurrentTile.GetHashCode()]);
		}
		if (evt is StoredTileChangedEvent) {
			StoredTileChangedEvent sEvt = (StoredTileChangedEvent)evt;
			GameObject.Find ("TileStored").GetComponent<Tile> ().SetTexture (tileTextures [sEvt.newStoredTile.GetHashCode()]);
			AudioClip clip = swapSound;
			playSound (clip);
		}
		if (evt is GameWonEvent) {
			Debug.Log ("WINNER");
			Celebration.globalWin ();
			Stats.addWin (generatorId);
			WinLoss.ShowWin ();
		}
		if (evt is GameLostEvent) {
			Debug.Log ("Loser");
			WinLoss.ShowLoss ();
		}
		return true;
	}

	private void autoSave()
	{
		PlayerPrefs.SetString ("CurrentSave", GameController.SaveGame ());
	}

	void ProcessAllEvents(bool withSound = true)
	{
		int q = 0;
		bool p = true;
		while (p) {
			p = ProcessEvent (withSound);
			if (p) {
				q++;
			}
		}
		UpdateCPTiles ();
	}

	private Tile createTile (int x, int y)
	{
		GameObject q = Instantiate (tilePrefab, transform);
		q.transform.localPosition = new Vector3 (x, y, 0);
		Tile t = q.GetComponent<Tile> ();
		t.controller = this;
		t.x = x;
		t.y = y;
		return t;
	}

	private void updateZoom(float factor)
	{
		float oldFactor = zoomFactor;
		zoomFactor = oldFactor * factor;
		if (zoomFactor > 1) {
			zoomFactor = 1;
		}
		if (zoomFactor < 0.125) {
			zoomFactor = 0.125f;
		}
		float tFactor = zoomFactor / oldFactor;
		Camera.main.GetComponent<InputController> ().OnScale (tFactor);
		transform.localScale = new Vector3 (zoomFactor, zoomFactor, zoomFactor);

	}

	public void onTileTapped(int x, int y)
	{
		engine.placePiece (x, y);
		ProcessAllEvents ();
	}

	public static Vector3 GetTopRightPos()
	{
		GameController c = GameObject.Find ("Tiles").GetComponent<GameController> ();
		Tile t = c.tiles [c.width - 1, c.height - 1];
		return t.transform.position;
	}

	public static void SwapPieces()
	{
		GameController c = GameObject.Find ("Tiles").GetComponent<GameController> ();
		c.engine.swapStoredPiece ();
		c.ProcessAllEvents ();
	}

	public static void RotatePiece()
	{
		GameController c = GameObject.Find ("Tiles").GetComponent<GameController> ();
		c.engine.rotateCurrentPieceCW ();
		c.ProcessAllEvents ();
	}

	public static void Zoom(int step)
	{
		GameController c = GameObject.Find ("Tiles").GetComponent<GameController> ();
		c.updateZoom (Mathf.Pow (2, step));
	}

	public static string SaveGame()
	{
		GameController c = GameObject.Find ("Tiles").GetComponent<GameController> ();
		return c.engine.saveGame ();
	}

	public static void loadGame(string ls)
	{
		loadString = ls;
		SceneManager.LoadScene (1);
	}

	public static void startGame()
	{
		generatorId = 1;
		loadString = null;
		SceneManager.LoadScene (1);
	}

	public static void startGame(int _generatorId)
	{
		generatorId = _generatorId;
		loadString = null;
		SceneManager.LoadScene (1);
	}

	public static void playSound(AudioClip clip)
	{
		if (clip != null) {
			AudioSource.PlayClipAtPoint (clip, Camera.main.transform.position);
		}
	}


}

