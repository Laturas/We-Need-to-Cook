using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Materials", menuName = "Scriptable Objects/Materials")]
public class SOMaterials : ScriptableObject
{
    public List<Material> list;
}
