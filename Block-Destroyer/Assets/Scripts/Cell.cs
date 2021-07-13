using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public delegate void CellChip(Cell cell,Chip chip);
    public event CellChip OnChipChanged;
    public Vector2Int PosToGrid { get; private set; }
    private Chip _chip;

    public Chip Chip
    {
        get { return _chip; }
        set
        {
            _chip = value;

            if (_chip != null)
            {
                _chip.transform.SetParent(transform);
            }
        }
    }

    private void Start()
    {
        PosToGrid = GridSystem.Instance.WorldToGrid(transform.position);
    }
}
