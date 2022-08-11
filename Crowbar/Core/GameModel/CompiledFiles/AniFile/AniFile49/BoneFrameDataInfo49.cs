using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Crowbar
{
	public class BoneFrameDataInfo49
	{

		//Public theConstantRawPos As SourceVector48bits
		//Public theConstantRawRot As SourceQuaternion48bits
		public SourceVector48bits theAnimPosition;
		public SourceQuaternion48bits theAnimRotation;
		public SourceVector theFullAnimPosition;
		//Public theFullAnimUnknown01 As Byte
		//'Public theFullAnimUnknown01 As Double
		//'Public theFullAnimUnknown01 As SourceQuaternion48bits
		//'Public theFullAnimUnknown01 As SourceVector
		//'Public theFullAnimUnknown01 As SourceQuaternion64bits
		//Public theFullAnimUnknown02 As SourceQuaternion64bits
		//Public theAnimRotationUnknown As SourceQuaternion48bitsSmallest3
		//Public theAnimRotationUnknown As SourceQuaternion48bitsAssumedW
		public SourceQuaternion48bitsViaBytes theAnimRotationUnknown;

	}

}