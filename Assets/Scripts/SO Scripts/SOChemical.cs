using UnityEngine;
using System.Collections.Generic;

public enum Chemical {
    Water,
    TestChemical,
    TestCompound,
}

[CreateAssetMenu(fileName = "Chemical", menuName = "Scriptable Objects/Chemical")]
public class SOChemical : ScriptableObject {
    public bool isBasicChemical;
    public List<Chemical> neededChemicals;
    public List<int> percentMixtures;
    public List<int> percentTolerance;
    public float cookTime;
}