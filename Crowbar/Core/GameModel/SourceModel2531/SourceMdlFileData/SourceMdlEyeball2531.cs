using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class SourceMdlEyeball2531
	{

		//struct mstudioeyeball_t
		//{
		//	int					sznameindex;
		//	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
		//	int		bone;
		//	Vector	org;
		//	float	zoffset;
		//	float	radius;
		//	Vector	up;
		//	Vector	forward;
		//	int		texture;
		//
		//	int		iris_material;
		//	float	iris_scale;
		//	int		glint_material;	// !!!
		//
		//	int		upperflexdesc[3];	// index of raiser, neutral, and lowerer flexdesc that is set by flex controllers
		//	int		lowerflexdesc[3];
		//	float	uppertarget[3];		// angle (radians) of raised, neutral, and lowered lid positions
		//	float	lowertarget[3];
		//	//int		upperflex;	// index of actual flex
		//	//int		lowerflex;
		//
		//	int		upperlidflexdesc;	// index of flex desc that actual lid flexes look to
		//	int		lowerlidflexdesc;
		//
		//	float	pitch[2];	// min/max pitch
		//	float	yaw[2];		// min/max yaw
		//};

		public int nameOffset;

		public int boneIndex;
		public SourceVector org = new SourceVector();
		public double zOffset;
		public double radius;
		public SourceVector up = new SourceVector();
		public SourceVector forward = new SourceVector();

		public int texture;
		public int iris_material;
		public double iris_scale;
		public int glint_material;

		public int[] upperFlexDesc = new int[3];
		public int[] lowerFlexDesc = new int[3];
		public double[] upperTarget = new double[3];
		public double[] lowerTarget = new double[3];

		public int upperLidFlexDesc;
		public int lowerLidFlexDesc;

		public double minPitch;
		public double maxPitch;
		public double minYaw;
		public double maxYaw;


		public string theName;

	}

}