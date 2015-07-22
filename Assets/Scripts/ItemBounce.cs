using UnityEngine;
using System.Collections;

public class ItemBounce : MonoBehaviour {
	
	// Make an item float up and down 
	void Start () 
    {
        
        // uses GoKit! Google it for documentation
        Vector3[] points = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, .25f, 0), new Vector3(0, 0, 0) };  // this path uses relative values rather than world values
        GoSpline path = new GoSpline(points);

        Go.to(transform, 2f, new GoTweenConfig().positionPath(path, true).setIterations(-1));
        //transform.positionTo(1f, new Vector3(0, .25f, 0), true);
	}
}
