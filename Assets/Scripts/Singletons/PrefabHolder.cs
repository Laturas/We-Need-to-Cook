using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabHolder : MonoBehaviour
{
    public static PrefabHolder instance;
    public GameObject glassCup;
    void Awake() => instance = this;
}
