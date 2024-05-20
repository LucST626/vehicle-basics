using UnityEngine;
using System.Collections;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] private Transform pilar; // Referencia al pilar (malla)
    [SerializeField] private Transform fromPosition; // Punto de referencia FROM
    [SerializeField] private Transform toPosition; // Punto de referencia TO
    [SerializeField] private float speed = 2f; // Velocidad de movimiento
    [SerializeField] private float waitTime = 1f; // Tiempo de espera en cada extremo

    private void Start()
    {
        StartCoroutine(MovePillar());
    }

    private IEnumerator MovePillar()
    {
        while (true)
        {
            yield return StartCoroutine(MoveToPosition(toPosition.position));
            yield return new WaitForSeconds(waitTime);
            yield return StartCoroutine(MoveToPosition(fromPosition.position));
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(pilar.position, targetPosition) > 0.01f)
        {
            pilar.position = Vector3.MoveTowards(pilar.position, targetPosition, speed * Time.deltaTime);
            yield return null; 
        }
        pilar.position = targetPosition;
    }
}