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
            Chip oldChip = _chip;
            _chip = value;

            if (_chip != null)
            {
                _chip.transform.SetParent(transform);
                //Cell cellNeghbour = GridSystem.Instance.GetCell(PosToGrid+Vector2Int.down);
                //cellNeghbour.OnChipChanged += _chip.NeighborChange;
            }

            OnChipChanged?.Invoke(this, _chip);
        }
    }

    private void Start()
    {
        PosToGrid = GridSystem.Instance.WorldToGrid(transform.position);
    }
}
