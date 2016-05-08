using UnityEngine;
using System.Collections;

public class CameraTest : MonoBehaviour
{
    public GameObject target;
    public GameObject cameraPoint;
    public bool flag;
    public bool UpArrowFlag;

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


        if (Input.GetAxis("Vertical") > 0) UpArrowFlag = true;
        else if (Input.GetAxis("Vertical") < 0) UpArrowFlag = false;

        if (target.GetComponent<PlayerMove>().GetLockOnInfo() == true) //ロックしてるとき
        {
            if (Input.GetAxis("Horizontal") != 0 && UpArrowFlag == true) flag = true;
            if (transform.rotation == target.transform.rotation || UpArrowFlag == false)
            {
                flag = false;
            }

            Vector3 targetRotation = targetTransform.rotation.eulerAngles;
            if (flag == true) //フラグがtrueだったらプレイヤーの後ろに回る
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetTransform.rotation, 2.0f * Time.deltaTime);
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.Euler(targetRotation.x, targetRotation.y + 180, targetRotation.z)
                    , 2.0f * Time.deltaTime);
            }
        }
        else
        {
            if (Input.GetAxis("Horizontal") != 0) flag = true;
            if (transform.rotation == target.transform.rotation || (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") == 0))
            {
                flag = false;
            }

            if (flag == true)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetTransform.rotation, 2.0f * Time.deltaTime);
            }
        }

        //ターゲットからカメラポイントの間に障害物がなければカメラをカメラポイントに移動する
        //障害物があったら障害物より前に移動する
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

    public void LockStart()
    {
        flag = true;
    }
}
