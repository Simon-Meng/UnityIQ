using System.Collections.Generic;
using UnityEngine;

public static class SAT
{
    /// 检查两个凸多边形是否重叠
    public static bool IsOverlap(Polygon polygonA, Polygon polygonB)
    {
        // 检查所有可能的分离轴
        return OverlapOnAllAxes(polygonA, polygonB) && OverlapOnAllAxes(polygonB, polygonA);
    }

    /// 获取多边形的所有分离轴
    private static IEnumerable<Vector2> GetAxes(Polygon polygon)
    {
        var axes = new List<Vector2>();
        var vertices = polygon.worldVertices;

        for (var i = 0; i < vertices.Length; i++)
        {
            // 取相邻的两个顶点
            var p1 = vertices[i];
            var p2 = vertices[i + 1 == vertices.Length ? 0 : i + 1];

            // 得到这两个顶点构成的边的向量
            var edge = p2 - p1;

            // 计算边的垂直向量，这将是一个可能的分离轴
            var normal = new Vector2(-edge.y, edge.x).normalized;
            axes.Add(normal);
        }

        return axes;
    }
    
    // private static IEnumerable<Vector2> GetAxes(Polygon polygon)
    // {
    //     var vertices = polygon.worldVertices;
    //
    //     for (var i = 0; i < vertices.Length; i++)
    //     {
    //         var p1 = vertices[i];
    //         var p2 = vertices[i + 1 == vertices.Length ? 0 : i + 1];
    //
    //         var edge = p2 - p1;
    //
    //         var normal = new Vector2(-edge.y, edge.x).normalized;
    //         yield return normal;
    //     }
    // }

    /// 计算多边形在给定轴上的投影
    private static void Project(Polygon polygon, Vector2 axis, out float min, out float max)
    {
        var vertices = polygon.worldVertices;

        // 记录最小和最大投影值
        min = Vector2.Dot(vertices[0], axis);
        max = min;

        // 遍历每个顶点来找到最小和最大的投影值
        for (var i = 1; i < vertices.Length; i++)
        {
            var projection = Vector2.Dot(vertices[i], axis);
            if (projection < min)
            {
                min = projection;
            }
            else if (projection > max)
            {
                max = projection;
            }
        }
    }

    /// 对特定的轴，检查两个多边形的投影是否重叠
    private static bool OverlapOnAxis(Polygon polygonA, Polygon polygonB, Vector2 axis)
    {
        // 获取在轴上的投影
        Project(polygonA, axis, out var minA, out var maxA);
        Project(polygonB, axis, out var minB, out var maxB);

        // 检查投影是否重叠
        return !(minB > maxA || minA > maxB);
    }

    /// 对所有轴检查两个多边形的投影是否重叠
    private static bool OverlapOnAllAxes(Polygon p, Polygon otherP)
    {
        var axes = GetAxes(p);

        // return axes.All(axis => OverlapOnAxis(p, otherP, axis));
        foreach (var axis in axes)
        {
            if (!OverlapOnAxis(p, otherP, axis))
            {
                return false;
            }
        }

        return true;
    }

    /// 绘制多边形边框
    public static void Draw(IList<Vector2> corners, bool isOverlap)
    {
        for (var i = 0; i < corners.Count; i++)
        {
            var start = new Vector3(corners[i].x, corners[i].y, 0);
            var end = new Vector3(corners[(i + 1) % corners.Count].x, corners[(i + 1) % corners.Count].y, 0);

            Debug.DrawLine(start, end, isOverlap ? Color.red : Color.green);
        }
    }
}