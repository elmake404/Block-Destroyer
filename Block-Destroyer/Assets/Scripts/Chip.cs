﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    private Cell _cell;
    [SerializeField]
    private MoveChip _moveChip;
    [SerializeField]
    private Animator _animator;
    private Chip[] _chopsGroup;

    [SerializeField]
    private float _hangTimer;
    private float _hangTime;
    [SerializeField]
    private int _colorId;

    public float Health;
    public int ColorId => _colorId;
    public bool IsSteadiness /*{ get; private set; }*/;

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
        _chopsGroup = MatchSystem.Instance.GetGroup(Cell);
        CommitFloor();
        _hangTime = _hangTimer;
        _moveChip.FinishedTheWay += СheckingСhanges;
        _moveChip.FinishedTheWay += Atack;
    }

    void FixedUpdate()
    {
        if (!_moveChip.enabled && !CommitCheck())
        {
            _animator.SetBool("Rattling", true);

            //MatchSystem.Instance.GroupStabilityCheck(Cell);
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
    private void CommitFloor()
    {
        Cell cellDown = GridSystem.Instance.GetCell(Cell.PosToGrid + Vector2Int.down);
        if (cellDown == null || (cellDown.Chip != null && cellDown.Chip.IsSteadiness))
            IsSteadiness = true;
        else
            IsSteadiness = false;

    }
    private bool CommitCheck()
    {
        if (IsSteadiness || CommitCheckGroup())
        {
            _hangTime = _hangTimer;
            return true;
        }
        else
            return false;
    }
    private bool CommitCheckGroup()
    {
        foreach (var item in _chopsGroup)
        {
            if (item.IsSteadiness) return true;
        }
        return false;
    }
    private void Atack()
    {
        if(CommitCheck())
        MatchSystem.Instance.TryConsumeMatch(Cell);
    }
    public void InPlace()
    {
        _animator.SetBool("Rattling", false);

        _hangTime = _hangTimer;
    }
    
    public void СheckingСhanges()
    {
        CommitFloor();
        _chopsGroup = MatchSystem.Instance.GetGroup(Cell);
    }

}
