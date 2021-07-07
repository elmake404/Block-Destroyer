using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2Int PosToGrid { get; private set; }
    private Chip _chip;

    public Chip Chip
    {
        get { return _chip; }
        set
        {
            //Chip oldChip = _chip;
            _chip = value;

            if (_chip != null)
            {
                _chip.transform.SetParent(transform);
            }

            //OnChipChanged?.Invoke(this, oldChip);
        }
    }

    private void Start()
    {
        PosToGrid = GridSystem.Instance.WorldToGrid(transform.position);
    }
}
