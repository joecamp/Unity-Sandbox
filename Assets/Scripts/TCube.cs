public class TCube {

	private int _type;
	private int _x, _y, _z;


	public TCube(int x, int y, int z, int type) 
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
	}


	public int Y
	{
		get {
			return _y;
		}
	}


	public int Z
	{
		get {
			return _z;
		}
	}


	public int Type
	{
		get {
			return _type;
		}
	}
}
