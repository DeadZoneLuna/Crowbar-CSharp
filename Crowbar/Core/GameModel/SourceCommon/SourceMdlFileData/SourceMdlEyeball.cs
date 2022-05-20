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
	public class SourceMdlEyeball
	{

		//FROM: SourceEngine2006+_source\public\studio.h
		//// eyeball
		//struct mstudioeyeball_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int					sznameindex;
		//	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
		//	int		bone;
		//	Vector	org;
		//	float	zoffset;
		//	float	radius;
		//	Vector	up;
		//	Vector	forward;
		//	int		texture;

		//	int		unused1;
		//	float	iris_scale;
		//	int		unused2;

		//	int		upperflexdesc[3];	// index of raiser, neutral, and lowerer flexdesc that is set by flex controllers
		//	int		lowerflexdesc[3];
		//	float	uppertarget[3];		// angle (radians) of raised, neutral, and lowered lid positions
		//	float	lowertarget[3];

		//	int		upperlidflexdesc;	// index of flex desc that actual lid flexes look to
		//	int		lowerlidflexdesc;
		//	int		unused[4];			// These were used before, so not guaranteed to be 0
		//	bool	m_bNonFACS;			// Never used before version 44
		//	char	unused3[3];
		//	int		unused4[7];

		//	mstudioeyeball_t(){}
		//private:
		//	// No copy constructors allowed
		//	mstudioeyeball_t(const mstudioeyeball_t& vOther);
		//};



		public int nameOffset;
		public int boneIndex;
		public SourceVector org;
		public double zOffset;
		public double radius;
		public SourceVector up;
		public SourceVector forward;
		//NOTE: Called mesh in one version, but seems to be only used internally by studiomdl.
		public int texture;

		//FROM: SourceEngine2006_source\public\studio.h
		//int		iris_material;
		public int unused1;
		public double irisScale;
		//FROM: SourceEngine2006_source\public\studio.h
		//int		glint_material;	// !!!
		public int unused2;

		public int[] upperFlexDesc = new int[3];
		public int[] lowerFlexDesc = new int[3];
		public double[] upperTarget = new double[3];
		public double[] lowerTarget = new double[3];

		public int upperLidFlexDesc;
		public int lowerLidFlexDesc;
		public int[] unused = new int[4];
		public byte eyeballIsNonFacs;
		public char[] unused3 = new char[3];
		public int[] unused4 = new int[7];



		public string theName;
		public int theTextureIndex;

	}

}