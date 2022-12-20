using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRcodeManager : MonoBehaviour
{
    [SerializeField] private Texture _texture = null;

    // Start is called before the first frame update
    void Start()
    {
        ShowQRCode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowQRCode()
    {
        Vector3[] vertices = new Vector3[]
{
            new Vector3(-1f, 1f, -1f),
            new Vector3(1f, 1f, -1f),
            new Vector3(1f, -1f, -1f),
            new Vector3(-1f, -1f, -1f),
};

        int[] triangles = new int[] { 0, 1, 2, 0, 2, 3 };
        Mesh mesh = new Mesh();
        Vector2[] uvs = new Vector2[] {
            new Vector2(0f, 1f),
            new Vector2(1f,1f),
            new Vector2(1f, 0f),
            new Vector2(0f,0f),
           };


        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().sharedMesh = mesh;

      // Material material = new Material(Shader.Find("Standard"));
      // material.SetTexture("_MainTex", _texture);
      // GetComponent<MeshRenderer>().material = material;
        GetComponent<MeshRenderer>().receiveShadows = false; // 그림자 받지 않도록 
    }


}
