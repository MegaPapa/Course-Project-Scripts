using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour {

	public void StartGame()
	{
		Application.LoadLevel ("FirstChapter");
	}

	public void QuitGame()
	{
		Application.Quit ();
	}

	public AnimationClip about_animation;
	public AnimationClip backslide_animation;

	public void Slide()
	{
		animation.Play (about_animation.name);
	}

	public void Backslide()
	{
		animation.Play (backslide_animation.name);
	}
}
