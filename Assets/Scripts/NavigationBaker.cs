using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class NavigationBaker : MonoBehaviour
{
    public NavMeshSurface[] surfaces;


    public void Baked()
    {
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].useGeometry = UnityEngine.AI.NavMeshCollectGeometry.PhysicsColliders;
            surfaces[i].BuildNavMesh();
        }
    }
}
