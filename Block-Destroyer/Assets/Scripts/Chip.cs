using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    [SerializeField]
    private int _colorId;
    [SerializeField]
    private Cell _cell;
    [SerializeField]
    private MoveChip _moveChip;
    public int ColorId => _colorId;
 
    public Cell Cell
    {
        get
        {
            if (_cell == null)
            {
                _cell = transform.GetComponentInParent<Cell>();
            }
            return _cell;
        }
    }

    void Start()
    {
        _moveChip.FinishedTheWay += СheckingСhanges;
    }

    void Update()
    {

    }
    public void Consume()
    {
        Cell.Chip = null;
        Destroy(gameObject);
    }
    private bool CommitCheck()
    {
        Cell cellDown = GridSystem.Instance.GetCell(Cell.PosToGrid + Vector2Int.down);
        if (cellDown == null || cellDown.Chip != null)
            return true;
        else
            return false;
        //Queue<Cell> cells = new Queue<Cell>();
        //cells.Enqueue(GridSystem.Instance.GetCell(Cell.PosToGrid + Vector2Int.left));
        //cells.Enqueue(GridSystem.Instance.GetCell(Cell.PosToGrid + Vector2Int.right));
        //cells.Enqueue(GridSystem.Instance.GetCell(Cell.PosToGrid + Vector2Int.up));
        //while (cells.Count>0)
        //{
        //    Cell cell = cells.Dequeue();
        //    if (cell!=null)
        //    {
        //        if (cell.Chip.ColorId==ColorId)
        //        {

        //        }
        //    }
        //}
    }
    public void СheckingСhanges()
    {
        if (!CommitCheck())
        {
            Cell cell = GridSystem.Instance.GetCell(Cell.PosToGrid + Vector2Int.down);
            _moveChip.ActivationMove(cell.transform);
            Cell.Chip = null;
            _cell = cell;
            Cell.Chip = this;
        }
    }
}
