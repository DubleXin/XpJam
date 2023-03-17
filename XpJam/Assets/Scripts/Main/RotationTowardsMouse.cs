using UnityEngine;

public class RotationTowardsMouse : MonoBehaviour
{
    private void Update() => transform.rotation = Angle.FromTargetToMouse(transform.position);
}
