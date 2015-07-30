using UnityEngine;
using System.Collections;

public class BeamControl : MonoBehaviour
{
    public Transform parrentTransform;
    public Transform endPoint;
    public Vector3 direction;
    public LineRenderer laserLine;
    public LayerMask mask;

    private RaycastHit hitInfo;

    void Start()
    {
        laserLine.SetWidth(0.2f, 0.2f);
        direction = Vector3.right;
    }

    void Update()
    {
        traceLine();
        //detectReflexion();
        //detectObstacle();
    }

    private void traceLine()
    {
        Debug.DrawRay(parrentTransform.position, direction, Color.red);

        //if (Physics.Raycast(parrentTransform.position, comingVector, out hit, mask))
        if (Physics.Raycast(parrentTransform.position, direction, out hitInfo))
        {
            laserLine.SetVertexCount(2);
            laserLine.SetPosition(0, parrentTransform.position);
            laserLine.SetPosition(1, hitInfo.point);

            if (true)
            {
                Vector3 incomingVector = hitInfo.point - parrentTransform.position;
                Vector3 reflectVector = Vector3.Reflect(incomingVector, hitInfo.normal);
                Debug.DrawRay(hitInfo.point, reflectVector, Color.green);
                laserLine.SetVertexCount(3);
                laserLine.SetPosition(1, hitInfo.point);
                laserLine.SetPosition(2, hitInfo.point + 2 * reflectVector);
            }
            return;
        }

        laserLine.SetVertexCount(2);
        laserLine.SetPosition(0, parrentTransform.position);
        laserLine.SetPosition(1, parrentTransform.position + 20 * direction);
    }

    private void detectReflexion()
    {
        Vector3 comingVector = endPoint.position - parrentTransform.position;

        //Debug.DrawLine(parrentTransform.position, endPoint.position, Color.red);
        Debug.DrawRay(parrentTransform.position, direction, Color.red);
        laserLine.SetPosition(0, parrentTransform.position);
        laserLine.SetPosition(1, endPoint.position);

        //if (Physics.Raycast(parrentTransform.position, comingVector, out hit, mask))
        if (Physics.Raycast(parrentTransform.position, comingVector, out hitInfo))
        {
            Vector3 incomingVector = hitInfo.point - parrentTransform.position;
            Vector3 reflectVector = Vector3.Reflect(incomingVector, hitInfo.normal);
            Debug.DrawRay(hitInfo.point, reflectVector, Color.green);
            laserLine.SetVertexCount(3);
            laserLine.SetPosition(1, hitInfo.point);
            laserLine.SetPosition(2, hitInfo.point + 2 * reflectVector);
        }
    }

    private void detectObstacle()
    {
        Vector3 comingVector = endPoint.position - parrentTransform.position;

        Debug.DrawLine(parrentTransform.position, endPoint.position, Color.red);


        //if (Physics.Raycast(parrentTransform.position, comingVector, out hit, mask))
        if (Physics.Raycast(parrentTransform.position, comingVector, out hitInfo))
        {
            laserLine.SetPosition(0, parrentTransform.position);
            laserLine.SetPosition(1, hitInfo.point);
            //laserLine.SetPosition(2, hitInfo.point);
            laserLine.SetVertexCount(2);
            //Vector3 incomingVector = hitInfo.point - parrentTransform.position;
            //Vector3 reflectVector = Vector3.Reflect(incomingVector, hitInfo.normal);
            //Debug.DrawRay(hitInfo.point, reflectVector, Color.green);

            //laserLine.SetPosition(1, hitInfo.point);
            //laserLine.SetPosition(2, hitInfo.point + 2 * reflectVector);
        }
    }
}
