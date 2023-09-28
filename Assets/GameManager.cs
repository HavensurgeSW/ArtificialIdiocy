using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEditor.Experimental.GraphView;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField wInput;
    [SerializeField] private TMP_InputField hInput;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject gameUI;
    HSSGrid<int> grid;

    [Header("Grid")]
    GridNode[,] gridArray;
    [SerializeField]GameObject nodePrefab;


    private void Start()
    {
        gridArray = null;
        menuUI.SetActive(true);
        gameUI.SetActive(false);
        
    }

    public void StartGame() {
        if (string.IsNullOrEmpty(wInput.text) || string.IsNullOrEmpty(hInput.text)) {
            CreateGrid(5,5);
        }

       CreateGrid(int.Parse(wInput.text), int.Parse(hInput.text));


        menuUI.SetActive(false);
        gameUI.SetActive(true);

    }

    void CreateGrid(int w, int h) {
        gridArray = new GridNode[w,h];

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                Vector3 worldPosition = new Vector3(x, 0, y);
                GameObject nodeObject = Instantiate(nodePrefab, worldPosition, Quaternion.identity);
                GridNode node = nodeObject.GetComponent<GridNode>();
                node.worldPosition = worldPosition;
                node.gridX = x;
                node.gridY = y;
                gridArray[x, y] = node;
            }
        }
    }


    private void Update()
    {
        
    }


}
