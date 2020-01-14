using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerLaser : MonoBehaviour
{
    public float laserMaxLength = 5f;
    public Vector3 startposition;
    public Vector3 endposition;
    public bool laserHit = false;
    public GameObject otherLaser;
    

    GameObject obj;
    Renderer objRenderer;
    public Material newMat1;
    Material oldMat1;

    public LineRenderer laserLineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        laserLineRenderer.SetPositions(initLaserPositions);
        laserLineRenderer.SetWidth(0.01f, 0.01f);
        
    }

    // Update is called once per frame
    void Update()
    {
        ShootLaserFromTargetPosition(transform.position, transform.forward, laserMaxLength);
        laserLineRenderer.enabled = true;

        laserLineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        laserLineRenderer.SetColors(Color.red, Color.blue);


    }

    void ShootLaserFromTargetPosition(Vector3 targetPosition, Vector3 direction, float length)
    {
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + (length * direction);

        if (Physics.Raycast(ray, out raycastHit, length))
        {
            endPosition = raycastHit.point;
            
            if (raycastHit.collider.tag == "targetObject" && otherLaser.GetComponent<ControllerLaser>().laserHit == false) //This section controls if the material of the targeted object changes
            {

                laserHit = true;
                if (obj != raycastHit.collider.gameObject)
                {
                    
                    if (obj != null)
                    {
                        objRenderer.material = oldMat1;
                    }

                    obj = raycastHit.collider.gameObject;
                    objRenderer = obj.GetComponent<Renderer>();
                    oldMat1 = objRenderer.material;
                    objRenderer.material = newMat1;

                }
            }
        }
        else
        {
            if(obj != null)
            {
                laserHit = false;
                Debugger.debugText = oldMat1.ToString();
                objRenderer.material = oldMat1;
                obj = null;
            }
        }
       

        laserLineRenderer.SetPosition(0, targetPosition);
        laserLineRenderer.SetPosition(1, endPosition);

        startposition = targetPosition;
        endposition = endPosition;

    }

    
}
