using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wait : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to wait for intro
        StartCoroutine(wait_for_intro());
    }

    // Coroutine that waits for 3 seconds and then loads a new scene
    IEnumerator wait_for_intro()
    {
        // Wait for (insert seconds) seconds before loading the next scene
        yield return new WaitForSeconds(42);

        // Load the scene with index 1 (make sure scene 1 exists in the Build Settings)
        SceneManager.LoadScene(1);
    }
}
