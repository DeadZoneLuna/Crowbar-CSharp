using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourcePhyCollisionData
	{

		//FROM: SourceEngine2006_source\utils\studiomdl\collisionmodel.cpp
		//Class CPhysCollisionModel
		//{
		//public:
		//   [...]
		//	CPhysCollide	*m_pCollisionData;
		//	CPhysCollisionModel	*m_pNext;
		//};
		//CPhysCollisionModel *pPhys = g_JointedModel.m_pCollisionList;

		//pPhys = g_JointedModel.m_pCollisionList;
		//while ( pPhys )
		//{
		//	int size = physcollision->CollideSize( pPhys->m_pCollisionData );
		//	fwrite( &size, sizeof(int), 1, fp );
		//	char *buf = (char *)stackalloc( size );
		//	physcollision->CollideWrite( buf, pPhys->m_pCollisionData );
		//	fwrite( buf, size, 1, fp );
		//	pPhys = pPhys->m_pNext;
		//}

		//	int		size;
		public int size;

		//Public theBoneIndex As Integer
		//Public theFaces As List(Of SourcePhyFace)
		public List<SourcePhyFaceSection> theFaceSections;
		//Public theVertices As List(Of SourceVector)
		public List<SourcePhyVertex> theVertices;

	}

}