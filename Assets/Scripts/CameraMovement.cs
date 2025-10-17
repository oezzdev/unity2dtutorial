using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Objective;

    // Update is called once per frame
    void Update()
    {
        if (Objective == null) return;

        Vector3 position = transform.position;
        position.x = Objective.transform.position.x;
        transform.position = position;
    }
}
