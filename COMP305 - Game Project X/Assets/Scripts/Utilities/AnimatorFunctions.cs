using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFunctions : MonoBehaviour
{
	[SerializeField] GameObject obj;
	public bool disableOnce;

	void PlaySound(AudioClip whichSound){
		if(!disableOnce){
			obj.GetComponent<AudioSource>().PlayOneShot (whichSound);
		}else{
			disableOnce = false;
		}
	}
}	
