// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using HoloToolkit.Unity.SpatialMapping;

#if UNITY_EDITOR || UNITY_WSA
using UnityEngine.VR.WSA.Persistence;
using UnityEngine.VR.WSA.Sharing;
using UnityEngine.VR.WSA;
#endif

#if !UNITY_EDITOR && UNITY_WSA
using Windows.Storage;
#endif

namespace HoloToolkit.Unity
{
    /// <summary>
    /// Wrapper around world anchor store to streamline some of the persistence api busy work.
    /// </summary>
    public class WorldAnchorManager : Singleton<WorldAnchorManager>
    {
        /// <summary>
        /// To prevent initializing too many anchors at once
        /// and to allow for the WorldAnchorStore to load asyncronously
        /// without callers handling the case where the store isn't loaded yet
        /// we'll setup a queue of anchor attachment operations.  
        /// The AnchorAttachmentInfo struct has the data needed to do this.
        /// </summary>
        private struct AnchorAttachmentInfo
        {
            public GameObject GameObjectToAnchor { get; set; }
            public string AnchorName { get; set; }
            public AnchorOperation Operation { get; set; }
        }

        private enum AnchorOperation
        {
            Create,
            Delete
        }

        /// <summary>
        /// The queue mentioned above.
        /// </summary>
        private Queue<AnchorAttachmentInfo> anchorOperations = new Queue<AnchorAttachmentInfo>();

#if UNITY_EDITOR || UNITY_WSA
        /// <summary>
        /// The WorldAnchorStore for the current application.
        /// Can be null when the application starts.
        /// </summary>
        public WorldAnchorStore AnchorStore { get; private set; }

        public string anchorFileName;
        string folderPath;
        string fileExtension = ".bytes";

        int retries = 0;
        const int maxRetry = 3;

        WorldAnchorTransferBatch TransferBatch;
        List<byte> storedData;
        bool exporting = false;
        public bool importing = false;
#endif

        /// <summary>
        /// When the app starts grab the anchor store immediately.
        /// </summary>
        void Start()
        {
#if UNITY_EDITOR
            Debug.LogWarning("World Anchor Manager does not work in the editor. Anchor Store will never be ready.");
#endif

#if UNITY_EDITOR || UNITY_WSA
            AnchorStore = null;
            WorldAnchorStore.GetAsync(AnchorStoreReady);

#if !UNITY_EDITOR && UNITY_WSA
            folderPath = ApplicationData.Current.RoamingFolder.Path;
#else
            folderPath = Application.streamingAssetsPath;
#endif
            TransferBatch = new WorldAnchorTransferBatch();
            storedData = new List<byte>();
#endif
        }

#if UNITY_EDITOR || UNITY_WSA
        /// <summary>
        /// Callback function that contains the WorldAnchorStore object.
        /// </summary>
        /// <param name="anchorStore">The WorldAnchorStore to cache.</param>
        private void AnchorStoreReady(WorldAnchorStore anchorStore)
        {
            AnchorStore = anchorStore;
        }

        /// <summary>
        /// Each frame see if there is work to do and if we can do a unit, do it.
        /// </summary>
        private void Update()
        {
            if (AnchorStore != null && anchorOperations.Count > 0)
            {
                DoAnchorOperation(anchorOperations.Dequeue());
            }
        }

        /// <summary>
        /// Attaches an anchor to the game object.  If the anchor store has
        /// an anchor with the specified name it will load the acnhor, otherwise
        /// a new anchor will be saved under the specified name.
        /// </summary>
        /// <param name="gameObjectToAnchor">The Gameobject to attach the anchor to.</param>
        /// <param name="anchorName">Name of the anchor.</param>
        public void AttachAnchor(GameObject gameObjectToAnchor, string anchorName)
        {
            if (gameObjectToAnchor == null)
            {
                Debug.LogError("Must pass in a valid gameObject");
                return;
            }

            if (string.IsNullOrEmpty(anchorName))
            {
                Debug.LogError("Must supply an AnchorName.");
                return;
            }

            anchorOperations.Enqueue(
                new AnchorAttachmentInfo
                {
                    GameObjectToAnchor = gameObjectToAnchor,
                    AnchorName = anchorName,
                    Operation = AnchorOperation.Create
                }
            );
        }

        /// <summary>
        /// Removes the anchor from the game object and deletes the anchor
        /// from the anchor store.
        /// </summary>
        /// <param name="gameObjectToUnanchor">gameObject to remove the anchor from.</param>
        public void RemoveAnchor(GameObject gameObjectToUnanchor)
        {
            if (gameObjectToUnanchor == null)
            {
                Debug.LogError("Invalid GameObject");
                return;
            }

            // This case is unexpected, but just in case.
            if (AnchorStore == null)
            {
                Debug.LogError("remove anchor called before anchor store is ready.");
                return;
            }

            anchorOperations.Enqueue(
                new AnchorAttachmentInfo
                {
                    GameObjectToAnchor = gameObjectToUnanchor,
                    AnchorName = string.Empty,
                    Operation = AnchorOperation.Delete
                });
        }

