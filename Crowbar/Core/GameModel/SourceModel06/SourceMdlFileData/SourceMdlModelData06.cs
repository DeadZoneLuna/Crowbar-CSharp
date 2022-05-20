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
	public class SourceMdlModelData06
	{

		//FROM: [06] HL1Alpha model viewer gsmv_beta2a_bin_src\src\src\studio\studio.h
		//typedef struct
		//{
		//	// TOMAS: UNDONE:
		//	int					unk01;
		//	int					unk02;
		//	int					unk03;
		//
		//	int					numverts;		// number of unique vertices
		//	int					vertindex;		// vertex vec3_t (data)
		//
		//	int					numnorms;		// number of unique surface normals
		//	int					normindex;		// normal vec3_t (data)
		//
		//} mstudiomodeldata_t;

		public int unknown01;
		public int unknown02;
		public int unknown03;

		public int vertexCount;
		public int vertexOffset;

		public int normalCount;
		public int normalOffset;


		public List<SourceVector> theNormals;
		public List<SourceVector> theVertexes;

	}

}