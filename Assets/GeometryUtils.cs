using UnityEngine;

public static class GeometryUtils
{
    public static Vector3 GetBackBorder(GameObject obj)
    {
        var transform = obj.transform;
        var position = transform.position;
        var scale = transform.localScale;

        return position + (Vector3.back * scale.z / 2.0f);
    }

    public static Vector3 GetFrontBorder(GameObject obj)
    {
        var transform = obj.transform;
        var position = transform.position;
        var scale = transform.localScale;

        return position + (Vector3.forward * scale.z / 2.0f);
    }
}