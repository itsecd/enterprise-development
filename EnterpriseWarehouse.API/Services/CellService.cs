using EnterpriseWarehouse.Domain.Entities;

namespace EnterpriseWarehouse.API.Services;

public class CellService
{
    private readonly List<Cell> _cells = [];

    private int _id = 1;

    public List<Cell> GetCells()
    {
        return _cells;
    }

    public Cell? GetCellById(int id)
    {
        return _cells.Find(o => o.Id == id);
    }

    public Cell AddCell(Cell cell)
    {
        cell.Id = _id++;
        _cells.Add(cell);
        return cell;
    }

    public bool DeleteCell (int id)
    {
        var cell = GetCellById(id);
        if (cell == null)
        {
            return false;
        }
        _cells.Remove(cell);
        return true;
    }

    public bool UpdateCell(Cell updatedCell)
    {
        var cell = GetCellById(updatedCell.Id);
        if (cell == null)
        {
            return false;
        }
        cell.Product = updatedCell.Product;
        cell.Quantity = updatedCell.Quantity;
        return true;
    }
}
