using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game {
	GamePiece?[,] tileData;
	GamePiece currentTile;
	GamePiece storedTile;
	Queue<GameEvent> eventQueue = new Queue<GameEvent> ();
	List<Position> uncoveredArrows = new List<Position>();
	int[] chanceArray;
	// Use this for initialization

	public void initialise(int xwidth, int ywidth, int[] chances)
	{
		if (chances.GetLength (0) != 100) {
			throw new System.ArgumentException ("chances array must be of length 100");
		}
		chanceArray = chances;
		tileData = new GamePiece?[xwidth, ywidth];
		currentTile = new GamePiece ();
		currentTile.up = true;
		currentTile.left = true;
		currentTile.right = true;
		currentTile.down = true;
		storedTile = generatePiece ();
		queueEvent (new CurrentTileChangedEvent (currentTile));
		queueEvent (new StoredTileChangedEvent (storedTile));

	}

	void queueEvent(GameEvent evt)
	{
		eventQueue.Enqueue (evt);
	}

	public GameEvent getQueuedEvent()
	{
		if (eventQueue.Count > 0) {
			return eventQueue.Dequeue ();
		} else {
			return null;
		}
	}

	public GamePiece getCurrentTile()
	{
		return currentTile;
	}

	public GamePiece getStoredPiece()
	{
		return storedTile;
	}

	private GamePiece generatePiece()
	{
		int i = Random.Range(0,99);

		int hash = chanceArray [i];
		Debug.Log (i.ToString () + "--" + hash.ToString ());
		GamePiece g = GamePiece.fromHash (hash);
		return g;
	}

	public bool[,] getPlaceablePositions(GamePiece t)
	{
		bool[,] a = new bool[tileData.GetLength (0), tileData.GetLength (1)];
		if (currentTile.GetHashCode() == 15) {
			return a;
		}
		for (int i = 0; i < uncoveredArrows.Count; i++) {
			Position p = uncoveredArrows [i];
			int x = p.x;
			int y = p.y;
			a [x, y] = canPlacePiece (t, x, y);
		}
		return a;
	}

	public bool[,] getPlaceablePositions()
	{
		return getPlaceablePositions (currentTile);
	}

	private GameEvent checkWinLoss()
	{
		if (uncoveredArrows.Count == 0) {
			return new GameWonEvent ();
		} else {
			GamePiece q = currentTile;
			if (countTrue (getPlaceablePositions (q)) > 0) {
				return null;
			}
			q = q.rotated ();
			if (countTrue (getPlaceablePositions (q)) > 0) {
				return null;
			}
			q = q.rotated ();
			if (countTrue (getPlaceablePositions (q)) > 0) {
				return null;
			}
			q = q.rotated ();
			if (countTrue (getPlaceablePositions (q)) > 0) {
				return null;
			}
			q = storedTile;
			if (countTrue (getPlaceablePositions (q)) > 0) {
				return null;
			}
			q = q.rotated ();
			if (countTrue (getPlaceablePositions (q)) > 0) {
				return null;
			}
			q = q.rotated ();
			if (countTrue (getPlaceablePositions (q)) > 0) {
				return null;
			}
			q = q.rotated ();
			if (countTrue (getPlaceablePositions (q)) > 0) {
				return null;
			}
		}
		return new GameLostEvent();


	}

	private int countTrue(bool[,] arr)
	{
		int r = 0;
		for (int x = 0; x < arr.GetLength (0); x++) {
			for (int y = 0; y < arr.GetLength (1); y++) {
				if (arr [x, y]) {
					r++;
				}
			}
		}
		return r;
	}

	public GamePiece swapStoredPiece()
	{
		GamePiece q = storedTile;
		storedTile = currentTile;
		currentTile = q;
		queueEvent (new CurrentTileChangedEvent (currentTile));
		queueEvent (new StoredTileChangedEvent (storedTile));
		return q;
	}

	public bool canPlacePiece(GamePiece piece, int x, int y)
	{
		if (isInBounds (x, y) == false) {
			return false;
		}
		if (tileData [x, y] != null) {
			return false;
		}
		if ((piece.left) && (piece.right) && (piece.down) && (piece.up)) {
			return ((x != 0) && (x != tileData.GetLength (0) - 1) && (y != 0) && (y != tileData.GetLength (1) - 1));
		}
		int inputs = 0;
		if (isInBounds (x + 1, y)) {
			if (tileData [x + 1, y] != null) {
				if (((GamePiece)(tileData [x + 1, y])).left) {
					if (piece.right) {
						return false;
					} else {
						inputs++;
					}
				}
			}
		} else {
			if (piece.right) {
				return false;
			}
		}
		if (isInBounds (x - 1, y)) {
			if (tileData [x - 1, y] != null) {
				if (((GamePiece)(tileData [x - 1, y])).right) {
					if (piece.left) {
						return false;
					} else {
						inputs++;
					}
				}
			}
		} else {
			if (piece.left) {
				return false;
			}
		}
		if (isInBounds (x, y + 1)) {
			if (tileData [x, y + 1] != null) {
				if (((GamePiece)(tileData [x, y + 1])).down) {
					if (piece.up) {
						return false;
					} else {
						inputs++;
					}
				}
			}
		} else {
			if (piece.up) {
				return false;
			}
		}
		if (isInBounds (x, y - 1)) {
			if (tileData [x, y - 1] != null) {
				if (((GamePiece)(tileData [x, y - 1])).up) {
					if (piece.down) {
						return false;
					} else {
						inputs++;
					}
				}
			}
		} else {
			if (piece.down) {
				return false;
			}
		}
		return (inputs > 0);
	}

	public bool isInBounds(int x, int y)
	{
		return ((x < tileData.GetLength (0)) && (x >= 0) && (y < tileData.GetLength (1)) && (y >= 0));
	}
		
	public void rotateCurrentPieceCW()
	{
		bool b = currentTile.up;
		currentTile.up = currentTile.left;
		currentTile.left = currentTile.down;
		currentTile.down = currentTile.right;
		currentTile.right = b;
		queueEvent (new CurrentTileChangedEvent (currentTile));
	}

	public void rotateCurrentPieceCCW()
	{
		bool b = currentTile.up;
		currentTile.up = currentTile.right;
		currentTile.right = currentTile.down;
		currentTile.down = currentTile.left;
		currentTile.left = b;
		queueEvent (new CurrentTileChangedEvent (currentTile));
	}

	public bool placePiece(int x, int y)
	{
		if (canPlacePiece (currentTile, x, y) == false) {
			return false;
		}
		tileData [x, y] = currentTile;
		if (uncoveredArrows.Contains (new Position (x, y))) {
			uncoveredArrows.Remove (new Position (x, y));
		}
		if (currentTile.up) {
			if (isInBounds (x, y + 1)) {
				if (tileData [x, y + 1] == null) {
					if (uncoveredArrows.Contains (new Position (x, y + 1)) == false) {
						uncoveredArrows.Add (new Position (x, y + 1));
					}
				}
			}
		}
		if (currentTile.left) {
			if (isInBounds (x - 1, y)) {
				if (tileData [x - 1, y] == null) {
					if (uncoveredArrows.Contains (new Position (x - 1, y)) == false) {
						uncoveredArrows.Add (new Position (x - 1, y));
					}
				}
			}
		}
		if (currentTile.right) {
			if (isInBounds (x + 1, y)) {
				if (tileData [x + 1, y] == null) {
					if (uncoveredArrows.Contains (new Position (x + 1, y)) == false) {
						uncoveredArrows.Add (new Position (x + 1, y));
					}
				}
			}
		}
		if (currentTile.down) {
			if (isInBounds (x, y - 1)) {
				if (tileData [x, y - 1] == null) {
					if (uncoveredArrows.Contains (new Position (x, y - 1)) == false) {
						uncoveredArrows.Add (new Position (x, y - 1));
					}
				}
			}
		}
		queueEvent (new TileChangedEvent (currentTile, x, y));
		currentTile = generatePiece ();
		queueEvent (new CurrentTileChangedEvent (currentTile));
		GameEvent wl = checkWinLoss ();
		if (wl != null) {
			queueEvent (wl);
		}
		return true;
	}

	private struct Position {
		public int x;
		public int y;
		public Position(int X, int Y)
		{
			x = X;
			y = Y;
		}
		public override bool Equals (object obj)
		{
			if (obj.GetType () != this.GetType ()) {
				return false;
			}
			Position p = (Position)obj;
			return ((p.x == x) && (p.y == y));
		}
	}

}

