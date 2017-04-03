public class ECube {

	private int _type;
	private int _x, _y, _z;


	public ECube(int x, int y, int z, int type) 
	{
		this._x = x;
		this._y = y;
		this._z = z;
		this._type = type;
	}

	public int X
	{
		get {
			return _x;
		}

		set {
			X = this._x;
		}
	}


	public int Y
	{
		get {
			return _y;
		}

		set {
			Y = this._y;
		}
	}


	public int Z
	{
		get {
			return _z;
		}

		set {
			Z = this._z;
		}
	}


	public int Type
	{
		get {
			return _type;
		}

		set {
			Type = this._type;
		}
	}
}