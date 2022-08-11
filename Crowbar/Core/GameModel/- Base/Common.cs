using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Crowbar
{
	internal static class Common
	{
		internal static System.Globalization.CultureInfo m_InvCulture = System.Globalization.CultureInfo.InvariantCulture;

		public static SourceVector ToEulerAngles(this SourceQuaternion q)
		{
			SourceVector eulerAngles = new SourceVector();

			// Threshold for the singularities found at the north/south poles.
			const double SINGULARITY_THRESHOLD = 0.4999995;

			var sqw = q.w * q.w;
			var sqx = q.x * q.x;
			var sqy = q.y * q.y;
			var sqz = q.z * q.z;
			var unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
			var singularityTest = (q.x * q.z) + (q.w * q.y);

			if (singularityTest > SINGULARITY_THRESHOLD * unit)
			{
				eulerAngles.z = 2 * Math.Atan2(q.x, q.w);
				eulerAngles.y = Math.PI / 2;
				eulerAngles.x = 0;
			}
			else if (singularityTest < -SINGULARITY_THRESHOLD * unit)
			{
				eulerAngles.z = -2 * Math.Atan2(q.x, q.w);
				eulerAngles.y = -(Math.PI / 2);
				eulerAngles.x = 0;
			}
			else
			{
				eulerAngles.z = Math.Atan2(2 * ((q.w * q.z) - (q.x * q.y)), sqw + sqx - sqy - sqz);
				eulerAngles.y = Math.Asin(2 * singularityTest / unit);
				eulerAngles.x = Math.Atan2(2 * ((q.w * q.x) - (q.y * q.z)), sqw - sqx - sqy + sqz);
			}

			return eulerAngles;
		}

		public enum VecOrder
		{
			XYZ,
			YXZ,
			YZX,
			XZY,
			ZXY,
			ZYX
		}

		#region Invert
		public enum InvertVecOrder
		{
			None,
			XYZ,
			X,
			Y,
			Z,
			XY,
			XZ,
			YZ
		}

		public static SourceVector InvertXYZ(this SourceVector input)
		{
			input.x = -input.x;
			input.y = -input.y;
			input.z = -input.z;
			return input;
		}

		public static SourceVector InvertX(this SourceVector input)
		{
			input.x = -input.x;
			return input;
		}

		public static SourceVector InvertY(this SourceVector input)
		{
			input.y = -input.y;
			return input;
		}

		public static SourceVector InvertZ(this SourceVector input)
		{
			input.z = -input.z;
			return input;
		}

		public static SourceVector InvertXY(this SourceVector input)
		{
			input.x = -input.x;
			input.y = -input.y;
			return input;
		}

		public static SourceVector InvertXZ(this SourceVector input)
		{
			input.x = -input.x;
			input.z = -input.z;
			return input;
		}

		public static SourceVector InvertYZ(this SourceVector input)
		{
			input.y = -input.y;
			input.z = -input.z;
			return input;
		}

		public static SourceVector Invert(this SourceVector input, InvertVecOrder order)
		{
			switch (order)
			{
				default: return input;
				case InvertVecOrder.XYZ: return input.InvertXYZ();
				case InvertVecOrder.X: return input.InvertX();
				case InvertVecOrder.Y: return input.InvertY();
				case InvertVecOrder.Z: return input.InvertZ();
				case InvertVecOrder.XY: return input.InvertXY();
				case InvertVecOrder.XZ: return input.InvertXZ();
				case InvertVecOrder.YZ: return input.InvertYZ();
			}
		}
		#endregion

		public static SourceVector Swap(this SourceVector input, VecOrder order = VecOrder.XYZ)
		{
			switch (order)
			{
				default:
					return new SourceVector
					{
						x = input.x,
						y = input.y,
						z = input.z
					};
				case VecOrder.YXZ:
					return new SourceVector 
					{ 
						x = input.y, 
						y = input.x, 
						z = input.z 
					};
				case VecOrder.YZX:
					return new SourceVector
					{
						x = input.y,
						y = input.z,
						z = input.x
					};
				case VecOrder.XZY:
					return new SourceVector
					{
						x = input.x,
						y = input.z,
						z = input.y
					};
				case VecOrder.ZXY:
					return new SourceVector
					{
						x = input.z,
						y = input.x,
						z = input.y
					};
				case VecOrder.ZYX:
					return new SourceVector
					{
						x = input.z,
						y = input.y,
						z = input.x
					};
			}
		}

		public static string ReadPhyCollisionTextSection(BinaryReader theInputFileReader, long endOffset)
		{
			string result = string.Empty;
			try
			{
				if (endOffset > theInputFileReader.BaseStream.Position)
				{
					//NOTE: Use -1 to avoid including the null terminator character.
					result = new string(theInputFileReader.ReadChars((int)(endOffset - theInputFileReader.BaseStream.Position - 1)));
					// Read the NULL byte to help with debug logging.
					theInputFileReader.ReadChar();
					// Only grab text to the first NULL byte. (Needed for PHY data stored within Titanfall 2 MDL file.)
					result = result.Substring(0, result.IndexOf('\0'));
				}
			}
			catch (Exception)
			{
				int debug = 4242;
			}

			return result;
		}

		public static void ProcessTexturePaths(List<string> theTexturePaths, List<SourceMdlTexture> theTextures, List<string> theModifiedTexturePaths, List<string> theModifiedTextureFileNames)
		{
			if (theTexturePaths != null)
			{
				foreach (string aTexturePath in theTexturePaths)
					theModifiedTexturePaths.Add(aTexturePath);
			}
			if (theTextures != null)
			{
				foreach (SourceMdlTexture aTexture in theTextures)
					theModifiedTextureFileNames.Add(aTexture.thePathFileName);
			}

			if (MainCROWBAR.TheApp.Settings.DecompileRemovePathFromSmdMaterialFileNamesIsChecked)
			{
				//SourceFileNamesModule.CopyPathsFromTextureFileNamesToTexturePaths(theModifiedTexturePaths, theModifiedTextureFileNames)
				SourceFileNamesModule.MovePathsFromTextureFileNamesToTexturePaths(ref theModifiedTexturePaths, ref theModifiedTextureFileNames);
			}
		}

		public static void WriteHeaderComment(StreamWriter outputFileStreamWriter)
		{
			if (!MainCROWBAR.TheApp.Settings.DecompileStricterFormatIsChecked)
				outputFileStreamWriter.WriteLine("// " + MainCROWBAR.TheApp.GetHeaderComment());
		}
	}
}