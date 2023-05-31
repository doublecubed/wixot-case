// ------------------------
// Onur Ereren - May 2023
// ------------------------

// Utility script that is used to find and return components on objects
// It incorporates a warning message if the required component is not found

using UnityEngine;

namespace WixotCase.Utility
{
	public static class GameUtility
	{
        public static T FindComponentOnSelf<T>(Transform searchingScript)
        {
            if (searchingScript.TryGetComponent<T>(out T component))
            {
                return component;
            }

            DisplayWarningMessage(searchingScript.name, typeof(T).Name);
            return default(T);

        }

        public static T FindComponentOnChildren<T>(Transform searchingScript)
        {
            int childCount = searchingScript.childCount;

            for (int i = 0; i<childCount; i++)
            {
                if (searchingScript.GetChild(i).TryGetComponent<T>(out T component))
                {
                    return component;
                }
            }

            DisplayWarningMessage(searchingScript.name, typeof(T).Name);
            return default(T);

        }

        public static T FindComponentOnParent<T>(Transform searchingScript)
        {
            if (searchingScript.parent.TryGetComponent<T>(out T component))
            {
                return component;
            }

            DisplayWarningMessage(searchingScript.name, typeof(T).Name);
            return default(T);
        }

        private static void DisplayWarningMessage(string searchingScriptName, string componentTypeName)
        {
            Debug.LogWarning(searchingScriptName + " requires a " + componentTypeName + " component but it is missing");
        }
    }
}