using UnityEngine;

public class Example : MonoBehaviour
{
    public Polygon[] polygons;

    private void Update()
    {
        for (int i = 0; i < polygons.Length; i++)
        {
            for (int j = 0; j < polygons.Length; j++)
            {
                if (i == j)
                {
                    continue;
                }
                
                polygons[i].isOverlap = SAT.IsOverlap(polygons[i], polygons[j]);
                if (polygons[i].isOverlap)
                {
                    break;
                }
            }
        }
    }
}