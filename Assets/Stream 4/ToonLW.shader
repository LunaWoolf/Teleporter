// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Amplify Streams 4/Toon LW"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
		#pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			half filler;
		};

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18912
0;53;1440;752;4353.929;1807.955;4.220135;True;True
Node;AmplifyShaderEditor.CommentaryNode;132;-4226,206;Inherit;False;1109;303;;4;31;78;59;130;Normals;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;134;-962,-1554;Inherit;False;939;597;;7;103;102;99;100;101;104;105;Outline;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;131;-2098,560.5215;Inherit;False;1483;654.4785;;14;54;51;82;42;44;43;45;46;72;53;48;50;28;49;Rim Color;1,1,1,1;0;0
Node;AmplifyShaderEditor.IndirectDiffuseLighting;23;-816,-144;Inherit;False;Tangent;1;0;FLOAT3;0,0,1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;55;-1384.923,-772.3851;Inherit;True;Property;_MainTex;Base Color;0;0;Create;False;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GradientSampleNode;15;-896,-416;Inherit;True;2;0;OBJECT;;False;1;FLOAT;0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;-384,-640;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SmoothstepOpNode;48;-1104,848;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;-0.11;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;165;-1067.293,-542.3909;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;51;-1104,1008;Inherit;False;Property;_RimColor;Rim Color;5;0;Create;True;0;0;0;False;0;False;0,0.5549643,1,0;0.2877352,1,0.7645714,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;56;-384,-304;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GradientNode;16;-1120,-416;Inherit;False;0;5;2;0.254717,0.254717,0.254717,0;0.3584906,0.3584906,0.3584906,0.5000076;0.754717,0.754717,0.754717,0.523537;0.8490566,0.8490566,0.8490566,0.9382315;1,1,1,0.9558862;1,0;1,1;0;1;OBJECT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;50;-784,832;Inherit;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMinOpNode;104;-448,-1152;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;101;-784,-1072;Inherit;False;Property;_DistanceCutoff;Distance Cutoff;4;0;Create;True;0;0;0;False;0;False;0;20;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;49;-1376,928;Inherit;False;Property;_RimFalloff;Rim Falloff;7;0;Create;True;0;0;0;False;0;False;0,0;0,0.4;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;130;-4176,304;Inherit;False;Property;_NormalScale;Normal Scale;2;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;99;-912,-1248;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;105;-192,-1312;Inherit;False;4;4;0;FLOAT3;0,0,0;False;1;FLOAT;1;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;103;-640,-1344;Inherit;False;Property;_OutlineWidth;Outline Width;3;0;Create;True;0;0;0;False;0;False;0;0.005;0;0.1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;109;-192,-896;Inherit;False;Property;_Dimming;Dimming;9;0;Create;True;0;0;0;False;0;False;0.75;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;54;-1366.839,723.1321;Inherit;False;Property;_RimShadow;Rim Shadow;8;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;33;128,-160;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.UnityObjToClipPosHlpNode;100;-688,-1248;Inherit;False;1;0;FLOAT3;0,0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NormalVertexDataNode;102;-640,-1504;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;72;-1329.209,610.5215;Inherit;False;71;mainLight;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.OneMinusNode;46;-1296,848;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;164;-1074.238,-284.4698;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;58;-176,-560;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;53;-1040,672;Inherit;False;3;0;FLOAT3;1,1,1;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;80;-2137.872,-301.6228;Inherit;True;78;worldNormal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.BreakToComponentsNode;147;-1913.875,82.37692;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;145;-3705.764,-266.0402;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;78;-3360,256;Inherit;False;worldNormal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;71;-3552,-256;Inherit;False;mainLight;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;79;-2240,-528;Inherit;True;78;worldNormal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;83;-2105.872,82.37692;Inherit;False;71;mainLight;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;160;-2016,-528;Inherit;False;SRP Additional Light;-1;;17;6c86746ad131a0a408ca599df5f40861;7,6,1,9,1,23,0,26,0,27,0,24,0,25,0;6;2;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;15;FLOAT3;0,0,0;False;14;FLOAT3;1,1,1;False;18;FLOAT;0.5;False;32;FLOAT4;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SaturateNode;45;-1440,848;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;3;-2137.872,-93.62306;Inherit;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;77;-1305.875,-237.6231;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;44;-1568,848;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;12;-1673.875,-237.6231;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0.5;False;2;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.LightAttenuation;135;-3966,-271;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;163;-1324.103,-400.5879;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;31;-3600,256;Inherit;True;True;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LightColorNode;146;-3906.325,-174.5511;Inherit;False;0;3;COLOR;0;FLOAT3;1;FLOAT;2
Node;AmplifyShaderEditor.DotProductOpNode;2;-1833.875,-237.6231;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;43;-1792,960;Float;False;Property;_RimOffset;Rim Offset;6;0;Create;True;0;0;0;False;0;False;0.24;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;59;-3904,256;Inherit;True;Property;_Normal;Normal;1;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;162;-1740.103,-416.5879;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.GetLocalVarNode;82;-2048,928;Inherit;True;78;worldNormal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;155;-1654.108,81.02609;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;42;-1792,848;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;156;-1497.875,98.37692;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;161;-1480.336,-417.9387;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;28;-2048,768;Float;False;World;True;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;108;118.8559,-887.0392;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;184;304,-160;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Amplify Streams 4/Toon LW;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;15;0;16;0
WireConnection;15;1;164;0
WireConnection;57;0;55;0
WireConnection;57;1;15;0
WireConnection;48;0;46;0
WireConnection;48;1;49;1
WireConnection;48;2;49;2
WireConnection;165;0;160;0
WireConnection;165;1;55;0
WireConnection;56;0;55;0
WireConnection;56;1;23;0
WireConnection;50;0;53;0
WireConnection;50;1;48;0
WireConnection;50;2;51;0
WireConnection;104;0;100;4
WireConnection;104;1;101;0
WireConnection;105;0;102;0
WireConnection;105;1;103;0
WireConnection;105;2;104;0
WireConnection;105;3;55;4
WireConnection;33;0;58;0
WireConnection;33;1;50;0
WireConnection;100;0;99;0
WireConnection;46;0;45;0
WireConnection;164;0;77;0
WireConnection;164;1;163;0
WireConnection;58;0;57;0
WireConnection;58;1;56;0
WireConnection;58;2;165;0
WireConnection;53;1;72;0
WireConnection;53;2;54;0
WireConnection;147;0;83;0
WireConnection;145;0;135;0
WireConnection;145;1;146;1
WireConnection;78;0;31;0
WireConnection;71;0;145;0
WireConnection;160;11;79;0
WireConnection;45;0;44;0
WireConnection;77;0;12;0
WireConnection;77;1;156;0
WireConnection;44;0;42;0
WireConnection;44;1;43;0
WireConnection;12;0;2;0
WireConnection;163;0;161;0
WireConnection;163;1;162;2
WireConnection;31;0;59;0
WireConnection;2;0;80;0
WireConnection;2;1;3;0
WireConnection;59;5;130;0
WireConnection;162;0;160;0
WireConnection;155;0;147;0
WireConnection;155;1;147;1
WireConnection;42;0;28;0
WireConnection;42;1;82;0
WireConnection;156;0;155;0
WireConnection;156;1;147;2
WireConnection;161;0;162;0
WireConnection;161;1;162;1
WireConnection;108;0;109;0
WireConnection;108;1;58;0
ASEEND*/
//CHKSM=AEC0F5B9D051614891D1CBE52397BD071A16EDAD