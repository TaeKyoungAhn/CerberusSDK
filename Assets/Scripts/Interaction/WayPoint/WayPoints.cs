using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cerberus_Platform
{
    public class WayPoints : MonoBehaviour
    {
        [SerializeField]
        [Range(0, 100)]
        private float circleSize = 1f;

        //Draw Gizmos Edit Mode
        private void OnDrawGizmos()
        {
            foreach (Transform t in transform)
            {
                Gizmos.color = Color.green; //Gizmos Color
                Gizmos.DrawWireSphere(t.position, circleSize); //Create Wire Sphere 
            }

            Gizmos.color = Color.red;

            for (int i = 0; i < transform.childCount - 1; i++)
            {
                Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position); // Connected Line
            }
            Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position); 
        }

        public Transform GetNextWayPoint(Transform currentWaypint)  // Change Next WayPoint
        {
            if (currentWaypint == null)
            {
                return transform.GetChild(0);
            }

            if (currentWaypint.GetSiblingIndex() < transform.childCount - 1)
            {
                return transform.GetChild(currentWaypint.GetSiblingIndex() + 1); //Next WayPoints return;
            }
            else
            {
                return null;
            }
        }
    }
}
