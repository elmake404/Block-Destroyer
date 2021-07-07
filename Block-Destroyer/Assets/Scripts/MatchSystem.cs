﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSystem : MonoBehaviour
{
    private readonly Queue<Cell> _cellsToProcess = new Queue<Cell>();
    private readonly HashSet<Cell> _processedCells = new HashSet<Cell>();
    private readonly List<Chip> _matchedChips = new List<Chip>();


    void Start()
    {

    }

    void Update()
    {

    }
    public void TryConsumeMatch(Cell originCell)
    {
        if (originCell?.Chip == null)
        {
            return;
        }

        _cellsToProcess.Enqueue(originCell);
        _processedCells.Add(originCell);

        while (_cellsToProcess.Count > 0)
        {
            Cell cell = _cellsToProcess.Dequeue();
            _matchedChips.Add(cell.Chip);

            TryEnqueueNeighbour(cell, Vector2Int.left);
            TryEnqueueNeighbour(cell, Vector2Int.right);
            TryEnqueueNeighbour(cell, Vector2Int.up);
            TryEnqueueNeighbour(cell, Vector2Int.down);
        }

        foreach (Chip chip in _matchedChips)
        {
            chip.Consume();
        }

        _cellsToProcess.Clear();
        _processedCells.Clear();
        _matchedChips.Clear();
    }
    private void TryEnqueueNeighbour(Cell cell, Vector2Int neighbourOffset)
    {
        Cell neighbour = GridSystem.Instance.GetCell(cell.PosToGrid + neighbourOffset);
        if (!_processedCells.Contains(neighbour) && neighbour?.Chip != null && cell.Chip.ColorId == neighbour.Chip.ColorId)
        {
            _processedCells.Add(neighbour);
            _cellsToProcess.Enqueue(neighbour);
        }
    }

}
