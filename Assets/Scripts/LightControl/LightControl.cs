using UnityEngine;
using System.Collections;

public class LightControl : MonoBehaviour
{
    public Transform startPoint;
    public Vector3 direction;
    public LineRenderer laserLine;
    public LayerMask mask;
    public float laserWidth = 0.2f;
    public Vector3 offsetVector = new Vector3(0f, 0f, 0f);
    public bool isSource = false;
    public bool isUpdate = true;

    private IList hitPoints = new ArrayList();
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
        if (isSource)
        {
            traceLine();
            Debug.DrawRay(startPoint.position, direction, Color.red);
            int i = 0;

            laserLine.SetVertexCount(hitPoints.Count);
            foreach (Vector3 point in hitPoints)
            {
                laserLine.SetPosition(i++, point);
            }
        }
    }

    public void setupInfo(Vector3 direction, Vector3 offsetVector)
    {
        this.direction = direction;
        this.offsetVector = offsetVector;
    }

    public void traceLine()
    {
        hitPoints = new ArrayList();
        hitPoints.Add(startPoint.position + offsetVector);
        traceLine(hitPoints);
    }

    protected void traceLine(IList hitList)
    {
        Debug.DrawRay(startPoint.position, direction, Color.red);

        if (Physics.Raycast(startPoint.position, direction, out hitInfo))
        {
            hitList.Add(hitInfo.point);

            // When hits the mirror
            if (true)
            {
                delegateTheReflex(hitInfo.transform.FindChild("LightBeam").gameObject, hitInfo.point, hitInfo.transform, hitList);
            }
            return;
        }
        // The light doesn't hit anything
        Debug.Log("Hit3");
        hitList.Add(startPoint.position + 20 * direction);
        Debug.Log("Hit4");
    }

    private void delegateTheReflex(GameObject lightObj, Vector3 newStartPoint, Transform objTrans, IList hitList)
    {
        if (lightObj == null)
            return;

        Vector3 incomingVector = hitInfo.point - startPoint.position;
        Vector3 reflectVector = Vector3.Reflect(incomingVector, hitInfo.normal);
        Debug.DrawRay(hitInfo.point, reflectVector, Color.green);

        reflexGameObj = lightObj;
        LightControl reflexComponent = reflexGameObj.GetComponent<LightControl>();
        //reflexComponent.direction = reflectVector;
        reflexComponent.startPoint = objTrans;
        //reflexComponent.offsetVector = newStartPoint - objTrans.position;
        reflexComponent.setupInfo(reflectVector, newStartPoint - objTrans.position);
        reflexGameObj.SetActive(true);
        reflexComponent.traceLine(hitList);
    }
}
