using System;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    public Transform Cam;
    public Transform playerTransform;
    [SerializeField] private float offsety;
    [SerializeField] private float offset;
    [SerializeField] private float smoothness;
    private void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(playerTransform.position.x + offset, playerTransform.position.y + offsety, -10f);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
    }

    public void CameraShake()
    {
        float xValue = 0.2f;
        float yValue = 0.2f;
        Cam.transform.position = new Vector3(Cam.transform.position.x + xValue , Cam.transform.position.y + yValue , smoothness * Time.deltaTime);
    }
}
