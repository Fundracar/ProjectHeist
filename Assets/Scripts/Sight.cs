using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.GlobalIllumination;

public class Sight : MonoBehaviour
{
	public float angle = 75f;
	public float distance = 10f;
	public int precision = 20;
	public Material material;
	public bool debug = false;
	public float freq = 0.1F;
	public LayerMask mask;
	
	Vector3[] _directions;
	Mesh _sightMesh;
	Transform _mTransform;
	
	int _nbPoints;
	int _nbTriangle;
	int _nbFace;
	int _nbIndice;
	int _row;
	Vector3[] _points;
	int[] _indices;
	
	// Use this for initialization
	void Start () 
	{
		// initialization of the cone
		GameObject sightObject = new GameObject( "ConeSight" );
		_sightMesh = new Mesh();
		((MeshFilter) sightObject.AddComponent( typeof( MeshFilter ))).mesh = _sightMesh;
		((MeshRenderer) sightObject.AddComponent( typeof( MeshRenderer ))).material = material;
		sightObject.GetComponent<MeshRenderer>().castShadows = false;
		_mTransform = transform;
		
		// Prepare the rays
		precision = precision > 1 ? precision : 2;
		_directions = new Vector3[precision];
		float angleStart = -angle*0.5F;
		float angleStep = angle / (precision-1);
		for( int i = 0; i < precision; i++ )
		{
			Matrix4x4 mat = Matrix4x4.TRS( Vector3.zero, Quaternion.Euler(0,angleStart + i*angleStep,0), Vector3.one );
			_directions[i] = mat * Vector3.forward;
		}
		
		// Prepare mesh manipulation tools
		_nbPoints =  precision*2;
		_nbTriangle = _nbPoints - 2;
		_nbFace = _nbTriangle / 2;
		_nbIndice =  _nbTriangle * 3;
		_row = _nbFace+1;
		
		_points = new Vector3[_nbPoints];
		_indices = new int[ _nbIndice ];
		
		for( int i = 0; i < _nbFace; i++ )
		{
			_indices[i*6+0] = i+0;
			_indices[i*6+1] = i+1;
			_indices[i*6+2] = i+_row;
			_indices[i*6+3] = i+1;
			_indices[i*6+4] = i+_row+1;
			_indices[i*6+5] = i+_row;
		}
		
		_sightMesh.vertices = new Vector3[_nbPoints];
		_sightMesh.uv = new Vector2[_nbPoints];
		_sightMesh.triangles = _indices;		
				
		StartCoroutine( Scan() );
	}
	
	// Calls mesh modification every freq seconds
	IEnumerator Scan() 
	{
		while( true )
		{
			UpdateSightMesh();
			yield return new WaitForSeconds(freq);
		}
	}
	
	// Modify the mesh
	private void UpdateSightMesh()
	{			
		// Throws the rays to place the vertices as far as possible
		for( int i = 0; i < precision; i++ )
		{
			Vector3 dir = _mTransform.TransformDirection(_directions[i]);
			RaycastHit hit;
			float dist = distance;
			if( Physics.Raycast( _mTransform.position, dir, out hit, distance, mask ) ) // If we touch, we narrow the ray
				dist = hit.distance;
			
			if( debug ) Debug.DrawRay( _mTransform.position, dir * dist );
			
			// Vertex positioning
			_points[i] = _mTransform.position + dir * dist;
			_points[i+precision] = _mTransform.position;
		}
		
		// We reassign the vertices
		_sightMesh.vertices = _points;		
		_sightMesh.RecalculateNormals();
		_sightMesh.RecalculateBounds();
	}
}
