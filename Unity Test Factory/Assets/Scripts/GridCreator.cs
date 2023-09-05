using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    [SerializeField]
    private int xSize;
    [SerializeField]
    private int zSize;
    [SerializeField]
    private float cellSize;
    [SerializeField]
    private Vector3 center;
    [SerializeField]
    GenericGrid<GridObject> genericGrid;
    // Start is called before the first frame update
    void Awake()
    {
        genericGrid = new GenericGrid<GridObject>(xSize, zSize, cellSize, center, (int x, int z, GenericGrid<GridObject> grid)=>new GridObject(x,z,grid));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos(){
        if (genericGrid != null)
        genericGrid.DrawDebug();
    }
}

public class GridObject{
    int x;
    int z;
    GameObject plane;
    public GridObject(int x, int z, GenericGrid<GridObject> grid){
        this.x = x;
        this.z = z;

        plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.localScale = plane.transform.localScale / 10 * grid.GetCellSize();
        plane.transform.position = grid.GetCenterWorldCoords(x, z) - new Vector3(0, 10, 0);
        plane.GetComponent<Renderer>().material.color = Color.black;
    }
    public override string ToString()
    {
        return "x: " + x + ", z: " + z;
    }
}
