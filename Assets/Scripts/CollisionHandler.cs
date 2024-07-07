using UnityEngine;
using UnityEngine.SceneManagement; 

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
                NextLevel();
                break;    
            default:
                Debug.Log("You're dead!");
                // SceneManager.LoadScene("Scenes/Sandbox");
                ReloadLevel();
                break;    
        }
    }

    void ReloadLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;   // get the index of the active scene.
        SceneManager.LoadScene(currentSceneIndex);
    }

    void NextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;   // get the index of the NEXT scene.
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
