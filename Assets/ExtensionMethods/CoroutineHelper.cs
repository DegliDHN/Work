/*======= Copyright (c) Immerxive Srl, All rights reserved. ===================

 Author: Oran Bar

 Purpose: Coroutines are not independent. They need a MonoBehaviour to be ran. But while every gameobject has a transform, it is not true about MonoBehaviours: 
 some objects might not have a MonoBehaviour component, making them unable to run, for example, a coroutine that changes their position
 So when a GameObject needs to run a coroutine, I just add this Object to it.

 Notes: Coroutines can be started from any script at any point. There is a catch: If an object that is running coroutines is deactivated, the routines stop immediately.  
 Because of this, we want to be aware of which component is running the coroutine, and the active state of its host gameobject 
 Using the ExtensionMethod GetMonoBehaviour() will automatically add (unless it finds one already there) and return a CoroutineHelper component that can be used to handle coroutines more cleanly

=============================================================================*/
using UnityEngine;	  

public class CoroutineHelper : MonoBehaviour {
					  
}
