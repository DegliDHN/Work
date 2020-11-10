using UnityEngine;
using System.Reflection;
using UnityEngine.UI;

/// <remarks>
/// Change the first two number to update the major and minor version number.
/// The following number are the build number (which is increased automatically
///  once a day, and the revision number which is increased every second). 
/// </remarks>
[assembly: AssemblyVersion("1.1.*")]
public class VersionNumber : MonoBehaviour
{
	public TMPro.TMP_Text versionNumberTxt;
	string _version;

	/// <summary>
	/// Gets the version.
	/// </summary>
	/// <value>The version.</value>
	public string Version
	{
		get
		{
			if (_version == null)
			{
				_version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
			return _version;
		}
	}

	void Start()
	{
		DontDestroyOnLoad(this);

		// Log current version in log file
		Debug.Log(string.Format("Currently running version is {0}", Version));

		if(Debug.isDebugBuild){
			versionNumberTxt.text = "v."+Version;

			versionNumberTxt.gameObject.SetActive(true);
		}
	}



}