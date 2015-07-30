using UnityEngine;
using System.Collections;

public class BeamControl : MonoBehaviour
{
    public Transform startPoint;
    public Vector3 direction;
    public LineRenderer laserLine;
    public LayerMask mask;
    public float laserWidth = 0.2f;
    public Vector3 offsetVector = new Vector3(0f, 0f, 0f);

    private GameObject reflexGameObj = null;
    private RaycastHit hitInfo;

    void Start()
    {
        startPoint = transform;
        laserLine.SetWidth(laserWidth, laserWidth);
        direction = Vector3.right;
    }

    void Update()
    {
        traceLine();
    }

    private void traceLine()
    {
        Debug.DrawRay(startPoint.position, direction, Color.red);

        //if (Physics.Raycast(parrentTransform.position, comingVector, out hit, mask))
        if (Physics.Raycast(startPoint.position, direction, out hitInfo))
        {
            laserLine.SetVertexCount(2);
            laserLine.SetPosition(0, startPoint.position + offsetVector);
            laserLine.SetPosition(1, hitInfo.point);

            // When hits the mirror
            if (true)
            {
                delegateTheReflex(hitInfo.transform.FindChild("LightBeam").gameObject, hitInfo.point, hitInfo.transform);
            }
            return;
        }

        // The light doesn't hit anything
        drawLongLight();
    }

    private void delegateTheReflex(GameObject lightObj, Vector3 newStartPoint, Transform objTrans)
    {
        if (lightObj == null)
            return;

        Vector3 incomingVector = hitInfo.point - startPoint.position;
        Vector3 reflectVector = Vector3.Reflect(incomingVector, hitInfo.normal);
        Debug.DrawRay(hitInfo.point, reflectVector, Color.green);

        reflexGameObj = lightObj;
        BeamControl reflexComponent = reflexGameObj.GetComponent<BeamControl>();
        reflexComponent.direction = reflectVector;
        reflexComponent.startPoint = objTrans;
        reflexComponent.offsetVector = newStartPoint - objTrans.position;
        reflexGameObj.SetActive(true);
    }

    /// <summary>
    /// Draw very long light
    /// </summary>
    private void drawLongLight()
    {
        laserLine.SetVertexCount(2);
        laserLine.SetPosition(0, startPoint.position + offsetVector);
        laserLine.SetPosition(1, startPoint.position + 20 * direction);

        notifyToReflexObj();
    }

    /// <summary>
    /// Notify the reflex object when the light doesn't hit anything
    /// </summary>
    public void notify()
    {
        notifyToReflexObj();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Concrete method to notify to reflex object
    /// </summary>
    private void notifyToReflexObj()
    {
        if (reflexGameObj == null)
            return;

        if (reflexGameObj.GetComponent<BeamControl>().reflexGameObj != null)
        {
            if (gameObject.Equals(reflexGameObj.GetComponent<BeamControl>().reflexGameObj))
            {
                Debug.Log("Return");
                return;
            }
        }

        reflexGameObj.GetComponent<BeamControl>().notify();
    }
}
