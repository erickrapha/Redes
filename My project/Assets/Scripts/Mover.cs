using UnityEngine;

public class Mover : MonoBehaviour
{   [SerializeField]
    private float speed;
    public Vector3 targetPosition;
    
    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}
