using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtil
{
    public static Vector2 AngleToVector2(float angle)
    {
        angle *= Mathf.Deg2Rad;
        return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
    }

    public static float Vector2ToAngle(Vector2 vector)
    {
        return (float)Math.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }
}
