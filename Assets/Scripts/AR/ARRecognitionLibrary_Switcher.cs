using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System.Linq;

[System.Serializable]
public struct AR_Recognition_Library{
	public string libraryName;
	public XRReferenceImageLibrary library;
}

public class ARRecognitionLibrary_Switcher : MonoBehaviour
{
    public List<AR_Recognition_Library> recognition_libraries;
	public Dropdown targetDropdown;

    void Start()
    {
		targetDropdown.ClearOptions();
		targetDropdown.AddOptions(recognition_libraries.Select(rl => rl.libraryName).ToList());

		targetDropdown.onValueChanged.AddListener(SwitchLibrary);
    }

    private void SwitchLibrary(int library_index)
    {
        
    }
}
