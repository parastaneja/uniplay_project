using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ARTrackedImageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabList;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;

    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        foreach (GameObject gObj in prefabList)
        {
            GameObject newPrefab = Instantiate(gObj, Vector3.zero, Quaternion.identity);
            newPrefab.name = gObj.name;
            spawnedPrefabs.Add(newPrefab.name, newPrefab);
        }
    }
    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }
    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }
    private void ImageChanged(ARTrackedImagesChangedEventArgs changedEventArgs)
    {
        foreach (ARTrackedImage img in changedEventArgs.added)
        {
            UpdateImage(img);
        }
        foreach (ARTrackedImage img in changedEventArgs.updated)
        {
            UpdateImage(img);
        }
        foreach (ARTrackedImage img in changedEventArgs.removed)
        {
            spawnedPrefabs[img.name].SetActive(false);
        }
    }
    private void UpdateImage(ARTrackedImage img)
    {
        string name = img.referenceImage.name;
        Vector3 position = img.transform.position;

        GameObject prefab = spawnedPrefabs[name];
        prefab.transform.position = position;
        prefab.SetActive(true);

        foreach (GameObject gObj in spawnedPrefabs.Values)
        {
            if (gObj.name != name)
            {
                gObj.SetActive(false);
            }
        }

    }
}
