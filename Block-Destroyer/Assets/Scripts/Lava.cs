using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    void Start()
    {

    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.down * _speed);
    }
}
