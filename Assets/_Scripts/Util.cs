using UnityEngine;
using System.Collections;

public class Util
{
    // http://answers.unity3d.com/questions/162177/vector2angles-direction.html
    // pure fucking magic
    public static float VectorAngleWithSign(Vector2 a, Vector2 b)
    {
        float ang = Vector2.Angle(a, b);
        Vector3 cross = Vector3.Cross(a, b);

        if (cross.z > 0)
            ang = 360 - ang;

        if (ang > 180)
            ang = -(360 - ang);

        return -ang;
    }
}