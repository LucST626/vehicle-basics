using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] List<Transform> spawPointPositions;
    [SerializeField] Vector3 up;
    int posicion;
    void Start()
    {
        posicion = Random.Range(0, spawPointPositions.Count);
        transform.position = new Vector3(spawPointPositions[posicion].position.x,
                spawPointPositions[posicion].position.y + up.y,
                spawPointPositions[posicion].position.z);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            posicion = Random.Range(0, spawPointPositions.Count);


            transform.position = new Vector3(spawPointPositions[posicion].position.x,
                spawPointPositions[posicion].position.y + up.y,
                spawPointPositions[posicion].position.z);
            print(posicion);
        }
    }
}
