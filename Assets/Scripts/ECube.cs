public class ECube {

	private int _x, _y, _z;
	private int _type;

	public ECube(int x, int y, int z, int type) 
	{
		_x = x;
		_y = y;
		_z = z;
		_type = type;
	}

	public int X
	{
		get {
			return _x;
		}

		set {
			_x = value;
		}
	}


	public int Y
	{
		get {
			return _y;
		}

		set {
			_y = value;
		}
	}


	public int Z
	{
		get {
			return _z;
		}

		set {
			_z = value;
		}
	}


	public int Type
	{
		get {
			return _type;
		}

		set {
			_type = value;
		}
	}
}