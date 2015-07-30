using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    public LightControl source;

    void Start()
    {

    }

    void Update()
    {
        //updateSourceLight();
    }

    public void updateSourceLight()
    {
        source.traceLine();
    }
}
