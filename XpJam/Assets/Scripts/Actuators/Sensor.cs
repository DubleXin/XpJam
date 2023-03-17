using UnityEngine;

public class Sensor : MonoBehaviour
{
    public void Activate() => _activate = _inTrigger;
    private bool _activate = false;
    private bool _inTrigger = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        _inTrigger = true;
        if (collision.CompareTag("Actuator") && _activate)
            collision.GetComponent<IActuator>().Action();
        _activate = false;
    }
    private void OnTriggerExit2D(Collider2D collision) => _inTrigger = false;
   
}
