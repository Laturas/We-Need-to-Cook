using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    private Transform camTransform;
    public Transform[] translist;
    public int current_transform;

    void Start() {
        camTransform = cam.transform;
        current_transform = 0;
    }

    // Update is called once per frame
    void Update() {
        camTransform.position = Vector3.Lerp(camTransform.position, translist[current_transform].position, Time.deltaTime * 5);
        camTransform.rotation = Quaternion.Lerp(camTransform.rotation, translist[current_transform].rotation, Time.deltaTime * 5);
    }

    public void MoveRight() {
        current_transform = (current_transform + 1) % translist.Length;
    }

    public void MoveLeft() {
        current_transform = (current_transform - 1 + translist.Length) % translist.Length;
    }
}
