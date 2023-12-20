using System.Collections.Generic;
using UnityEngine;

public class Polygon : MonoBehaviour
{
    public bool isOverlap;
    
    public Vector2[] worldVertices;
    private Vector2[] vertices;

    private void Start()
    {
        // 获取精灵的物理形状轮廓
        var path = new List<Vector2>();
        GetComponent<SpriteRenderer>().sprite.GetPhysicsShape(0, path);

        // 存储本地坐标
        vertices = path.ToArray();
        worldVertices = new Vector2[vertices.Length];
    }

    private void Update()
    {
        // 顶点坐标转换为世界坐标
        for (var i = 0; i < vertices.Length; i++)
        {
            worldVertices[i] = transform.TransformPoint(vertices[i]);
        }

        SAT.Draw(worldVertices, isOverlap);
    }
}