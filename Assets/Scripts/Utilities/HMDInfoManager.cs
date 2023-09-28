using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.MockHMD;
using Unity.XR.Oculus;
using UnityEditor;
using UnityEditor.XR.Management;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;

namespace Utilities
{
    public class HMDInfoManager : MonoBehaviour
    {
        void Awake()
        {
            var generalSettings = XRGeneralSettingsPerBuildTarget.XRGeneralSettingsForBuildTarget(BuildTargetGroup.Standalone);
            var settingsManager = generalSettings.Manager;

            // Get a readonly reference to the current set of loaders.
            var readonlyCurrentLoaders = settingsManager.activeLoaders;

            // Modifying the loader list order
            var reorderedLoadersList = new List<XRLoader>();

            foreach (var loader in readonlyCurrentLoaders)
            {
#if UNITY_ANDROID
        if (loader is XRFooLoader)
        {
            // Insert 'Foo' Loaders at head of list
            reorderedLoaderList.Insert(loader, 0);
        }
        else if (loader is XRBarLoader)
        {
            // Insert 'Bar' Loaders at back of list
            reorderedLoaderList.Insert(loader, reorderedLoaderList.Count);
        }
#else // !UNITY_ANDROID
                if (loader is MockHMDLoader)
                {
                    // Insert 'Bar' Loaders at head of list
                    reorderedLoadersList.Insert(0, loader);
                }
                else if (loader is OculusLoader)
                {
                    // Insert 'Foo' Loaders at back of list
                    reorderedLoadersList.Insert(reorderedLoadersList.Count, loader);
                }
#endif // end UNITY_ANDROID
            }
            foreach (var loader in readonlyCurrentLoaders)
            {
                Debug.Log(loader);
            }
            // Would only fail if the new list contains a loader that was
            // not in the original list.
            if (!settingsManager.TrySetLoaders(reorderedLoadersList))
                Debug.LogError("Failed to set the reordered loader list! Refer to the documentation for additional information!");

        }
    
        private void Start()
        {
            /*StartCoroutine(StartXRCoroutine());*/
             if (!XRSettings.isDeviceActive)
            {
                Debug.Log("Sin Lentes VR Asignados");
            }
            else
            {
                if (XRSettings.isDeviceActive && (XRSettings.loadedDeviceName == "Mock HMD" || XRSettings.loadedDeviceName == "MockHMD Display"))
                {
                    Debug.Log("Se esta utilizando mock para simulacion");
                }
                else
                {
                    Debug.Log($"Se conecto el headset con nombre {XRSettings.loadedDeviceName}");
                }
            }
        }
        
        private IEnumerator StartXRCoroutine()
        {
            Debug.Log("Initializing XR...");
            yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

            if (XRGeneralSettings.Instance.Manager.activeLoader == null)
            {
                Debug.LogError("Initializing XR Failed. Check Editor or Player log for details.");
            }
            else
            {
                Debug.Log("Starting XR...");
                XRGeneralSettings.Instance.Manager.StartSubsystems();
            }
        }

    }
}