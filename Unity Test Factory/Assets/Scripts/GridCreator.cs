using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GridCreator : MonoBehaviour
{
    [SerializeField] private int xSize;
    [SerializeField] private int zSize;
    [SerializeField] private float cellSize;
    [SerializeField] private Vector3 center;
    [SerializeField] GenericGrid<GridObject> grid;
    public CinemachineVirtualCamera virtualCamera;
    void Awake()
    {
        grid = new GenericGrid<GridObject>(xSize, zSize, cellSize, center, (int x, int z, GenericGrid<GridObject> grid)=>new GridObject(x,z,grid));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)){
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 planeIntersectCoords = GetRayHitPosXZPlane(ray, 0);
            grid.GetXZ(out int x, out int z, planeIntersectCoords);
            GridObject gridObject = grid.GetGridObject(x, z);
            if (gridObject != null){
                GameObject visual = GameObject.CreatePrimitive(PrimitiveType.Cube);
                gridObject.SetVisual(visual);
            }
        }
        if (Input.GetKeyDown(KeyCode.E)){
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 planeIntersectCoords = GetRayHitPosXZPlane(ray, 0);
            grid.GetXZ(out int x, out int z, planeIntersectCoords);
            GridObject gridObject = grid.GetGridObject(x, z);
            if (gridObject != null){
                GameObject visual = GameObject.CreatePrimitive(PrimitiveType.Cube);
                gridObject.SetVisual(visual);
            }
        }
    }

    private Vector3 GetRayHitPosXZPlane(Ray ray, float yHeight){
        Vector3 origin = ray.origin;
        Vector3 direction = ray.direction;
        float travelMultiplier = -origin.y / direction.y;
        return new Vector3(origin.x + travelMultiplier * direction.x, yHeight, origin.z + travelMultiplier * direction.z);
    }
    private void OnDrawGizmos(){
        if (grid != null)
        grid.DrawDebug();
    }
}

public class GridObject{
    private int x;
    private int z;
    private GameObject plane;
    private GameObject visual;
    private GenericGrid<GridObject> grid;
    public GridObject(int x, int z, GenericGrid<GridObject> grid){
        this.x = x;
        this.z = z;
        this.grid = grid;

        plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.localScale = plane.transform.localScale / 10 * grid.GetCellSize();
        plane.transform.position = grid.GetCenterWorldCoords(x, z) + new Vector3(0, 0, 0);
        plane.GetComponent<Renderer>().material.color = Color.black;
    }
    public override string ToString()
    {
        return "x: " + x + ", z: " + z;
    }
    public void SetVisual(GameObject visual){
        GameObject.Destroy(this.visual);
        this.visual = visual;
        visual.transform.position = grid.GetCenterWorldCoords(x, z);
    }
}

public enum TileType{
    terrain,
    building,
    resource
}