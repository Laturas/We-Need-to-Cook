using UnityEngine;

public class WorkOrder
{
    public Chemical chemical;
    public int worth;
    public Document associated_doc;
    public float time_to_complete;

    public WorkOrder(Chemical chemical, int worth, Document associated_doc, float time_to_complete) {
        this.chemical = chemical;
        this.worth = worth;
        this.associated_doc = associated_doc;
        this.time_to_complete = time_to_complete;
    }
}
