namespace BasicExample
{
    using InControl;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class SceneChange : MonoBehaviour
    {
        public string sceneToLoad;
        public void onStartClick()
        {
            // Change Scene
            onSceneChange("playScene");
        }

        public void onEndClick()
        {
            // Change Scene
            onSceneChange("");
        }

        public void onCreditsClick()
        {
            // Change Scene
            onSceneChange("");
        }

        public void onBackToMainClick()
        {
            // Change Scene
            onSceneChange("");
        }

        public void onSceneChange(string sceneToChange)
        {
            // Load Scene
            SceneManager.LoadScene(sceneToChange);
        }
    }
}

