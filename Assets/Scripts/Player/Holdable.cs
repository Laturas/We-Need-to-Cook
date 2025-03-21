using UnityEngine;

public class Holdable : MonoBehaviour
{
    protected bool selected = false;
    protected Vector3 gotoPos;
    protected Vector3 basePos;
    protected Transform gotoTransform;
    public float selection_pos_modifier = 0.05f;
    protected float active_select_mod;
    protected bool isHeld => gotoTransform != null;

    void Awake() => basePos = transform.position;

    public void Update() {
        if (gotoTransform != null) {
            basePos = gotoTransform.position;
        }
        gotoPos = basePos + new Vector3(0f, active_select_mod, 0f);
        transform.position = Vector3.Lerp(transform.position, gotoPos, Time.deltaTime * 5f);
    }

    /**
    * Triggered by clicking on the object. Should handle selection and deselection.
    */
    public void Select() {
        selected = true;
        active_select_mod = selection_pos_modifier;
    }

    public void Deselect() {
        selected = false;
        active_select_mod = 0f;
    }

    /**
    * Item should lerp to the hold 
    */
    public void GoTo(Transform position) {
        basePos = position.position;
        gotoTransform = position;
    }
}
