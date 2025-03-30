using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispenserSpot : MonoBehaviour, IHolder
{
    Holdable heldObject;
    public Holdable getSubscribedObject()
    {
        return heldObject;
    }

    public bool SubscribeObject(Holdable holdable)
    {
        if ()
        heldObject = holdable;
        return true;
    }

    public void UnsubscribeObject(Holdable holdable)
    {
        throw new System.NotImplementedException();
    }
}
