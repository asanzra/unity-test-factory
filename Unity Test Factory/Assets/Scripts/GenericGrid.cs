using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenericGrid<TGridObject>
{
    private int xSize;
    private int zSize;
    private float cellSize;
    private Vector3 origin;
    private TGridObject[] objects;
    public GenericGrid(int xSize, int zSize, float cellSize, Vector3 center, Func<int, int, GenericGrid<TGridObject>, TGridObject> CreateDefaultGridObject, bool debugText = true){
        if (xSize < 1 || zSize < 1 || cellSize < 2 * float.Epsilon) Debug.Log("Params not correct");

        this.xSize = Mathf.Max(1, xSize);
        this.zSize = Mathf.Max(1, zSize);
        this.cellSize = Mathf.Max(2 * float.Epsilon, cellSize);
        this.origin = center - new Vector3(xSize, 0, zSize) * cellSize / 2.0f;
        this.objects = new TGridObject[xSize * zSize];

        for (int x = 0; x < xSize; x++){
            for (int z = 0; z < zSize; z++){
                objects[ObjectIndex(x,z)] = CreateDefaultGridObject(x, z, this);
                if(debugText) {
                    TextMesh text = CreateWorldText(objects[ObjectIndex(x,z)].ToString(), null, GetCenterWorldCoords(x,z), (int)cellSize, null, TextAnchor.MiddleCenter, TextAlignment.Center);
                }
            }
        }
    }
    private int ObjectIndex(int x, int z){
        return x + z * xSize;
    }
    public float GetCellSize(){
        return cellSize;
    }
    public void DrawDebug(){
        // Draws a blue line from this transform to the target
        Gizmos.color = Color.white;
        for (int x = 0; x < xSize; x++){
            for (int z = 0; z < zSize; z++){
                Gizmos.DrawLine(GetCornerWorldCoords(x, z), GetCornerWorldCoords(x + 1, z));
                Gizmos.DrawLine(GetCornerWorldCoords(x, z), GetCornerWorldCoords(x, z + 1));
            }
        }
        for (int z = 0; z < zSize; z++){
            Gizmos.DrawLine(GetCornerWorldCoords(xSize, z), GetCornerWorldCoords(xSize, z + 1));
        }
        for (int x = 0; x < xSize; x++){
            Gizmos.DrawLine(GetCornerWorldCoords(x, zSize), GetCornerWorldCoords(x + 1, zSize));
        }
    }
    public Vector3 GetCornerWorldCoords(int x, int z){
        return origin + new Vector3(x, 0, z) * cellSize;
    }
    public Vector3 GetCenterWorldCoords(int x, int z){
        return GetCornerWorldCoords(x,z) + new Vector3(1,0,1) * cellSize / 2.0f;
    }





    public const int sortingOrderDefault = 5000;
    // Create Text in the World
    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = sortingOrderDefault) {
        if (color == null) color = Color.white;
        return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
    }
    
    // Create Text in the World
    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder) {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }
}
