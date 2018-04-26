using System.Linq;
using ProBuilder2.Common;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace ProBuilder2.EditorCommon
{
    /**
     * When building the project, remove all references to pb_Objects.
     */
    public class pb_ScenePostProcessor
    {
        [PostProcessScene]
        public static void OnPostprocessScene()
        {
            var invisibleFaceMaterial = (Material) Resources.Load("Materials/InvisibleFace");

            /**
             * Hide nodraw faces if present.
             */
            foreach (pb_Object pb in Object.FindObjectsOfType(typeof(pb_Object)))
            {
                if (pb.GetComponent<MeshRenderer>() == null)
                    continue;

                if (pb.GetComponent<MeshRenderer>().sharedMaterials.Any(x => x != null && x.name.Contains("NoDraw")))
                {
                    var mats = pb.GetComponent<MeshRenderer>().sharedMaterials;

                    for (var i = 0; i < mats.Length; i++)
                        if (mats[i].name.Contains("NoDraw"))
                            mats[i] = invisibleFaceMaterial;

                    pb.GetComponent<MeshRenderer>().sharedMaterials = mats;
                }
            }

            if (EditorApplication.isPlayingOrWillChangePlaymode)
                return;

            foreach (pb_Object pb in Object.FindObjectsOfType(typeof(pb_Object)))
            {
                var go = pb.gameObject;

                var entity = pb.gameObject.GetComponent<pb_Entity>();

                if (entity == null)
                    continue;

                if (entity.entityType == EntityType.Collider || entity.entityType == EntityType.Trigger)
                    go.GetComponent<MeshRenderer>().enabled = false;

                // clear hideflags on prefab meshes
                if (pb.msh != null)
                    pb.msh.hideFlags = HideFlags.None;

                if (!pb_PreferencesInternal.GetBool(pb_Constant.pbStripProBuilderOnBuild))
                    return;

                pb.dontDestroyMeshOnDelete = true;

                Object.DestroyImmediate(pb);
                Object.DestroyImmediate(go.GetComponent<pb_Entity>());
            }
        }
    }
}