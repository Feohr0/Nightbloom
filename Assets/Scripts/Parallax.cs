using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float starPos;
    public GameObject cam;
    public float parallaxAmount;

    private void Start()
    {
        starPos = transform.position.x;
    }

    private void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxAmount;

        transform.position = new Vector3(starPos + distance , transform.position.y , transform.position.z);
    }
}
