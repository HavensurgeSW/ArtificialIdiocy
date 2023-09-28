using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public NodeType nodeType;

    public GridNode(GameObject prefab, int gridX, int gridY)
    {
        this.prefab = prefab;
        this.gridX = gridX;
        this.gridY = gridY;

    }

    public void Start()
    {
        nodeType = NodeType.Path;
    }

    Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y);
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y) { 
        x = Mathf.FloorToInt(worldPosition.x);
        y = Mathf.FloorToInt(worldPosition.y);
    }
}

public enum NodeType { 
    Path,
    Field,
    Building,
    Resource
}
