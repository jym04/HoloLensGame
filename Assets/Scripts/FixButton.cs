using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using Microsoft.MixedReality.Toolkit.UI;

public class FixButton : MonoBehaviour
{
    public SolverHandler solverHandler;

    public ObjectManipulator objectManipulator;

    public GameObject fixOnButton;
    public GameObject fixOffButton;

    public void FixOnButtonOnClick()
    {
        solverHandler.UpdateSolvers = false;
        objectManipulator.enabled = true;
        fixOnButton.SetActive(false);
        fixOffButton.SetActive(true);
    }
    public void FixedOffButtonOnClick()
    {
        solverHandler.UpdateSolvers = true;
        objectManipulator.enabled = false;
        fixOnButton.SetActive(true);
        fixOffButton.SetActive(false);
    }
}
