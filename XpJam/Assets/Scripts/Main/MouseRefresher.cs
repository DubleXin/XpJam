using UnityEngine;
using UnityEngine.InputSystem;

public class MouseRefresher : MonoBehaviour 
{
    private void Update() => Angle.MousePosition = Mouse.current.position.ReadValue();
}
