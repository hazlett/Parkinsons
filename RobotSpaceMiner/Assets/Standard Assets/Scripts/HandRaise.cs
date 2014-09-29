using UnityEngine;
using System.Collections;
using OhioState.Libraries.Gesture;

public class HandRaise : IGestureAction {
    private CubeGridCollider[] cubes;
    public HandRaise()
    {
        cubes = GameObject.Find("Cube Grid").GetComponentsInChildren<CubeGridCollider>();
    }
    public void Trigger(object data)
    {
        foreach (CubeGridCollider cube in cubes)
        {
            if (cube.onCount > 0)
                cube.Deactivate();
        }
    }
}
