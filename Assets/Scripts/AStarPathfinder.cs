using UnityEngine;
using System.Collections;
using Pathfinding;

public class AStarPathfinder : MonoBehaviour {
    // where the mob is pathing to
    public Transform target;
    protected Vector3 targetPosition;

    protected Seeker seeker;
    protected CharacterController controller;
    
    // The calculated path
    public Path path;

    // mob's speed
    public float speed = 100;

    // Waypoint system 
    public float nextWaypointDistance = 3;
    private int currentWaypoint = 0;

	// Use this for initialization
	void Start () 
    {
	    // Get a reference to the seeker component on the mob
        seeker = GetComponent<Seeker>();
        // Get a reference to the character controller
        controller = GetComponent<CharacterController>();

        // get the target position
        targetPosition = target.position;

        // make a new path to targetPosition, calling OnPathComplete once it's done
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);
	}
	
	// called when the current path is complete
    public void OnPathComplete(Path p)
    {
        Debug.Log("Path complete! Here are any errors: " + p.error);
        // if there are no errors, reset the path
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Update()
    {
        // if there's no path, just exit
        if (path == null) return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            Debug.Log("End of Path Reached");
            return;
        }

        // Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.deltaTime;
        controller.SimpleMove(dir);

        // Check if we are close enough to the next waypoint
        // If we are, proceed to follow the next waypoint
        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }
}
