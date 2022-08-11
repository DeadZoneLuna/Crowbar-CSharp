using System;
using System.Collections;
using System.Collections.Generic;

namespace Crowbar
{
	public class SourceMdlFlex
	{
		//FROM: SourceEngineXXXX_source\public\studio.h
		//struct mstudioflex_t
		//{
		//	DECLARE_BYTESWAP_DATADESC();
		//	int					flexdesc;	// input value

		//	float				target0;	// zero
		//	float				target1;	// one
		//	float				target2;	// one
		//	float				target3;	// zero

		//	int					numverts;
		//	int					vertindex;

		//	inline	mstudiovertanim_t *pVertanim( int i ) const { Assert( vertanimtype == STUDIO_VERT_ANIM_NORMAL ); return (mstudiovertanim_t *)(((byte *)this) + vertindex) + i; };
		//	inline	mstudiovertanim_wrinkle_t *pVertanimWrinkle( int i ) const { Assert( vertanimtype == STUDIO_VERT_ANIM_WRINKLE ); return  (mstudiovertanim_wrinkle_t *)(((byte *)this) + vertindex) + i; };

		//	inline	byte *pBaseVertanim( ) const { return ((byte *)this) + vertindex; };
		//	inline	int	VertAnimSizeBytes() const { return ( vertanimtype == STUDIO_VERT_ANIM_NORMAL ) ? sizeof(mstudiovertanim_t) : sizeof(mstudiovertanim_wrinkle_t); }

		//	int					flexpair;	// second flex desc
		//	unsigned char		vertanimtype;	// See StudioVertAnimType_t
		//	unsigned char		unusedchar[3];
		//	int					unused[6];
		//};
		public int flexDescIndex;

		public double target0;
		public double target1;
		public double target2;
		public double target3;

		public int vertCount;
		public int vertOffset;

		public int flexDescPartnerIndex;
		public byte vertAnimType;
		public char[] unusedChar = new char[3];
		public int[] unused = new int[6];

		public List<SourceMdlVertAnim> theVertAnims;
		//Enum StudioVertAnimType_t
		//{
		//	STUDIO_VERT_ANIM_NORMAL = 0,
		//	STUDIO_VERT_ANIM_WRINKLE,
		//};
		public byte STUDIO_VERT_ANIM_NORMAL = 0;
		public byte STUDIO_VERT_ANIM_WRINKLE = 1;

		public override string ToString()
		{
			string temp = $"\tflexDescIndex = {flexDescIndex}\n";

			temp += $"\ttarget0 = {target0}\n";
			temp += $"\ttarget1 = {target1}\n";
			temp += $"\ttarget2 = {target2}\n";
			temp += $"\ttarget3 = {target3}\n";

			temp += $"\tvertCount = {vertCount}\n";
			temp += $"\tvertOffset = {vertOffset}\n";

			temp += $"\tflexDescPartnerIndex = {flexDescPartnerIndex}\n";
			temp += $"\tvertAnimType = {vertAnimType}\n";
			for(int i = 0; i < 3; i++)
				temp += $"\tunusedChar({i}) = {(unusedChar[i] == '\0' ? '0' : unusedChar[i])}\n";
			for (int i = 0; i < 6; i++)
				temp += $"\tunused({i}) = {unused[i]}\n";

			return temp;
		}

		public double GetPosition()
        {
			return ((target0 == -11 && target1 == -10) || (target2 == 10 && target3 == 11)) ? 1 : target1;
        }
	}
}