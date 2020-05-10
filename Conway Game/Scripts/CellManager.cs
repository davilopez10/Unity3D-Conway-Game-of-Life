using UnityEngine;

public class CellManager : MonoBehaviour
{
    public bool IsPause { get; set; }

    [SerializeField]
    private int rows = 99;

    [SerializeField]
    private int columns = 99;

    [SerializeField]
    private Vector2 separationCell;

    [SerializeField]
    private Cell cellPrefab;

    private Cell[,] cells;

    [SerializeField]
    private float updatePeriod = 1f;

    private float count;

    public void CreateCells()
    {
        cells = new Cell[rows, columns];

        Cell auxCell;
        Vector3 auxCellPos = Vector3.zero;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                auxCell = Instantiate(cellPrefab, transform);
                auxCellPos.Set(i + i * separationCell.x, j + j * separationCell.y, 0);
                auxCell.transform.localPosition = auxCellPos;
                cells[i, j] = auxCell;
            }
        }
    }

    private void UpdateCells()
    {
        int[,] newCells = new int[rows, columns];

        int liveNb = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                liveNb = 8 - GetDieNeightbours(i, j);

                if (cells[i, j].IsDie == false)
                {
                    //Rule 1 and Rule 3
                    if (liveNb < 2 || liveNb > 3)
                        newCells[i, j] = 0;
                    //Rule 2
                    if (liveNb == 2 || liveNb == 3)
                        newCells[i, j] = 1;
                }
                else if (liveNb == 3)
                {
                    newCells[i, j] = 1;
                }
            }
        }

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                cells[i, j].IsDie = newCells[i, j] == 0;
            }
        }
    }

    private int GetDieNeightbours(int i, int j)
    {
        int die = 0;
        int rightNb = i + 1;
        int leftNb = i - 1;
        int topNb = j + 1;
        int bottonNb = j - 1;

        //Top
        die += topNb >= columns ? 1 : cells[i, topNb].IsDie ? 1 : 0;

        //Botton
        die += bottonNb < 0 ? 1 : cells[i, bottonNb].IsDie ? 1 : 0;

        //Right
        die += rightNb >= columns ? 1 : cells[rightNb, j].IsDie ? 1 : 0;

        //Left
        die += leftNb < 0 ? 1 : cells[leftNb, j].IsDie ? 1 : 0;

        //Top Right
        die += (topNb >= columns || rightNb >= columns) ? 1 : cells[rightNb, topNb].IsDie ? 1 : 0;

        //Top Left
        die += (topNb >= columns || leftNb < 0) ? 1 : cells[leftNb, topNb].IsDie ? 1 : 0;

        //Botton Right
        die += (bottonNb < 0 || rightNb >= columns) ? 1 : cells[rightNb, bottonNb].IsDie ? 1 : 0;

        //Botton Left
        die += (bottonNb < 0 || leftNb < 0) ? 1 : cells[leftNb, bottonNb].IsDie ? 1 : 0;

        return die;
    }

    private void Awake()
    {
        CreateCells();
        IsPause = true;
    }

    private void Update()
    {
        if (IsPause == false)
        {
            count += Time.deltaTime;
            if (count >= updatePeriod)
            {
                UpdateCells();
                count = 0;
            }
        }
    }
}
