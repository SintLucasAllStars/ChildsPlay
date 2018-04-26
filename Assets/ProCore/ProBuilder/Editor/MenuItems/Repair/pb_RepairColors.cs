using ProBuilder2.Common;
using ProBuilder2.EditorCommon;
using UnityEditor;
using UnityEngine;

namespace ProBuilder2.Actions
{
    /**
     * Menu interface for replacing vertex colors on broken objces.
     */
    public class pb_RepairColors : Editor
    {
        [MenuItem("Tools/" + pb_Constant.PRODUCT_NAME + "/Repair/Rebuild Vertex Colors", false,
            pb_Constant.MENU_REPAIR)]
        public static void MenuRepairColors()
        {
            var count = 0;
            foreach (var pb in Selection.transforms.GetComponents<pb_Object>())
                if (pb.colors == null || pb.colors.Length != pb.vertexCount)
                {
                    pb.ToMesh();
                    pb.SetColors(pbUtil.FilledArray(Color.white, pb.vertexCount));
                    pb.Refresh();
                    pb.Optimize();

                    count++;
                }

            pb_EditorUtility.ShowNotification("Rebuilt colors for " + count + " ProBuilder Objects.");
        }
    }
}