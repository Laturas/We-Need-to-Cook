using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LiquidRenderer : MonoBehaviour
{
void Start() {
		//generate_strip();
		generate_cylinder();
        generate_circle();
		//foreach (Vector3 vert in verts) {
		//	Instantiate(debug_sphere, vert, Quaternion.identity);
		//}
		Generate();
	}
	public int segments = 5;
	public float topPos = 3f;
    public float radius = 2f;
    public bool generate = false;
    private GameObject cylinder_go;
    private GameObject circle_go;

	public List<Vector3> verts;
	public List<Vector3> tri_indices;
	public Vector3[] normals;
	public Vector2[] uvs;
    public float bottom_offset = 0.01f;

    public List<Vector3> circle_verts;
    public List<Vector3> circle_tri_indices;
	public Vector3[] circle_normals;
	public Vector2[] circle_uvs;

	public void generate_cylinder() {
		verts = new();
		tri_indices = new();

		for (int it = 0; it < segments; it++) {
			float t = it / (float)segments;
			float angle = t * 2 * 3.1415926f;

			Vector3 position = new(
				Mathf.Cos(angle) * radius,
				Mathf.Sin(angle) * radius,
				bottom_offset
			);
            Vector3 position_2 = new(
				Mathf.Cos(angle) * radius,
				Mathf.Sin(angle) * radius,
				topPos + bottom_offset
			);

			verts.Add(position_2 * transform.lossyScale.x);
			verts.Add(position * transform.lossyScale.x);
		}
		int bound = segments * 2;
		int i = 2; int j = 0; int k = 1;
		while (i < bound && j < bound && k < bound) {
			tri_indices.Add(new(i,j,k));
			j += 2;
			i++;
			if (!(i < bound && j < bound && k < bound)) {break;}
			tri_indices.Add(new(i,j,k));
			k += 2;
			i++;
		}
		tri_indices.Add(new(0,j,k));
		tri_indices.Add(new(1,0,k));
	}
    
    public void generate_circle() {
		circle_verts = new();
		circle_tri_indices = new();

        circle_verts.Add(new Vector3(
				0f, 0f, topPos + bottom_offset
			) * transform.lossyScale.x);

		for (int it = 0; it < segments; it++) {
			float t = it / (float)segments;
			float angle = t * 2 * 3.1415926f;

            Vector3 position = new(
				Mathf.Cos(angle) * radius,
				Mathf.Sin(angle) * radius,
				topPos + bottom_offset
			);

			circle_verts.Add(position * transform.lossyScale.x);
		}
		int bound = segments;
		int i = 1; int j = 2;
		while (i <= bound && j <= bound) {
            circle_tri_indices.Add(new(0,i,j));
            i++; j++;
		}
		circle_tri_indices.Add(new(0,i,1));
	}

    public (MeshRenderer, MeshFilter) generate_mesh_go(ref GameObject assign_to, Material mat) {
        if (assign_to != null) {Destroy(assign_to);}
		GameObject new_mesh = new("Liquid");
		MeshRenderer mr = new_mesh.AddComponent<MeshRenderer>();
		MeshFilter mf = new_mesh.AddComponent<MeshFilter>();

		List<Material> tmp_mats = new();
		tmp_mats.Add(mat);
        new_mesh.transform.SetParent(transform);
        new_mesh.transform.position = transform.position;
        new_mesh.transform.rotation = Quaternion.LookRotation(Vector3.up);

		mr.materials = tmp_mats.ToArray();
        assign_to = new_mesh;

		return (mr, mf);
	}

	public void Generate() {
		var vals_cyl = generate_mesh_go(ref cylinder_go, ScriptableObjectHolder.instance.materials.list[0]);

		int[] triangle_indices = new int[tri_indices.Count * 3];
		int iter = 0;
		foreach (Vector3 tri in tri_indices) {
			triangle_indices[iter++] = (int)tri.x;
			triangle_indices[iter++] = (int)tri.y;
			triangle_indices[iter++] = (int)tri.z;
		}

		build_simple_mesh("Procedural Ring", vals_cyl.Item2, verts.ToArray(), triangle_indices, normals, uvs);

        var vals_circ = generate_mesh_go(ref circle_go, ScriptableObjectHolder.instance.materials.list[0]);

		int[] circle_triangle_indices = new int[circle_tri_indices.Count * 3];
		iter = 0;
		foreach (Vector3 tri in circle_tri_indices) {
			circle_triangle_indices[iter++] = (int)tri.x;
			circle_triangle_indices[iter++] = (int)tri.y;
			circle_triangle_indices[iter++] = (int)tri.z;
		}

		build_simple_mesh("Procedural Circle", vals_circ.Item2, circle_verts.ToArray(), circle_triangle_indices, normals, uvs);
	}

    public static void build_simple_mesh(
        string name,
		MeshFilter shared_mesh,
		Vector3[] verts, int[] tri_indices,
		Vector3[] normals, Vector2[] uvs
	) {
		Mesh mesh = new();
		mesh.name = name;

		mesh.SetVertices(verts);
		mesh.SetNormals(normals);
		mesh.SetUVs(0, uvs);
		mesh.triangles = tri_indices;
		shared_mesh.mesh = mesh;
	}

	void Update() {
		if (generate) {generate_cylinder(); generate_circle(); Generate(); generate = false;}
	}
}
