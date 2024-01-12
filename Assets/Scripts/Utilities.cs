using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static float wrap(float value, float min, float max)
    {
        return value > max ? min : value < min ? max : value;
    }

	public static Vector3 wrap(Vector3 value, Vector3 min, Vector3 max)
	{
		value.x = wrap(value.x, min.x, max.x);
		value.y = wrap(value.y, min.y, max.y);
		value.z = wrap(value.z, min.z, max.z);
		return value;
	}
	public static Vector3 wrap(Vector3 value, float min, float max)
	{
		value.x = wrap(value.x, min, max);
		value.y = wrap(value.y, min, max);
		value.z = wrap(value.z, min, max);
		return value;
	}

}
