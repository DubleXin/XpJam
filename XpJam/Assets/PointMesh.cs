using System.Collections.Generic;
using UnityEngine;

public class PointMesh : MonoBehaviour
{
    [SerializeField] List<Vector2> _points;

    private void Awake()
    {
        int childCount = transform.childCount;
        _points = new();
        for (int i = 0; i < childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out PatrolPoint patrolPoint))
                _points.Add(transform.GetChild(i).transform.position);
            bool isComposite = false;
            List<Vector2> points = new List<Vector2>();
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                isComposite = transform.GetChild(i).GetChild(j).TryGetComponent(out patrolPoint);
                points.Add(transform.GetChild(i).GetChild(j).transform.position);
            }
            if (isComposite)
                _points.AddRange(points);
        }
    }
    public (Vector2,int) GetNearestPoint(Vector2 position, int[] mask = null)
    {
        (Vector2, int) result = (_points[0], 0);
        for( int i =0; i<  _points.Count; i++)
        {
            bool isMasked = false;
            if(mask != null)
                foreach (var pointMask in mask)
                    if(i == pointMask)
                        isMasked = true;
            if (isMasked)
                continue;
            if ((result.Item1 - position).magnitude > (_points[i] - position).magnitude)
                result = (_points[i],i);
        }
        return result;
    }
}
