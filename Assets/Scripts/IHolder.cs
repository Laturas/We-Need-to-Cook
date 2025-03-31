using UnityEngine;

public interface IHolder
{
    Holdable getSubscribedObject();

    /**
    * Returns the transform of this object and subscribes the holdable object to this
    */
    bool SubscribeObject(Holdable holdable);
    Holdable UnsubscribeObject();
    Transform getTransform();
}
