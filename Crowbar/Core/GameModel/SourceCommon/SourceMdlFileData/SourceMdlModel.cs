//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlModel
	{

		public SourceMdlModel()
		{
			//MyBase.New()

			theSmdFileNames = new List<string>(SourceConstants.MAX_NUM_LODS);
			for (int i = 0; i < SourceConstants.MAX_NUM_LODS; i++)
			{
				theSmdFileNames.Add("");
			}
		}

		//struct mstudiomodel_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	inline const char * pszName( void ) const { return name; }
		//	char				name[64];

		//	int					type;

		//	float				boundingradius;

		//	int					nummeshes;	
		//	int					meshindex;
		//	inline mstudiomesh_t *pMesh( int i ) const { return (mstudiomesh_t *)(((byte *)this) + meshindex) + i; };

		//	// cache purposes
		//	int					numvertices;		// number of unique vertices/normals/texcoords
		//	int					vertexindex;		// vertex Vector
		//	int					tangentsindex;		// tangents Vector

		//	// These functions are defined in application-specific code:
		//	const vertexFileHeader_t			*CacheVertexData(			void *pModelData );

		//	// Access thin/fat mesh vertex data (only one will return a non-NULL result)
		//	const mstudio_modelvertexdata_t		*GetVertexData(		void *pModelData = NULL );
		//	const thinModelVertices_t			*GetThinVertexData(	void *pModelData = NULL );

		//	int					numattachments;
		//	int					attachmentindex;

		//	int					numeyeballs;
		//	int					eyeballindex;
		//	inline  mstudioeyeball_t *pEyeball( int i ) { return (mstudioeyeball_t *)(((byte *)this) + eyeballindex) + i; };

		//	mstudio_modelvertexdata_t vertexdata;

		//	int					unused[8];		// remove as appropriate
		//};

		//	char				name[64];
		public char[] name = new char[64];
		//	int					type;
		public int type;
		//	float				boundingradius;
		public float boundingRadius;
		//	int					nummeshes;	
		public int meshCount;
		//	int					meshindex;
		public int meshOffset;

		//	int					numvertices;		// number of unique vertices/normals/texcoords
		public int vertexCount;
		//	int					vertexindex;		// vertex Vector
		public int vertexOffset;
		//	int					tangentsindex;		// tangents Vector
		public int tangentOffset;
		//	int					numattachments;
		public int attachmentCount;
		//	int					attachmentindex;
		public int attachmentOffset;

		//	int					numeyeballs;
		public int eyeballCount;
		//	int					eyeballindex;
		public int eyeballOffset;

		//	mstudio_modelvertexdata_t vertexdata;
		public SourceMdlModelVertexData vertexData;

		//	int					unused[8];		// remove as appropriate
		public int[] unused = new int[8];


		public List<string> theSmdFileNames;
		public List<SourceMdlMesh> theMeshes;
		public List<SourceMdlEyeball> theEyeballs;

	}

}