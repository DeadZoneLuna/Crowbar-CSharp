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
	public class SourcePhyFileData : SourceFileData
	{

		//FROM: SourceEngine2007\src_main\public\phyfile.h
		//typedef struct phyheader_s
		//{
		//	int		size;
		//	int		id;
		//	int		solidCount;
		//	long	checkSum;	// checksum of source .mdl file
		//} phyheader_t;



		//	int		size;
		public int size;
		//	int		id;
		public int id;
		//	int		solidCount;
		public int solidCount;
		//	long	checkSum;	// checksum of source .mdl file
		public int checksum;



		public string thePhysicsMeshSmdFileName;
		public long theSourcePhyKeyValueDataOffset;
		public List<SourcePhyCollisionData> theSourcePhyCollisionDatas;
		public List<SourcePhyPhysCollisionModel> theSourcePhyPhysCollisionModels;
		public SortedList<int, SourcePhyRagdollConstraint> theSourcePhyRagdollConstraintDescs;
		public List<SourcePhyCollisionPair> theSourcePhyCollisionPairs;
		public bool theSourcePhySelfCollides = true;
		public SourcePhyEditParamsSection theSourcePhyEditParamsSection;
		public SourcePhyPhysCollisionModel theSourcePhyPhysCollisionModelMostUsedValues;
		public string theSourcePhyCollisionText;
		public bool theSourcePhyIsCollisionModel = false;
		public int theSourcePhyMaxConvexPieces;

	}

}