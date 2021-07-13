using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    private Cell _cell;
    [SerializeField]
    private MoveChip _moveChip;
    [SerializeField]
    private Animator _animator;

    private bool _isUnstable = false;
    [SerializeField]
    private float _hangTimer;
    private float _hangTime;
    [SerializeField]
    private int _colorId;

    public float Health;
    public int ColorId => _colorId;
    public bool IsSteadiness { get; private set; }

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
        _hangTime = _hangTimer;
        _moveChip.FinishedTheWay += СheckingСhanges;
    }

    void FixedUpdate()
    {
        if (_isUnstable)
        {
            if (_hangTime < 0)
            {
                _animator.SetBool("Rattling", false);

                Cell cell = GridSystem.Instance.GetCell(Cell.PosToGrid + Vector2Int.down);
                if (cell.Chip == null)
                {
                    _moveChip.ActivationMove(cell.transform);
                    Cell.Chip = null;
                    _cell = cell;

                    Cell.Chip = this;
                    _isUnstable = false;
                }
                else
                {
                    СheckingСhanges();
                }
            }
            else
            {
                _hangTime -= Time.deltaTime;
            }
        }
    }
    public void Consume()
    {
        Cell.Chip = null;
        Destroy(gameObject);
    }
    private bool CommitCheck()
    {
        Cell cellDown = GridSystem.Instance.GetCell(Cell.PosToGrid + Vector2Int.down);
        if (cellDown == null || (cellDown.Chip != null && cellDown.Chip.IsSteadiness))
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
            _animator.SetBool("Rattling",true);
            _isUnstable = true;
            IsSteadiness = false;
        }
        else
        {
            _hangTime = _hangTimer;
            IsSteadiness = true;
            _isUnstable = false;
        }
    }
}
