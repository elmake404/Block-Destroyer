using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour
{
    private List<GameObject> _floor = new List<GameObject>();

    public bool IsFloor { get { return _floor.Count > 0; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer==8)
        {
            _floor.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_floor.Contains(other.gameObject))
        {
            _floor.Remove(other.gameObject);
        }
    }
}
