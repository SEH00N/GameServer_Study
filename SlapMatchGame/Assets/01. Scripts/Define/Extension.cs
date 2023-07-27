using UnityEngine;

public static class Extension
{
    public static Vector2 Clamp(this Vector2 original, Vector2 min, Vector2 max)
    {
        return new Vector2(Mathf.Clamp(original.x, min.x, max.x), Mathf.Clamp(original.y, min.y, max.y));
    }
}
