using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectHolder : MonoBehaviour
{
    public SOChemical[] chemicals;
    public SOMaterials materials;
    static public ScriptableObjectHolder instance;

    public void Awake() => instance = this;
}
