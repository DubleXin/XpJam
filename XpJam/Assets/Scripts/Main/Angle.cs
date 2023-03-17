using UnityEngine;

public static class Angle
{
    public static Vector3 MousePosition;
    public static Quaternion FromTargetToMouse(Vector2 Target)
    {
        Vector2 Direction = (MousePosition - Camera.main.WorldToScreenPoint(Target)).normalized;
        float Z = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, Z);
    }

    public static float FromScreenCentreToMouse()
    {
        Vector2 Direction = MousePosition - new Vector3(Screen.width/2, Screen.height/2, 0);
        float Z = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        return Z;
    }

    public static float DistanceFromScreenCentreToMouse()
    {
        return Vector2.Distance(MousePosition, new Vector2(Screen.width / 2, Screen.height / 2));
    }

    public static Quaternion FromAtoB(Vector2 A, Vector2 B)
    {
        Vector2 Direction = (B - A).normalized;
        float Z = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, Z);
    }
}
