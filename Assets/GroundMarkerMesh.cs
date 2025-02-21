using UnityEngine;

public class GroundMarkerMesh : MonoBehaviour
{
    private Mesh mesh;  // Holds the marker's mesh
    private Material material;  // Material for rendering the mesh

    [Header("Marker Settings")]
    public float radius = 1f; // Radius of the circle
    public int segments = 36;   // Number of segments to create the circle

    [Header("Pulsing Effect")]
    public float pulseSpeed = 2f;  // Speed of the pulsing effect
    public float pulseScale = 0.05f; // Intensity of the pulsing effect

    void Awake()
    {
        // Initialize MeshFilter and MeshRenderer components
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        mesh = new Mesh();
        meshFilter.mesh = mesh;

        // Set up the material with transparency
        material = new Material(Shader.Find("Standard"));
        material.color = new Color(0, 1, 0, 0.3f); // Semi-transparent green
        material.SetFloat("_Mode", 3); // Enable transparency in the shader
        material.EnableKeyword("_ALPHABLEND_ON");
        material.renderQueue = 3000; // Ensure it renders after opaque objects
        material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
        meshRenderer.material = material;
        

        // Generate the circular mesh
        CreateCircleMesh();
    }

    void Update()
    {
        // Add a pulsing effect to the marker
        float scale = 0.5f + Mathf.Sin(Time.time * pulseSpeed) * pulseScale;
        transform.localScale = new Vector3(scale, 0.5f, scale);
    }

    private void CreateCircleMesh()
    {
        // Generate vertices and triangles for the circle
        Vector3[] vertices = new Vector3[segments + 1]; // +1 for the center vertex
        int[] triangles = new int[segments * 3]; // Each segment has 3 vertices

        // Center vertex at the origin
        vertices[0] = Vector3.zero;

        // Create the vertices around the circle
        for (int i = 0; i < segments; i++)
        {
            float angle = Mathf.Deg2Rad * (360f / segments) * i;
            vertices[i + 1] = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
        }

        // Create the triangles
        for (int i = 0; i < segments; i++)
        {
            triangles[i * 3] = 0; // Center vertex
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = (i + 2 > segments) ? 1 : i + 2; // Wrap around at the end
        }

        // Assign the vertices and triangles to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals(); // Recalculate for proper lighting
        Debug.Log("Created mesh with " + mesh.vertexCount + " vertices.");
    }
}
