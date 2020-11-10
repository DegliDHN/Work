using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;
using OranUnityUtils;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

public static class AnimatorEx 
{
	/// <summary>
	/// This method will start returning true the frame after an animation/transition is started, until it is done.
	/// </summary>
	/// <param name="animator"></param>
	/// <param name="layer"></param>
	/// <returns></returns>
    public static bool IsAnimating(this Animator animator, int layer = 0){
		return animator.GetCurrentAnimatorStateInfo(layer).normalizedTime < 1 || animator.IsInTransition(layer);
	}
	
}
