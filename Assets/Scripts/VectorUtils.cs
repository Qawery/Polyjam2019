using UnityEngine;

public static class VectorUtils
{
	public static Vector2 XY(this Vector3 vec)
	{
		return new Vector2(vec.x, vec.y);
	}

	public static Vector3 To3D(this Vector2 vec, float z = 0)
	{
		return new Vector3(vec.x, vec.y, 0);
	}
}
