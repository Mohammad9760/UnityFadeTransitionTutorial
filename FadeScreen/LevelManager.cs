using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

	private int NextSceneIndex
	{
		get
		{
			var i = SceneManager.GetActiveScene().buildIndex;
			if(i >= SceneManager.sceneCountInBuildSettings)
				i = 0;
			return i;
		}
	}
	public void FadeToNextScene()
	{
		FadeIntoScene(NextSceneIndex);
	}
	public void FadeIntoScene(int index)
	{
		StartCoroutine(FadeToSceneAsync(index));
	}

	private IEnumerator FadeToSceneAsync(int i)
	{
		yield return new WaitUntil(() => FadeScreen.FadeCompleted);
		FadeScreen.instance.FadeOut();
		yield return new WaitUntil(() => FadeScreen.FadeCompleted);
		
		var op = SceneManager.LoadSceneAsync(i);
		while(!op.isDone)
		{
			yield return null;
		}
	}

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
            FadeToNextScene();
    }

}
