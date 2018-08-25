/*
 * Scriptable Object Context Editor 		
 * --------------------------------------------------------------------
 * Allows you to easily add ways to create scriptable objects to the Create pulldown in the project pane.
 *
 * Author
 * Martin Nerurkar of Sharkbomb Studios (http://www.sharkbombs.com)
 * Based on the CreateScriptableObjectAsset work by Brandon Edmark and Lea Hayes
 * 
 * License
 * This script is made available under a CC0 1.0 Universal license.
 * You can copy, modify, distribute and perform the work, even for commercial purposes, all without asking permission. 
 * Find out more here: https://creativecommons.org/publicdomain/zero/1.0/
 */

using UnityEngine;
using UnityEditor;
using System.IO;

public class ScriptableObjectContextEditor : Editor
{

	// To create new menu items, do this, here or elsewhere:
	// Third parameter in MenuItem is priority which determines order and is used to group in increments of 50
	/*
	[MenuItem("Assets/Create/ScriptableObject/YourClass", false, 51)]
	public static void CreateAsset ()
	{
		// To create a Scriptable Object
		ScriptableObjectContextEditor.CreateAsset<YourClass> ();
	}
	*/

	/// <summary>
	/// Creating specific class menu items.
	/// </summary>
	/// <returns>The created ScriptableObject.</returns>
	/// <typeparam name="T">Type of ScriptableObject to create.</typeparam>
	public static T CreateAsset<T>() where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T>();

		string path = GetAssetPath();
		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + ".asset");

		BuildAsset(asset, assetPathAndName);

		return asset;
	}

	/// <summary>
	/// Gets the target path for the asset to create.
	/// </summary>
	/// <returns>The asset path.</returns>
	public static string GetAssetPath()
	{
		string path;

		path = AssetDatabase.GetAssetPath(Selection.activeObject);
		if (path == "")
		{
			path = "Assets";
		}
		else if (Path.GetExtension(path) != "")
		{
			path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
		}

		return path;
	}

	/// <summary>
	/// Builds the asset and does neccessary AssetDatabase things.
	/// </summary>
	/// <param name="assetType">Asset type.</param>
	/// <param name="path">Path.</param>
	public static void BuildAsset(ScriptableObject asset, string assetPathAndName)
	{
		AssetDatabase.CreateAsset(asset, assetPathAndName);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
	}

	/// <summary>
	/// Create Project Create Context Menu item
	/// </summary>
	[MenuItem("Assets/Create/ScriptableObject/Select Script", false, 1)]
	static void DoCreateScriptableObject()
	{
		string targetScriptPath;
		MonoScript targetScript;

		string str_ScriptFolder = Application.dataPath + "/" + "Assets" + "/" + "Scripts";

		DirectoryInfo ScriptFolder = new DirectoryInfo(str_ScriptFolder);

		foreach(var ScriptFile in ScriptFolder.GetFiles("*.cs"))
		{

			if(File.Exists(ScriptFolder + "/" + Path.GetFileNameWithoutExtension(ScriptFile.FullName) + ".asset"))
			{
				continue;
			} //End if

			targetScriptPath = ScriptFile.FullName.Replace('\\', '/');

			if (targetScriptPath.StartsWith(Application.dataPath))
			{
				targetScriptPath = "Assets" + targetScriptPath.Substring(Application.dataPath.Length);
			}

			// Get the target script
			targetScript = AssetDatabase.LoadAssetAtPath<MonoScript>(targetScriptPath);

			if (targetScript == null)
			{
				continue;
			}

			// Check if we are a ScriptableObject
			if (typeof(ScriptableObject).IsAssignableFrom(targetScript.GetClass()))
			{
				var scriptableObject = ScriptableObject.CreateInstance(targetScript.GetClass());

				string path = GetAssetPath();
				string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + scriptableObject.GetType().ToString() + ".asset");

				BuildAsset(scriptableObject, assetPathAndName);
			}
			else
			{
				Debug.LogWarning("Create ScriptableObject Asset failed: Selected Class does not inherit from ScriptableObject");
			}
		}
	}
}