        public void RemoveAnchorFromStore(string anchorName)
        {
            // This case is unexpected, but just in case.
            if (AnchorStore == null)
            {
                Debug.LogError("Remove anchor called before anchor store is ready.");
                return;
            }

            AnchorStore.Delete(anchorName);
        }

        public bool CheckAnchorExist(string anchorName)
        {
            // This case is unexpected, but just in case.
            if (AnchorStore == null)
            {
                Debug.LogError("Check anchor exist called before anchor store is ready.");
                return false;
            }

            string[] allID = AnchorStore.GetAllIds();
            for (int i = 0; i < allID.Length; ++i)
            {
                if (allID[i] == anchorName)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Removes all anchors from the scene and deletes them from the anchor store.
        /// </summary>
        public void RemoveAllAnchors()
        {
            SpatialMappingManager spatialMappingManager = SpatialMappingManager.Instance;

            // This case is unexpected, but just in case.
            if (AnchorStore == null)
            {
                Debug.LogError("remove all anchors called before anchor store is ready.");
            }

            WorldAnchor[] anchors = FindObjectsOfType<WorldAnchor>();

            if (anchors != null)
            {
                foreach (WorldAnchor anchor in anchors)
                {
                    // Don't remove SpatialMapping anchors if exists
                    if (spatialMappingManager == null ||
                        anchor.gameObject.transform.parent.gameObject != spatialMappingManager.gameObject)
                    {
                        anchorOperations.Enqueue(new AnchorAttachmentInfo()
                        {
                            AnchorName = anchor.name,
                            GameObjectToAnchor = anchor.gameObject,
                            Operation = AnchorOperation.Delete
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Function that actually adds the anchor to the game object.
        /// </summary>
        /// <param name="anchorAttachmentInfo">Parameters for attaching the anchor.</param>
        private void DoAnchorOperation(AnchorAttachmentInfo anchorAttachmentInfo)
        {
            switch (anchorAttachmentInfo.Operation)
            {
                case AnchorOperation.Create:
                    string anchorName = anchorAttachmentInfo.AnchorName;
                    GameObject gameObjectToAnchor = anchorAttachmentInfo.GameObjectToAnchor;

                    if (gameObjectToAnchor == null)
                    {
                        Debug.LogError("GameObject must have been destroyed before we got a chance to anchor it.");
                        break;
                    }

                    WorldAnchor existingAnchor = gameObjectToAnchor.GetComponent<WorldAnchor>();
                    if (existingAnchor != null)
                    {
                        DestroyImmediate(existingAnchor);
                    }

                    // Try to load a previously saved world anchor.
                    WorldAnchor savedAnchor = AnchorStore.Load(anchorName, gameObjectToAnchor);
     
                    if (savedAnchor == null)
                    {
                        // Either world anchor was not saved / does not exist or has a different name.
                        Debug.LogWarning(gameObjectToAnchor.name + " : World anchor could not be loaded for this game object. Creating a new anchor.");

                        // Create anchor since one does not exist.
                        CreateAnchor(gameObjectToAnchor, anchorName);
                    }
                    else
                    {
                        savedAnchor.name = anchorName;
                        Debug.Log(gameObjectToAnchor.name + " : World anchor loaded from anchor store and updated for this game object.");
                    }
                    break;
                case AnchorOperation.Delete:
                    if (AnchorStore == null)
                    {
                        Debug.LogError("Remove anchor called before anchor store is ready.");
                        break;
                    }

                    GameObject gameObjectToUnanchor = anchorAttachmentInfo.GameObjectToAnchor;
                    var anchor = gameObjectToUnanchor.GetComponent<WorldAnchor>();

                    if (anchor != null)
                    {
                        AnchorStore.Delete(anchor.name);
                        DestroyImmediate(anchor);
                    }
                    else
                    {
                        Debug.LogError("Cannot get anchor while deleting");
                    }
                    break;
            }
        }

        /// <summary>
        /// Creates an anchor, attaches it to the gameObjectToAnchor, and saves the anchor to the anchor store.
        /// </summary>
        /// <param name="gameObjectToAnchor">The GameObject to attach the anchor to.</param>
        /// <param name="anchorName">The name to give to the anchor.</param>
        private void CreateAnchor(GameObject gameObjectToAnchor, string anchorName)
        {
            var anchor = gameObjectToAnchor.AddComponent<WorldAnchor>();
            anchor.name = anchorName;

            // Sometimes the anchor is located immediately. In that case it can be saved immediately.
            if (anchor.isLocated)
            {
                SaveAnchor(anchor);
            }
            else
            {
                // Other times we must wait for the tracking system to locate the world.
                anchor.OnTrackingChanged += Anchor_OnTrackingChanged;
            }
        }

        /// <summary>
        /// When an anchor isn't located immediately we subscribe to this event so
        /// we can save the anchor when it is finally located.
        /// </summary>
        /// <param name="self">The anchor that is reporting a tracking changed event.</param>
        /// <param name="located">Indicates if the anchor is located or not located.</param>
        private void Anchor_OnTrackingChanged(WorldAnchor self, bool located)
        {
            if (located)
            {
                Debug.Log(gameObject.name + " : World anchor located successfully.");

                SaveAnchor(self);

                // Once the anchor is located we can unsubscribe from this event.
                self.OnTrackingChanged -= Anchor_OnTrackingChanged;
            }
            else
            {
                Debug.LogError(gameObject.name + " : World anchor failed to locate.");
            }
        }

        /// <summary>
        /// Saves the anchor to the anchor store.
        /// </summary>
        /// <param name="anchor"></param>
        private void SaveAnchor(WorldAnchor anchor)
        {
            // Save the anchor to persist holograms across sessions.
            if (AnchorStore.Save(anchor.name, anchor))
            {
                Debug.Log(gameObject.name + " : World anchor saved successfully.");
            }
            else
            {
                Debug.LogError(gameObject.name + " : World anchor save failed.");
            }
        }

        #region Export
        public void ExportToFile(GameObject gameObjectToExport)
        {
            if (exporting)
            {
                Debug.LogWarning("Manager is currently exporting, please try again later.");
                return;
            }

            if (gameObjectToExport == null)
            {
                Debug.LogError("Invalid GameObject");
                return;
            }

            var exportAnchor = gameObjectToExport.GetComponent<WorldAnchor>();
            if (exportAnchor != null)
            {
                if (TransferBatch.anchorCount > 0)
                {
                    TransferBatch.Dispose();
                }

                TransferBatch.AddWorldAnchor(exportAnchor.name, exportAnchor);
                WorldAnchorTransferBatch.ExportAsync(TransferBatch, ExportData, ExportComplete);
                exporting = true;
            }
            else
            {
                Debug.LogError("Cannot get anchor while exporting");
            }
        }

        void ExportData(byte[] data)
        {
            Debug.Log("Exporting data size : " + data.Length.ToString());
            storedData.AddRange(data);
        }

        void ExportComplete(SerializationCompletionReason completionReason)
        {
            exporting = false;
            if (completionReason != SerializationCompletionReason.Succeeded)
            {
                Debug.LogError("Export Failed due to : " + completionReason.ToString());
                return;
            }

            string fullPath = folderPath + "\\" + anchorFileName + fileExtension;

#if UNITY_WINRT
            UnityEngine.Windows.File.WriteAllBytes(fullPath, storedData.ToArray());
#else
            System.IO.File.WriteAllBytes(fullPath, storedData.ToArray());
#endif
            Debug.Log("Succesfully exported data to path : " + fullPath);
        }
        #endregion

        #region Import
        public void ImportFromFile()
        {
            if (importing)
            {
                Debug.LogWarning("Manager is currently importing, please try again later.");
                return;
            }

            importing = true;
            string filePath = Path.Combine(Application.streamingAssetsPath, anchorFileName + fileExtension);
            Debug.Log(String.Format("Importing from {0}", filePath));

#if UNITY_WINRT
            byte[] data = UnityEngine.Windows.File.ReadAllBytes(filePath);
#else
            byte[] data = System.IO.File.ReadAllBytes(filePath);
#endif

            WorldAnchorTransferBatch.ImportAsync(data, ImportComplete);
        }

        private void ImportComplete(SerializationCompletionReason completionReason, WorldAnchorTransferBatch deserializedTransferBatch)
        {
            importing = false;
            if (completionReason != SerializationCompletionReason.Succeeded)
            {
                if (retries == maxRetry)
                {
                    Debug.LogError("Failed to import due to " + completionReason.ToString() + ". After " + retries.ToString() + " retries.");
                    retries = 0;
                    return;
                }

                Debug.LogError("Failed to import due to " + completionReason.ToString() + ". Retries : " + retries.ToString());
                retries++;
                ImportFromFile();
                return;
            }
            Debug.Log("Succesfully imported files.");

            retries = 0;

            string[] ids = deserializedTransferBatch.GetAllIds();
            for (int i = 0; i < ids.Length; ++i)
            {
                AnchorStore.Save(ids[i], deserializedTransferBatch.LockObject(ids[i], this.gameObject));
                Destroy(GetComponent<WorldAnchor>());
            }
        }
#endregion
#endif
        }
}
