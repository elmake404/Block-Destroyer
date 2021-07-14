using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSystem : MonoBehaviour
{
    public static MatchSystem Instance;
    [SerializeField]
    private int _minAmountForDestruction;
    private void Awake()
    {
        Instance = this;
    }
    public Chip[] GetGroup(Cell originCell)
    {
        Queue<Cell> cellsToProcess = new Queue<Cell>();
        HashSet<Cell> processedCells = new HashSet<Cell>();
        List<Chip> matchedChips = new List<Chip>();

        cellsToProcess.Enqueue(originCell);
        processedCells.Add(originCell);

        while (cellsToProcess.Count > 0)
        {
            Cell cell = cellsToProcess.Dequeue();
            matchedChips.Add(cell.Chip);


            TryEnqueueNeighbour(cell, Vector2Int.left, ref cellsToProcess, ref processedCells);
            TryEnqueueNeighbour(cell, Vector2Int.right, ref cellsToProcess, ref processedCells);
            TryEnqueueNeighbour(cell, Vector2Int.up, ref cellsToProcess, ref processedCells);
            TryEnqueueNeighbour(cell, Vector2Int.down, ref cellsToProcess, ref processedCells);
        }
        return matchedChips.ToArray();
    }
    private void TryEnqueueNeighbour(Cell cell, Vector2Int neighbourOffset, ref Queue<Cell> cellsToProcess, ref HashSet<Cell> processedCells)
    {
        Cell neighbour = GridSystem.Instance.GetCell(cell.PosToGrid + neighbourOffset);
        if (!processedCells.Contains(neighbour) && neighbour?.Chip != null  && cell.Chip.ColorId == neighbour.Chip.ColorId)
        {
            processedCells.Add(neighbour);
            cellsToProcess.Enqueue(neighbour);
        }
    }
    public void TryConsumeMatch(Cell originCell, float damage)
    {
        if (originCell?.Chip == null) return;

        originCell.Chip.Health -= damage;
        if (originCell.Chip.Health <= 0)
        {

            Chip[] chips = GetGroup(originCell);

            foreach (Chip chip in chips)
            {
                chip.Consume();
            }

            GridSystem.Instance.UpdateStateCell();
        }
    }
    public void TryConsumeMatch(Cell originCell)
    {
        if (originCell?.Chip == null) return;

        Chip[] chips = GetGroup(originCell);
        if (chips.Length>=_minAmountForDestruction)
        {
            foreach (Chip chip in chips)
            {
                chip.Consume();
            }
        }

        GridSystem.Instance.UpdateStateCell();
    }
}
