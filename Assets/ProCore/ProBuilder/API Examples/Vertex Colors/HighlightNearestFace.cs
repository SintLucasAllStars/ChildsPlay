using ProBuilder2.Common;
using UnityEngine;

/**
 *	Move a sphere around the surface of a ProBuilder mesh, changing the
 *	vertex color of the nearest face.
 *
 *	Scene setup:  Create a Unity Sphere primitive in a new scene, then attach
 *	this script to the sphere.  Press 'Play'
 */
public class HighlightNearestFace : MonoBehaviour
{
    // The nearest face to this sphere.
    private pb_Face nearest;

    // The speed at which the sphere will move.
    public float speed = .2f;

    // ProBuilder mesh component
    private pb_Object target;

    // The distance covered by the plane.
    public float travel = 50f;

    private void Start()
    {
        // Generate a 50x50 plane with 25 subdivisions, facing up, with no smoothing applied.
        target = pb_ShapeGenerator.PlaneGenerator(travel, travel, 25, 25, Axis.Up, false);

        target.SetFaceMaterial(target.faces, pb_Constant.DefaultMaterial);

        target.transform.position = new Vector3(travel * .5f, 0f, travel * .5f);

        // Rebuild the mesh (apply pb_Object data to UnityEngine.Mesh)
        target.ToMesh();

        // Rebuild UVs, Colors, Collisions, Normals, and Tangents
        target.Refresh();

        // Orient the camera in a good position
        var cam = Camera.main;
        cam.transform.position = new Vector3(25f, 40f, 0f);
        cam.transform.localRotation = Quaternion.Euler(new Vector3(65f, 0f, 0f));
    }

    private void Update()
    {
        var time = Time.time * speed;

        var position = new Vector3(
            Mathf.PerlinNoise(time, time) * travel,
            2,
            Mathf.PerlinNoise(time + 1f, time + 1f) * travel
        );

        transform.position = position;

        if (target == null)
        {
            Debug.LogWarning("Missing the ProBuilder Mesh chaser!");
            return;
        }

        // instead of testing distance by converting each face's center to world space,
        // convert the world space of this object to the pb-Object local transform.
        var pbRelativePosition = target.transform.InverseTransformPoint(transform.position);

        // reset the last colored face to white
        if (nearest != null)
            target.SetFaceColor(nearest, Color.white);

        // iterate each face in the pb_Object looking for the one nearest
        // to this object.
        var faceCount = target.faces.Length;
        var smallestDistance = Mathf.Infinity;
        nearest = target.faces[0];

        for (var i = 0; i < faceCount; i++)
        {
            var distance = Vector3.Distance(pbRelativePosition, FaceCenter(target, target.faces[i]));

            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                nearest = target.faces[i];
            }
        }

        // Set a single face's vertex colors.  If you're updating more than one face, consider using
        // the pb_Object.SetColors(Color[] colors); function instead.
        target.SetFaceColor(nearest, Color.blue);

        // Apply the stored vertex color array to the Unity mesh.
        target.RefreshColors();
    }

    /**
     *	Returns the average of each vertex position in a face.
     *	In local space.
     */
    private Vector3 FaceCenter(pb_Object pb, pb_Face face)
    {
        var vertices = pb.vertices;

        var average = Vector3.zero;

        // face holds triangle data.  distinctIndices is a
        // cached collection of the distinct indices that
        // make up the triangles. Ex:
        // tris = {0, 1, 2, 2, 3, 0}
        // distinct indices = {0, 1, 2, 3}
        foreach (var index in face.distinctIndices)
        {
            average.x += vertices[index].x;
            average.y += vertices[index].y;
            average.z += vertices[index].z;
        }

        var len = face.distinctIndices.Length;

        average.x /= len;
        average.y /= len;
        average.z /= len;

        return average;
    }
}