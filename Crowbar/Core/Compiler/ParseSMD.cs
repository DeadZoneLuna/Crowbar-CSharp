using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Crowbar
{
	//TODO: Unused? Maybe this can be used in Crowbar in the future (but hasn't been updated for a long time...)
	internal static class ParseSMD
	{

		public static List<ParseMeshSmdInfo> ParseMeshSmdFiles(string pathOrPathFileName, ref int materialCount)
		{
			infos = new List<ParseMeshSmdInfo>();

			materialCount = 0;

			if (File.Exists(pathOrPathFileName))
			{
				ParseMeshSmdFile(pathOrPathFileName, ref materialCount);
			}
			else if (Directory.Exists(pathOrPathFileName))
			{
				foreach (string aPathFileName in Directory.GetFiles(pathOrPathFileName, "*.smd"))
				{
					if (File.Exists(aPathFileName))
					{
						ParseMeshSmdFile(aPathFileName, ref materialCount);
					}
				}
			}

			return infos;
		}

		//TODO: ParseMeshSmdFile(). 
		public static void ParseMeshSmdFile(string pathFileName, ref int materialCount)
		{
			info = new ParseMeshSmdInfo();
			infos.Add(info);

			int tokenCount = 0;

			commandsRemaining = new List<string>();
			commandsRemaining.Add("version");
			commandsRemaining.Add("nodes");
			commandsRemaining.Add("skeleton");
			commandsRemaining.Add("triangles");

			//NOTE: This code is converted and modified a little from Valve's MDL v48 studiomdl.exe source code: src_main\utils\studiomdl\v1support.cpp > Load_SMD().

			outputFileStream = new StreamReader(pathFileName);
			try
			{
				string command = "";
				int commandOption = 0;

				info.lineCount = 0;
				while (GetLineInput())
				{
					tokenCount = GetCommandAndOption(ref command, ref commandOption);
					if (tokenCount == 0)
					{
						continue;
					}

					if (command.ToLower() == "version")
					{
						if (commandOption != 1)
						{
							info.messages.Add("ERROR: Incorrect version on line " + info.lineCount.ToString() + ". Must be: version 1");
							break;
						}
						commandsRemaining.Remove("version");

					}
					else if (command.ToLower() == "nodes")
					{
						info.boneCount = GrabNodes();
						commandsRemaining.Remove("nodes");

					}
					else if (command.ToLower() == "skeleton")
					{
						//			Grab_Animation( psource, "BindPose" );
						commandsRemaining.Remove("skeleton");

					}
					else if (command.ToLower() == "triangles")
					{
						//			Grab_Triangles( psource );
						commandsRemaining.Remove("triangles");

					}
					else if (command == "//" || command == ";" || command == "#")
					{
						continue;

					}
					else
					{
						string commandsRemainingText = string.Join(" ", commandsRemaining);
						info.messages.Add("ERROR: Unknown command on line " + info.lineCount.ToString() + ": " + command);
						info.messages.Add("       Expected one of these commands: " + commandsRemainingText);
						break;
					}
				}
			}
			catch (Exception ex)
			{
				info.messages.Add("WARNING: Exception raised on line " + info.lineCount.ToString() + ": " + ex.Message);
			}
			finally
			{
				outputFileStream.Close();
			}
		}

		private static bool GetLineInput()
		{
			while (outputFileStream.Peek() >= 0)
			{
				line = outputFileStream.ReadLine();
				info.lineCount += 1;
				// Skip comments.
				if (!line.StartsWith("//"))
				{
					return true;
				}
			}
			return false;
		}

		private static int GetCommandAndOption(ref string command, ref int commandOption)
		{
			int tokenCount = 0;

			string[] tokens = line.Split(whitespaceSeparators, StringSplitOptions.RemoveEmptyEntries);
			if (tokens.Length > 0)
			{
				command = tokens[0];
				tokenCount += 1;

				if (tokens.Length > 1)
				{
					try
					{
						commandOption = int.Parse(tokens[1]);
						tokenCount += 1;
					}
					catch (Exception ex)
					{
						commandOption = 0;
						return tokenCount;
					}
				}
			}

			return tokenCount;
		}

		private static int GrabNodes()
		{
			int boneCount = 0;
			int tokenCount = 0;

			int boneIndex = 0;
			string boneName = "";
			int parentBoneIndex = 0;

			while (GetLineInput())
			{
				tokenCount = GetBoneIndexAndNameAndParent(ref boneIndex, ref boneName, ref parentBoneIndex);
				if (tokenCount == 3)
				{
					//TODO: Show warning about clipped boneName longer than 1023 characters. Valve source code shows boneName using max of 1024 characters, including null end byte.
					//	char name[1024];

					if (info.boneNames.Contains(boneName))
					{
						info.messages.Add("WARNING: Duplicate bone name of \"" + boneName + "\" at line " + info.lineCount.ToString());
					}
					else
					{
						info.boneNames.Add(boneName);
						if (boneIndex > boneCount)
						{
							boneCount = boneIndex;
						}
					}
				}
				else
				{
					return boneCount + 1;
				}
			}

			info.messages.Add("ERROR: Unexpected end of file at line " + info.lineCount.ToString());
			return 0;
		}

		private static int GetBoneIndexAndNameAndParent(ref int boneIndex, ref string boneName, ref int parentBoneIndex)
		{
			int tokenCount = 0;

			string[] tokens = line.Split(whitespaceSeparators, StringSplitOptions.RemoveEmptyEntries);
			if (tokens.Length > 0)
			{
				try
				{
					boneIndex = int.Parse(tokens[0]);
					tokenCount += 1;
				}
				catch (Exception ex)
				{
					boneIndex = 0;
				}

				if (tokens.Length > 1)
				{
					boneName = tokens[1];
					tokenCount += 1;

					if (tokens.Length > 2)
					{
						try
						{
							parentBoneIndex = int.Parse(tokens[2]);
							tokenCount += 1;
						}
						catch (Exception ex)
						{
							parentBoneIndex = 0;
						}
					}
				}
			}

			return tokenCount;
		}

		//void Grab_Animation( s_source_t *pSource, const char *pAnimName )
		//{
		//	Vector pos;
		//	RadianEuler rot;
		//	char cmd[1024];
		//	int index;
		//	int	t = -99999999;
		//	int size;
		//
		//	s_sourceanim_t *pAnim = FindOrAddSourceAnim( pSource, pAnimName );
		//	pAnim->startframe = -1;
		//
		//	size = pSource->numbones * sizeof( s_bone_t );
		//
		//	while ( GetLineInput() ) 
		//	{
		//		if ( sscanf( g_szLine, "%d %f %f %f %f %f %f", &index, &pos[0], &pos[1], &pos[2], &rot[0], &rot[1], &rot[2] ) == 7 )
		//		{
		//			if ( pAnim->startframe < 0 )
		//			{
		//				MdlError( "Missing frame start(%d) : %s", g_iLinecount, g_szLine );
		//			}
		//
		//			scale_vertex( pos );
		//			VectorCopy( pos, pAnim->rawanim[t][index].pos );
		//			VectorCopy( rot, pAnim->rawanim[t][index].rot );
		//
		//			clip_rotations( rot ); // !!!
		//			continue;
		//		}
		//		
		//		if ( sscanf( g_szLine, "%1023s %d", cmd, &index ) == 0 )
		//		{
		//			MdlError( "MdlError(%d) : %s", g_iLinecount, g_szLine );
		//			continue;
		//		}
		//
		//		if ( !Q_stricmp( cmd, "time" ) ) 
		//		{
		//			t = index;
		//			if ( pAnim->startframe == -1 )
		//			{
		//				pAnim->startframe = t;
		//			}
		//			if ( t < pAnim->startframe )
		//			{
		//				MdlError( "Frame MdlError(%d) : %s", g_iLinecount, g_szLine );
		//			}
		//			if ( t > pAnim->endframe )
		//			{
		//				pAnim->endframe = t;
		//			}
		//			t -= pAnim->startframe;
		//
		//			if ( t >= pAnim->rawanim.Count())
		//			{
		//				s_bone_t *ptr = NULL;
		//				pAnim->rawanim.AddMultipleToTail( t - pAnim->rawanim.Count() + 1, &ptr );
		//			}
		//
		//			if ( pAnim->rawanim[t] != NULL )
		//			{
		//				continue;
		//			}
		//
		//			pAnim->rawanim[t] = (s_bone_t *)kalloc( 1, size );
		//
		//			// duplicate previous frames keys
		//			if ( t > 0 && pAnim->rawanim[t-1] )
		//			{
		//				for ( int j = 0; j < pSource->numbones; j++ )
		//				{
		//					VectorCopy( pAnim->rawanim[t-1][j].pos, pAnim->rawanim[t][j].pos );
		//					VectorCopy( pAnim->rawanim[t-1][j].rot, pAnim->rawanim[t][j].rot );
		//				}
		//			}
		//			continue;
		//		}
		//		
		//		if ( !Q_stricmp( cmd, "end" ) ) 
		//		{
		//			pAnim->numframes = pAnim->endframe - pAnim->startframe + 1;
		//
		//			for ( t = 0; t < pAnim->numframes; t++ )
		//			{
		//				if ( pAnim->rawanim[t] == NULL)
		//				{
		//					MdlError( "%s is missing frame %d\n", pSource->filename, t + pAnim->startframe );
		//				}
		//			}
		//
		//			Build_Reference( pSource, pAnimName );
		//			return;
		//		}
		//
		//		MdlError( "MdlError(%d) : %s", g_iLinecount, g_szLine );
		//	}
		//
		//	MdlError( "unexpected EOF: %s\n", pSource->filename );
		//}

		private static void GrabAnimation()
		{
			int tokenCount = 0;

			int boneIndex = 0;
			SourceVector position = new SourceVector();
			SourceVector rotation = new SourceVector();
			string command = "";
			int frameIndex = 0;

			//	int	t = -99999999;
			//	int size;
			//
			//	s_sourceanim_t *pAnim = FindOrAddSourceAnim( pSource, pAnimName );
			//	pAnim->startframe = -1;
			//
			//	size = pSource->numbones * sizeof( s_bone_t );
			//
			while (GetLineInput())
			{
				tokenCount = GetAnimationBoneIndexAndPositionAndRotation(ref boneIndex, ref position, ref rotation);
				if (tokenCount == 7)
				{
					//			if ( pAnim->startframe < 0 )
					//			{
					//				MdlError( "Missing frame start(%d) : %s", g_iLinecount, g_szLine );
					//			}
					//
					//			scale_vertex( pos );
					//			VectorCopy( pos, pAnim->rawanim[t][index].pos );
					//			VectorCopy( rot, pAnim->rawanim[t][index].rot );
					//
					//			clip_rotations( rot ); // !!!
					continue;
				}

				//		if ( sscanf( g_szLine, "%1023s %d", cmd, &index ) == 0 )
				tokenCount = GetAnimationTimeAndIndex(ref command, ref frameIndex);
				if (tokenCount == 0)
				{
					//			MdlError( "MdlError(%d) : %s", g_iLinecount, g_szLine );
					continue;
				}

				if (command.ToLower() == "time")
				{
					//			t = index;
					//			if ( pAnim->startframe == -1 )
					//			{
					//				pAnim->startframe = t;
					//			}
					//			if ( t < pAnim->startframe )
					//			{
					//				MdlError( "Frame MdlError(%d) : %s", g_iLinecount, g_szLine );
					//			}
					//			if ( t > pAnim->endframe )
					//			{
					//				pAnim->endframe = t;
					//			}
					//			t -= pAnim->startframe;
					//
					//			if ( t >= pAnim->rawanim.Count())
					//			{
					//				s_bone_t *ptr = NULL;
					//				pAnim->rawanim.AddMultipleToTail( t - pAnim->rawanim.Count() + 1, &ptr );
					//			}
					//
					//			if ( pAnim->rawanim[t] != NULL )
					//			{
					//				continue;
					//			}
					//
					//			pAnim->rawanim[t] = (s_bone_t *)kalloc( 1, size );
					//
					//			// duplicate previous frames keys
					//			if ( t > 0 && pAnim->rawanim[t-1] )
					//			{
					//				for ( int j = 0; j < pSource->numbones; j++ )
					//				{
					//					VectorCopy( pAnim->rawanim[t-1][j].pos, pAnim->rawanim[t][j].pos );
					//					VectorCopy( pAnim->rawanim[t-1][j].rot, pAnim->rawanim[t][j].rot );
					//				}
					//			}
					continue;
				}

				if (command.ToLower() == "end")
				{
					//			pAnim->numframes = pAnim->endframe - pAnim->startframe + 1;
					//
					//			for ( t = 0; t < pAnim->numframes; t++ )
					//			{
					//				if ( pAnim->rawanim[t] == NULL)
					//				{
					//					MdlError( "%s is missing frame %d\n", pSource->filename, t + pAnim->startframe );
					//				}
					//			}
					//
					//			Build_Reference( pSource, pAnimName );
					//			return;
				}

				//		MdlError( "MdlError(%d) : %s", g_iLinecount, g_szLine );
			}

			//	MdlError( "unexpected EOF: %s\n", pSource->filename );
		}

		//		if ( sscanf( g_szLine, "%d %f %f %f %f %f %f", &index, &pos[0], &pos[1], &pos[2], &rot[0], &rot[1], &rot[2] ) == 7 )
		private static int GetAnimationBoneIndexAndPositionAndRotation(ref int boneIndex, ref SourceVector position, ref SourceVector rotation)
		{
			int tokenCount = 0;

			string[] tokens = line.Split(whitespaceSeparators, StringSplitOptions.RemoveEmptyEntries);
			if (tokens.Length > 0)
			{
				try
				{
					boneIndex = int.Parse(tokens[0]);
					tokenCount += 1;
				}
				catch (Exception ex)
				{
					return tokenCount;
				}

				if (tokens.Length > 1)
				{
					try
					{
						position.x = double.Parse(tokens[1]);
						tokenCount += 1;
					}
					catch (Exception ex)
					{
						return tokenCount;
					}

					if (tokens.Length > 2)
					{
						try
						{
							position.y = double.Parse(tokens[2]);
							tokenCount += 2;
						}
						catch (Exception ex)
						{
							return tokenCount;
						}

						if (tokens.Length > 3)
						{
							try
							{
								position.z = double.Parse(tokens[3]);
								tokenCount += 1;
							}
							catch (Exception ex)
							{
								return tokenCount;
							}

							if (tokens.Length > 4)
							{
								try
								{
									rotation.x = double.Parse(tokens[4]);
									tokenCount += 1;
								}
								catch (Exception ex)
								{
									return tokenCount;
								}

								if (tokens.Length > 5)
								{
									try
									{
										rotation.y = double.Parse(tokens[5]);
										tokenCount += 1;
									}
									catch (Exception ex)
									{
										return tokenCount;
									}

									if (tokens.Length > 6)
									{
										try
										{
											rotation.z = double.Parse(tokens[6]);
											tokenCount += 1;
										}
										catch (Exception ex)
										{
											return tokenCount;
										}
									}
								}
							}
						}
					}
				}
			}

			return tokenCount;
		}

		private static int GetAnimationTimeAndIndex(ref string command, ref int frameIndex)
		{
			int tokenCount = 0;

			string[] tokens = line.Split(whitespaceSeparators, StringSplitOptions.RemoveEmptyEntries);
			if (tokens.Length > 0)
			{
				command = tokens[0];
				tokenCount += 1;

				if (tokens.Length > 1)
				{
					try
					{
						frameIndex = int.Parse(tokens[1]);
						tokenCount += 1;
					}
					catch (Exception ex)
					{
						frameIndex = 0;
						return tokenCount;
					}
				}
			}

			return tokenCount;
		}

		//void Grab_Triangles( s_source_t *psource )
		//{
		//	int		i;
		//	Vector	vmin, vmax;
		//
		//	vmin[0] = vmin[1] = vmin[2] = 99999;
		//	vmax[0] = vmax[1] = vmax[2] = -99999;
		//
		//	g_numfaces = 0;
		//	numvlist = 0;
		// 
		//	//
		//	// load the base triangles
		//	//
		//	int texture;
		//	int material;
		//	char texturename[MAX_PATH];
		//
		//	while (1) 
		//	{
		//		if (!GetLineInput()) 
		//			break;
		//
		//		// check for end
		//		if (IsEnd( g_szLine )) 
		//			break;
		//
		//		// Look for extra junk that we may want to avoid...
		//		int nLineLength = strlen( g_szLine );
		//		if (nLineLength >= sizeof( texturename ))
		//		{
		//			MdlWarning("Unexpected data at line %d, (need a texture name) ignoring...\n", g_iLinecount );
		//			continue;
		//		}
		//
		//		// strip off trailing smag
		//		strncpy( texturename, g_szLine, sizeof( texturename ) - 1 );
		//		for (i = strlen( texturename ) - 1; i >= 0 && ! isgraph( texturename[i] ); i--)
		//		{
		//		}
		//		texturename[i + 1] = '\0';
		//
		//		// funky texture overrides
		//		for (i = 0; i < numrep; i++)  
		//		{
		//			if (sourcetexture[i][0] == '\0') 
		//			{
		//				strcpy( texturename, defaulttexture[i] );
		//				break;
		//			}
		//			if (stricmp( texturename, sourcetexture[i]) == 0) 
		//			{
		//				strcpy( texturename, defaulttexture[i] );
		//				break;
		//			}
		//		}
		//
		//		if (texturename[0] == '\0')
		//		{
		//			// weird source problem, skip them
		//			GetLineInput();
		//			GetLineInput();
		//			GetLineInput();
		//			continue;
		//		}
		//
		//		if (stricmp( texturename, "null.bmp") == 0 || stricmp( texturename, "null.tga") == 0 || stricmp( texturename, "debug/debugempty" ) == 0)
		//		{
		//			// skip all faces with the null texture on them.
		//			GetLineInput();
		//			GetLineInput();
		//			GetLineInput();
		//			continue;
		//		}
		//
		//		texture = LookupTexture( texturename, ( g_smdVersion > 1 ) );
		//		psource->texmap[texture] = texture;	// hack, make it 1:1
		//		material = UseTextureAsMaterial( texture );
		//
		//		s_face_t f;
		//		ParseFaceData( psource, material, &f );
		//
		//		// remove degenerate triangles
		//		if (f.a == f.b || f.b == f.c || f.a == f.c)
		//		{
		//			// printf("Degenerate triangle %d %d %d\n", f.a, f.b, f.c );
		//			continue;
		//		}
		//	
		//		g_src_uface[g_numfaces] = f;
		//		g_face[g_numfaces].material = material;
		//		g_numfaces++;
		//	}
		//
		//	BuildIndividualMeshes( psource );
		//}

		private static List<ParseMeshSmdInfo> infos;
		private static ParseMeshSmdInfo info;

		private static StreamReader outputFileStream;
		private static string line;
		private static List<string> commandsRemaining;
		//NOTE: Empty array means all whitespace in Split().
		private static char[] whitespaceSeparators = {};

	}
}