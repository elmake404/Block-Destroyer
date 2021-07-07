using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{ 
    [SerializeField]
    private int _colorId;
    private Cell _cell;

    public int ColorId => _colorId;
    public Cell Cell { get {
            if (_cell==null)
            {
                _cell = transform.GetComponentInParent<Cell>();
            }
            return _cell; } }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void Consume()
    {
        Cell.Chip = null;
        Destroy(gameObject);
    }

}
