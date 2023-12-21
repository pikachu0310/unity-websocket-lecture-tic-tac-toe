using System;
using UnityEngine;

public class Board : MonoBehaviour
{
    // 0 1 2
    // 3 4 5
    // 6 7 8
    [SerializeField] private Cell[] _cells;

    public void SetCellClickEvent(Action<Vector2Int> callback)
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            var index = new Vector2Int(i % 3, i / 3);
            _cells[i].SetClickEvent(() => callback.Invoke(index));
        }
    }
    
    public void UpdateView(Cell.ViewType[] cellTypes)
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i].UpdateView(cellTypes[i]);
        }
    }
}