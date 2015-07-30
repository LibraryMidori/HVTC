using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour
{
    /// <summary>
    /// Flag to control the object can be drag or not
    /// </summary>
    public bool isDragable = true;

    /// <summary>
    /// Temporary variable to improving efficience purpose.
    /// </summary>
    private Vector3 curScreenPos;
    private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown()
    {
        if (isDragable)
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            setCurrentScreenPoint(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(curScreenPos);
        }
    }

    void OnMouseDrag()
    {
        if (isDragable)
        {
            setCurrentScreenPoint(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPos) + offset;
            transform.position = curPosition;
        }
    }

    /// <summary>
    /// Improving the efficience by reducing the new operating in Vector3 objects.
    /// </summary>
    /// <param name="x">x coordinate value</param>
    /// <param name="y">y coordinate value</param>
    /// <param name="z">z coordinate value</param>
    private void setCurrentScreenPoint(float x, float y, float z)
    {
        curScreenPos.x = x;
        curScreenPos.y = y;
        curScreenPos.z = z;
    }
}
