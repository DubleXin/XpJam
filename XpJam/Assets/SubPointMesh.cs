using UnityEngine;

public class SubPointMesh : MonoBehaviour
{
    public Vector2[] Points { get => _points; }
    private Vector2[] _points;
    private void Awake()
    {
        _points = new Vector2[transform.childCount];
        for (int i = 0; i < _points.Length; i++)
            _points[i] = transform.GetChild(i).position;
    }
}
