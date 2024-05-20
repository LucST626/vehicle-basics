using UnityEngine;

public class CameraMov : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float followSmooth = 0.125f;
    [SerializeField] float rotationSmooth = 0.1f;

    Vector3 offset;
    Vector3 velocity = Vector3.zero;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followSmooth);

        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSmooth);
    }
}
