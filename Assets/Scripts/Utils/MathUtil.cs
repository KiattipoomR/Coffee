using UnityEngine;

namespace Utils
{
    public static class MathUtil
    {
        public static Vector2 RotateVector2(Vector2 vector, float radianDegree)
        {
            float sin = Mathf.Sin(radianDegree);
            float cos = Mathf.Cos(radianDegree);
            
            return new Vector2(cos* vector.x - sin * vector.y, sin * vector.x + cos * vector.y);
        }
    }
}