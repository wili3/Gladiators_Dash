using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : Singleton<CameraMain> {

	public void Activate()
	{
		GameEvents.Instance.start = true;
	}
}