public struct GamePiece {
	public bool up;
	public bool down;
	public bool left;
	public bool right;
	public override bool Equals (object obj)
	{
		if (!(obj is GamePiece)) {
			return false;
		}
		GamePiece other = (GamePiece)obj;
		return ((other.up == up) && (other.left == left) && (other.down == down) && (other.right == right));
	}
	public override int GetHashCode ()
	{
		int q = 0;
		if (up) {
			q += 1;
		}
		if (left) {
			q += 2;
		} 
		if (right) {
			q += 4;
		}
		if (down) {
			q += 8;
		}
		return q;
	}
	public static bool operator ==(GamePiece a, GamePiece b)
	{
		return a.Equals (b);
	}
	public static bool operator !=(GamePiece a, GamePiece b)
	{
		return !a.Equals (b);
	}
	public GamePiece rotated()
	{
		GamePiece r = new GamePiece ();
		r.left = down;
		r.down = right;
		r.up = left;
		r.right = up;
		return r;
	}
	public static GamePiece fromHash(int h)
	{
		GamePiece c = new GamePiece();
		c.down = (h / 8 == 1);
		c.right = ((h / 4) % 2 == 1);
		c.left = ((h / 2) % 2 == 1);
		c.up = (h % 2 == 1);
		return c;
	}
}

public class GameEvent {

}

public class CurrentTileChangedEvent : GameEvent {
	public GamePiece newCurrentTile;
	public CurrentTileChangedEvent(GamePiece n)
	{
		newCurrentTile = n;
	}
}

public class StoredTileChangedEvent : GameEvent {
	public GamePiece newStoredTile;
	public StoredTileChangedEvent(GamePiece n)
	{
		newStoredTile = n;
	}
}

public class TileChangedEvent : GameEvent {
	public int x;
	public int y;
	public GamePiece currentPiece;
	public TileChangedEvent(GamePiece n, int X, int Y)
	{
		currentPiece = n;
		x = X;
		y = Y;
	}
}

public class GameLostEvent : GameEvent {

}

public class GameWonEvent : GameEvent {

}



