using UnityEngine;
using System.Collections;

public class CameraTest : MonoBehaviour
{
    public GameObject target;
    public GameObject cameraPoint;
    public bool flag;

    private Transform cameraTransform;
    private Transform playerTransform;

    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, 3.0f * Time.deltaTime);

        if (Input.GetAxis("Horizontal") == 1 || Input.GetAxis("Horizontal") == -1)
        {
            flag = true;
        }

        if (flag == true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target.transform.rotation, 2.0f * Time.deltaTime);
        }

        if (transform.rotation == target.transform.rotation || (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") == 0))
        {
            flag = false;
        }

        Vector3 rayDirection = playerTransform.position - cameraTransform.position;
        Ray ray = new Ray(cameraTransform.position, rayDirection);
        RaycastHit hitInfo;
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

        if (Physics.Raycast(ray, out hitInfo) && (hitInfo.collider.tag != "Player"))
        {
            print("RayHit");
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, target.transform.position, 3.0f * Time.deltaTime);
        }
    }
}
