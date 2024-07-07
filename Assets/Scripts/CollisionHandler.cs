using UnityEngine;

// used on player or other objs? on player
public class CollisionHandler : MonoBehaviour
{
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag) {
            case "Fuel":
                Debug.Log("Obtained fuel!");
                break; 
            case "Friendly":
                Debug.Log("At starting pad!");
                break; 
            case "Finish":
                Debug.Log("Finished!");
                break;    
            default:
                Debug.Log("You're dead!");
                break;    
        }
    }
}
