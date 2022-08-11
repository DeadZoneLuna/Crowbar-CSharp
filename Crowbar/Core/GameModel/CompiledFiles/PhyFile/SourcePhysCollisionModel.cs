﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourcePhyPhysCollisionModel
	{

		//FROM: SourceEngine2006_source\utils\studiomdl\collisionmodel.cpp
		////-----------------------------------------------------------------------------
		//// Purpose: Contains a single convex element of a physical collision system
		////-----------------------------------------------------------------------------
		//	Class CPhysCollisionModel
		//{
		//public:
		//	CPhysCollisionModel( void )
		//	{
		//		memset( this, 0, sizeof(*this) );
		//	}
		//	const char	*m_parent;
		//	const char	*m_name;
		//	// physical properties stored on disk
		//	float		m_mass;
		//	float		m_volume;
		//	float		m_surfaceArea;
		//	float		m_damping;
		//	float		m_rotdamping;
		//	float		m_inertia;
		//	float		m_dragCoefficient;
		//	// these tune the model building process, they don't go in the file
		//	float		m_massBias;
		//	CPhysCollide	*m_pCollisionData;
		//	CPhysCollisionModel	*m_pNext;
		//};
		//[...]
		//// write out the properties of each solid
		//int solidIndex = 0;
		//pPhys = g_JointedModel.m_pCollisionList;
		//while ( pPhys )
		//{
		//	pPhys->m_mass = ((pPhys->m_volume * pPhys->m_massBias) / volume) * g_JointedModel.m_totalMass;
		//	if ( pPhys->m_mass < 1.0 )
		//		pPhys->m_mass = 1.0;
		//	fprintf( fp, "solid {\n" );
		//	KeyWriteInt( fp, "index", solidIndex );
		//	KeyWriteString( fp, "name", pPhys->m_name );
		//	if ( pPhys->m_parent )
		//	{
		//		KeyWriteString( fp, "parent", pPhys->m_parent );
		//	}
		//	KeyWriteFloat( fp, "mass", pPhys->m_mass );
		//	//KeyWriteFloat( fp, "volume", pPhys->m_volume );
		//	char* pSurfaceProps = GetSurfaceProp( pPhys->m_name );
		//	KeyWriteString( fp, "surfaceprop", pSurfaceProps );
		//	KeyWriteFloat( fp, "damping", pPhys->m_damping );
		//	KeyWriteFloat( fp, "rotdamping", pPhys->m_rotdamping );
		//	if ( pPhys->m_dragCoefficient != -1 )
		//	{
		//		KeyWriteFloat( fp, "drag", pPhys->m_dragCoefficient );
		//	}
		//	KeyWriteFloat( fp, "inertia", pPhys->m_inertia );
		//	KeyWriteFloat( fp, "volume", pPhys->m_volume );
		//	if ( pPhys->m_massBias != 1.0f )
		//	{
		//		KeyWriteFloat( fp, "massbias", pPhys->m_massBias );
		//	}
		//	fprintf( fp, "}\n" );
		//	pPhys = pPhys->m_pNext;
		//	solidIndex++;
		//}

		// Example: 
		//solid {
		//"index" "1"
		//"name" "ValveBiped.Bip01_Spine1"
		//"parent" "ValveBiped.Bip01_Pelvis"
		//"mass" "25.264887"
		//"surfaceprop" "flesh"
		//"damping" "0.050000"
		//"rotdamping" "5.000000"
		//"inertia" "10.000000"
		//"volume" "805.543762"
		//"massbias" "8.000000"
		//}

		public int theIndex;
		public string theName;
		public bool theParentIsValid = false;
		public string theParentName;
		public float theMass;
		public string theSurfaceProp;
		public float theDamping;
		public float theRotDamping;
		public bool theDragCoefficientIsValid = false;
		public float theDragCoefficient;
		public float theInertia;
		public float theVolume;
		public bool theMassBiasIsValid = false;
		public float theMassBias;
		public bool theRollingDragCoefficientIsValid = false;
		public float theRollingDragCoefficient;

	}

}