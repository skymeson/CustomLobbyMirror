using UnityEngine;
using System.Collections;

public class SceneSwitcher : MonoBehaviour {
    public GameObject[] sceneObjects;
		
	void Start()
    {
        SceneManagerScript.DisableAllScenes();
        SceneManagerScript.InitializeDefaultScene();
    }

    public void ActivateScene(string name)
    {
        SceneManagerScript.LoadIngameScene(name);
    }

    public void ActivateScene(GameObject sceneObject)
    {
        ActivateScene(sceneObject.name);
    }
}
