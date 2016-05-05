using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Generate world tiles and rows
/// </summary>
public class WorldGenerator : MonoBehaviour {

    /// <summary>
    /// Block prefab.
    /// TODO: maybe moved to the Resources dir later.
    /// </summary>
    public GameObject BoxPrefab;
    private PoolManager _poolManager;

    private PoolOfObjects _blocksPool;
    private PoolOfObjects _rowPool;
    private Vector3 StartPosition;
    public int PreloadedRows = 5;
    public float RowInterval;
    private float _nextY;


    string InputFileName = "WorldRowPatterns.txt";
    List<int[]> _rows;

    void Awake()
    {
        GameEvents.OnCameraRowEnter += CreateRandomRow;
    }

    void Start () {
        _poolManager = GameObject.FindObjectOfType<PoolManager>();
        _blocksPool = _poolManager.GetPool(PoolTypesEnum.Block);
        _rowPool = _poolManager.GetPool(PoolTypesEnum.Row);
        _rows = new List<int[]>();

        // read file
        InputFileName = Application.dataPath + "/" + InputFileName;
        using ( StreamReader reader = File.OpenText(InputFileName) )
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (null == line)
                    continue;

                int[] rowArray = new int[line.Length];
                for (int i = 0; i < line.Length; i++)
                {
                    rowArray[i] = int.Parse(line[i].ToString());
                }
                _rows.Add(rowArray); 
            }

        CreateNewLevel();
        GameEvents.OnGameRestart += StartNewGame;
    }
	
 

    public void StartNewGame()
    {
        _rowPool.ReturnAll();
        _blocksPool.ReturnAll();
        CreateNewLevel();
    }

    void CreateNewLevel()
    {
        _nextY = 2 - Camera.main.orthographicSize + RowInterval;
        StartPosition = new Vector3(Camera.main.transform.position.x - Camera.main.orthographicSize,
            _nextY,
            0); 

        CreateRow(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 }); // TODO: temp 
        for (int i = 0; i < PreloadedRows; i++)
            CreateRandomRow();

        Debug.Log(" StartPosition: " + StartPosition.ToString());
    }

    private int prevRandIndex;

    public void CreateRandomRow()
    {
        int index = Random.Range(0, _rows.Count); 
        if (prevRandIndex == index) // skip the same indexes results
        { 
            index = (index + 1 < _rows.Count) ? index + 1 : // if there is greater number take that
                ((index - 1 >= 0) ? index - 1 : // else if there lesser number take that
                index);  // otherwise use index that we ha
        } 
        prevRandIndex = index;

        CreateRow(_rows[index]); 
    }

    public void CreateRow(int[] data)
    {
        GameObject row = _rowPool.Get();
        row.transform.position = new Vector3(StartPosition.x, StartPosition.y + _nextY + BoxPrefab.transform.localScale.y, 0);
        row.transform.rotation = Quaternion.identity; 

        for (int i = 0; i < data.Length; i++)
        { 
            if (data[i] == (int) PoolTypesEnum.Block)
            { 
                float offsetX = BoxPrefab.transform.localScale.x * i; 
                var GO = _blocksPool.Get();
                GO.transform.parent = row.transform;
                GO.transform.localPosition = new Vector3(offsetX, 0, 0);
                GO.transform.rotation = Quaternion.identity;
                GO.SetActive(true);
            }
        }
        _nextY += RowInterval;
    }
}
