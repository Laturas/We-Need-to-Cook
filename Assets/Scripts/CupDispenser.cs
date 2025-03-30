using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupDispenser : MonoBehaviour
{
    public Animator thisAnimator;
    private bool isDispensing;
    private Transform dispensedCupTransform;
    public float distanceTrigger;
    public Transform dispensePosition;

    // Start is called before the first frame update
    void Start()
    {
        thisAnimator = GetComponent<Animator>();
        isDispensing = true;
        thisAnimator.SetTrigger("Dispense");
    }

    public void FixedUpdate() {
        if (
            !isDispensing 
            && dispensedCupTransform != null 
            && Vector3.Distance(dispensedCupTransform.position, dispensePosition.position) > distanceTrigger
        ) {
            thisAnimator.SetTrigger("Dispense");
            isDispensing = true;
        }
    }

    public void DispenseCup() {
        dispensedCupTransform = Instantiate(
            PrefabHolder.instance.glassCup,
            dispensePosition.position,
            Quaternion.identity)
        .transform;
    }

    public void EndDispense() {
        isDispensing = false;
    }
}
