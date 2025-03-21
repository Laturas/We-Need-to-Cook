using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HoldablePaper : Holdable
{
    // Update is called once per frame
    public new void Update() {
        if (gotoTransform != null) {
            basePos = gotoTransform.position;
        }
        gotoPos = basePos + new Vector3(0f, active_select_mod, 0f);
        transform.position = Vector3.Lerp(transform.position, gotoPos, Time.deltaTime * 5f);

        if (isHeld) {
            Quaternion fwRotation = Quaternion.LookRotation(gotoTransform.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, fwRotation, Time.deltaTime * 5f);
        } else {
            Quaternion upRotation = Quaternion.LookRotation(-Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, upRotation, Time.deltaTime * 5f);
        }
    }
}
