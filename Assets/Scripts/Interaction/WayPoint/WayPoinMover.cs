
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cerberus_Platform
{
    public class WaypointMover : MonoBehaviour
    {

        public WayPoints wayPoints;
        [SerializeField]
        private float moveSpeed = 1f;
        private float distanceThreshold = 0.1f;

        public Transform currentWayPoint;

        [SerializeField]
        public LineRenderer lineRenderer;

        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        void Update()
        {
            if (currentWayPoint != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentWayPoint.position, moveSpeed * Time.deltaTime);

                Debug.Log("child Count : " + wayPoints.transform.childCount.ToString());

                if (Vector3.Distance(transform.position, currentWayPoint.position) < distanceThreshold)
                {
                    currentWayPoint = wayPoints.GetNextWayPoint(currentWayPoint);
                    transform.LookAt(currentWayPoint);
                }
            }
            else if (currentWayPoint == null)
            {
                //Do-Nothing
            }
        }
    }
}
