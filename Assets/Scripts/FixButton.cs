using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class FixButton : MonoBehaviour
{
    public SolverHandler solverHandler;

    public GameObject fixOnButton;
    public GameObject fixOffButton;

    public void FixOnButtonOnClick()
    {
        solverHandler.UpdateSolvers = false;
        fixOnButton.SetActive(false);
        fixOffButton.SetActive(true);
    }
    public void FixedOffButtonOnClick()
    {
        solverHandler.UpdateSolvers = true;
        fixOnButton.SetActive(true);
        fixOffButton.SetActive(false);
    }
}
