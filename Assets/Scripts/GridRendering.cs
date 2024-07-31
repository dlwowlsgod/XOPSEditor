using UnityEngine;
using UnityEngine.Rendering;

public class GridRendering : MonoBehaviour
{
    // 1 unit = 1 meter (0.01 = 1 cm (0.01m))
    public Color gridColor = new Color(1f, 0f, 0f, 0.8f);
    public float gridBoxSize = 1f;
    public float gridBoxCount = 1000f;
    private void Start()
    {
        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
    }
    
    private void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        GL.Begin(GL.LINES);
        Material material = new(Shader.Find("Hidden/Internal-Colored"));
        material.SetPass(0);
        GL.Color(gridColor);

        Vector3 startGridPosition = GetCurrentGridCenterCoordinate();
        float xs = startGridPosition.x - gridBoxCount, xe = startGridPosition.x + gridBoxCount;
        float zs = startGridPosition.z - gridBoxCount, ze = startGridPosition.z + gridBoxCount;
        
        for (float x = xs; x <= xe; x += gridBoxSize)
        {
            GL.Vertex3(x, 0, zs);
            GL.Vertex3(x, 0, ze);
        }

        for (float z = zs; z <= ze; z += gridBoxSize)
        {
            GL.Vertex3(xs, 0, z);
            GL.Vertex3(xe, 0, z);
        }

        GL.End(); 
    }

    private void OnDestroy()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
    }

    private Vector3 GetCurrentGridCenterCoordinate()
    {
        //한 칸당 1제곱미터
        Vector3Int floorVector = Vector3Int.FloorToInt(transform.position);
        Vector3 finalVector = floorVector;

        return finalVector;
    } 
}
