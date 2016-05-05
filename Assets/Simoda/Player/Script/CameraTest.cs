using UnityEngine;
using System.Collections;

public class CameraTest : MonoBehaviour
{
    public GameObject target;
    public GameObject cameraPoint;
    public bool flag;

    private Transform cameraTransform;
    private Transform targetTransform;

    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        targetTransform = target.transform;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetTransform.position, 3.0f * Time.deltaTime);

        //if (Input.GetAxis("Horizontal") == 1 || Input.GetAxis("Horizontal") == -1)
        //{
        //    flag = true;
        //}
        if (Input.GetAxis("Horizontal") != 0) flag = true;

        if (flag == true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetTransform.rotation, 2.0f * Time.deltaTime);
        }

        if (transform.rotation == target.transform.rotation || (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") == 0))
        {
            flag = false;
        }


        Vector3 cameraPointDirection = cameraPoint.transform.position - targetTransform.position;
        Ray cameraPointRay = new Ray(targetTransform.position, cameraPointDirection);
        RaycastHit cameraPointRayHitInfo;
        Debug.DrawRay(cameraPointRay.origin, cameraPointRay.direction * 6.5f, Color.green);

        if (Physics.SphereCast(cameraPointRay, 0.5f, out cameraPointRayHitInfo, 6.5f))
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraPointRayHitInfo.point, 3.0f * Time.deltaTime);
        }
        else
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraPoint.transform.position, 3.0f * Time.deltaTime);
        }

        ////カメラからプレイヤーへのRay
        //Vector3 playerRayDirection = targetTransform.position - cameraTransform.position;
        //Ray playerRay = new Ray(cameraTransform.position, playerRayDirection);
        //RaycastHit playerRayHitInfo;
        //Debug.DrawRay(playerRay.origin, playerRay.direction * 10, Color.red);

        ////print(cameraTransform.position);

        ////カメラとプレイヤーの間に障害物があったらカメラを近づける
        //if (Physics.Raycast(playerRay, out playerRayHitInfo) && playerRayHitInfo.collider.tag != "Player")
        //{
        //    print("RayHit");
        //    cameraTransform.position = Vector3.Lerp(cameraTransform.position, target.transform.position, 3.0f * Time.deltaTime);
        //}

        ////else if (Physics.Raycast(playerRay, out playerRayHitInfo) && playerRayHitInfo.collider.tag == "Player")
        ////{
        ////    cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraPoint.transform.position, 3.0f * Time.deltaTime);
        ////}

        ////カメラからカメラの後ろへのRay + cameraTransform.forward * -1.0f
        //Ray cameraBackRay = new Ray(cameraTransform.position, playerRayDirection * -1.0f);
        //RaycastHit cameraBackRayHitInfo;
        //Debug.DrawRay(cameraBackRay.origin, cameraBackRay.direction * 1, Color.blue);

        //if (!Physics.Raycast(cameraBackRay, out cameraBackRayHitInfo, 1.0f) && !Physics.Raycast(playerRay, out cameraBackRayHitInfo, 1.0f, 8))
        //{
        //    cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraPoint.transform.position, 3.0f * Time.deltaTime);
        //}
    }
}
