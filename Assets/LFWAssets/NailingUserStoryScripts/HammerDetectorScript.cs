using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerDetectorScript : MonoBehaviour
{
    private NailScript parent;

    private void Start()
    {
        parent = transform.parent.GetComponent<NailScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        parent.onChildTriggerEnter(this.gameObject.GetComponent<BoxCollider>(), other);
    }

    private void OnTriggerExit(Collider other)
    {
        parent.onChildTriggerEnter(this.gameObject.GetComponent<BoxCollider>(), null);
    }
}
