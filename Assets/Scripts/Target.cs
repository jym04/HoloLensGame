using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public NavMeshAgentController navMeshAgentController;

    private void OnEnable()
    {
        navMeshAgentController.MakePath();
    }
}
