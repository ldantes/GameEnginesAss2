using UnityEngine;
using System.Collections;
 
public class FollowObject : MonoBehaviour {
    public Transform target;
 
    void Update () {
       transform.LookAt(target);
    }
}
