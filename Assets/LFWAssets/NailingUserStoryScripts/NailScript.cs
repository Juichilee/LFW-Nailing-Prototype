using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NailScript : MonoBehaviour
{
    //Debug variables and tools
    public GameObject testObject; // testObject
    public Text hammerDetectorText;
    public Text nailableObjectsText;

    public GameObject nailableDetector;
    public GameObject hammerDetector;

    public bool isHitByHammer;

    public int nailDistance;

    //Variables for storing gameobjects
    public List<GameObject> nailableObjectsList = new List<GameObject>();
    public static GameObject[] nailableObjects;
    public Collider[] nailableColliders;
    public Collider[] tempColliders = null;

    private void Start()
    {
        testObject.SetActive(false); // testObject
        hammerDetector.SetActive(false);
        nailableDetector.SetActive(false);
        isHitByHammer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GrabbedChecker() == true)
        {
            //hammerDetectorText.text = "Nail is being grabbed.";
            //Deactivate the Nailable Objects Detector and Hammer Detector.
            nailableDetector.SetActive(true);
            hammerDetector.SetActive(true);

            printNailableObjects();
            //Debugger.hammerDetectorText = isHitByHammer.ToString();
            if(isHitByHammer == true)
            {

            }
        }
        else
        {
            //hammerDetectorText.text = "Nail is not being grabbed.";
            //Deactivate the Nailable Objects Detector and Hammer Detector.
            nailableDetector.SetActive(false);
            hammerDetector.SetActive(false);
        }
    }


    //Checks if the nail is being grabbed by a controller.
    public bool GrabbedChecker()
    {
        if (this.gameObject.GetComponent<OVRGrabbable>().isGrabbed == false)
        {
            return false;
        }
        return true;
    }

    //Used by NailableDetectorScript and HammerDetectorScript
    public bool onChildTriggerEnter(Collider boxCollider, Collider other)
    {
        if(boxCollider.gameObject.name == "hammerDetector")
        {
            if(other == null)
            {         
                hammerDetectorText.text = "Trigger: " + boxCollider.gameObject.name + "--" + "Other: " + "null";
                isHitByHammer = false;
            }
            else
            {
                hammerDetectorText.text = "Trigger: " + boxCollider.gameObject.name + "--" + "Other: " + other.gameObject.name;
                isHitByHammer = true;
            }
        }else if(boxCollider.gameObject.name == "nailableObjectsDetector")
        {
            if(other != null)
            {
                if(other.gameObject.tag == "nailable")
                {
                    if (!nailableObjectsList.Contains(other.gameObject))
                    {
                        nailableObjectsList.Add(other.gameObject);
                        nailableObjects = nailableObjectsList.ToArray();
                    }
                }
            }
        }
        
        return false;
    }

    //Used by NailableDetectorScript
    public void onChildTriggerExit(Collider boxCollider, Collider other)
    {
        if(other.gameObject.tag == "nailable")
        {
            for(int i = 0; i < nailableObjectsList.Count; i++)
            {
                if(nailableObjectsList[i] == other.gameObject)
                {
                    nailableObjectsList.Remove(nailableObjectsList[i]);
                }
            }
            nailableObjects = nailableObjectsList.ToArray();
        }

    }

    public void printNailableObjects()
    {
        nailableObjectsText.text = "";
        for(int i = 0; i < nailableObjects.Length; i++)
        {
            nailableObjectsText.text += "\n" + nailableObjects[i].name;
        }
    }
    /**
    public void SetDetectors(bool activate)
    {
        if(activate == true)
        {
            nailableDetector.SetActive(true);
            hammerDetector.SetActive(true);
        }
        nailableDetector.SetActive(false);
        hammerDetector.SetActive(false);
    }
    **/

    //Helper function to resize and append to an array
    public Collider[] AppendToArray(Collider[] arrayToResize, Collider[] elementsArray)
    {
        // Create a new resizedArray
        int arrayLength = arrayToResize.Length + elementsArray.Length;
        Collider[] resizedArray = new Collider[arrayLength];

        // Add elements in the first array to the new array.
        for (int i = 0; i < arrayToResize.Length; i++)
        {

            resizedArray[i] = arrayToResize[i];
        }

        // Add elements in the second array to the new array.
        int counter = 0;
        for (int i = arrayToResize.Length; i < arrayLength; i++)
        {

            resizedArray[i] = elementsArray[counter];
            counter++;
        }

        return resizedArray;
    }

}
