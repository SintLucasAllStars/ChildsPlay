#if UNITY_EDITOR || UNITY_STANDALONE

using System.Linq;
using ProBuilder2.Common;
using ProBuilder2.MeshOperations;
using UnityEngine;
// Extrude lives here

/**
 * Do a snake-like thing with a quad and some extrudes.
 */
public class ExtrudeRandomEdges : MonoBehaviour
{
    public float distance = 1f;
    private pb_Face lastExtrudedFace;
    private pb_Object pb;

    /**
     * Build a starting point (in this case, a quad)
     */
    private void Start()
    {
        pb = pb_ShapeGenerator.PlaneGenerator(1, 1, 0, 0, Axis.Up, false);
        pb.SetFaceMaterial(pb.faces, pb_Constant.DefaultMaterial);
        lastExtrudedFace = pb.faces[0];
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Extrude Random Edge")) ExtrudeEdge();
    }

    private void ExtrudeEdge()
    {
        var sourceFace = lastExtrudedFace;

        // fetch a random perimeter edge connected to the last face extruded
        var wings = pb_WingedEdge.GetWingedEdges(pb);
        var sourceWings = wings.Where(x => x.face == sourceFace);
        var nonManifoldEdges = sourceWings.Where(x => x.opposite == null).Select(y => y.edge.local).ToList();
        var rand = Random.Range(0, nonManifoldEdges.Count);
        var sourceEdge = nonManifoldEdges[rand];

        // get the direction this edge should extrude in
        var dir = (pb.vertices[sourceEdge.x] + pb.vertices[sourceEdge.y]) * .5f -
                  sourceFace.distinctIndices.Average(x => pb.vertices[x]);
        dir.Normalize();

        // this will be populated with the extruded edge
        pb_Edge[] extrudedEdges;

        // perform extrusion
        pb.Extrude(new[] {sourceEdge}, 0f, false, true, out extrudedEdges);

        // get the last extruded face
        lastExtrudedFace = pb.faces.Last();

        // not strictly necessary, but makes it easier to handle element selection
        pb.SetSelectedEdges(extrudedEdges);

        // translate the vertices
        pb.TranslateVertices(pb.SelectedTriangles, dir * distance);

        // rebuild mesh with new geometry added by extrude
        pb.ToMesh();

        // rebuild mesh normals, textures, collisions, etc
        pb.Refresh();
    }
}
#endif