using UnityEngine;
using UnityEngine.SceneManagement; 

// used on player or other objs? on player
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 2f;

    [SerializeField] AudioClip explode;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem explodeParticles;
    [SerializeField] ParticleSystem successParticles;

    private AudioSource audioSource; 

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();  // Cache our audioSource. This is a component that's left manually empty.
    }

    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnCollisionEnter(Collision other)
    {
        // tutorial's MUCH cleaner approach: 
        // this ensures that all other logic is ignored if isTransitioning is true.
        if (isTransitioning) {
            return;
        }

        switch(other.gameObject.tag) {
            case "Friendly":
                Debug.Log("At starting pad!");
                break; 
            case "Finish":                
                successParticles.Play();
                NextLevelSequence();               
                break;  
            
            default:
                explodeParticles.Play();
                StartCrashSequence();               
                break;    
        }
    }

    void StartCrashSequence() {
        isTransitioning = true;
        audioSource.Stop();     // current audioSource (rocket engine) stops 
        audioSource.PlayOneShot(explode);
        GetComponent<MovementScript>().enabled = false;
        // ReloadLevel();
        Invoke("ReloadLevel", delay);
    }

    void NextLevelSequence() {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        GetComponent<MovementScript>().enabled = false;
        Invoke("NextLevel", delay);
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
