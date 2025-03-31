using UnityEngine;

public class DispenserSpot : MonoBehaviour, IHolder
{
    Holdable heldObject;
    public Holdable getSubscribedObject()
    {
        return heldObject;
    }

    public Transform getTransform()
    {
        return transform;
    }

    public bool SubscribeObject(Holdable holdable)
    {
        if (holdable == null) {
            return false;
        }
        if (heldObject != null) {
            return false;
        }
        heldObject = holdable;
        return true;
    }

    public Holdable UnsubscribeObject()
    {
        Holdable cp = heldObject;
        heldObject = null;
        return cp;
    }
}
