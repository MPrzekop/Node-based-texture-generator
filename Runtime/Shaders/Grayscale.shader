// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Przekop/TextureGraph/Grayscale"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		[KeywordEnum(PrzekopTextureGraphGrayscaleLuminance,PrzekopTextureGraphClassic,PrzekopTextureGraphOldSchool)] _PrzekopTextureGraphGrayscaleMode("PrzekopTextureGraphGrayscaleMode", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Opaque" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		AlphaToMask Off
		Cull Off
		ColorMask RGBA
		ZWrite Off
		ZTest LEqual
		Offset 0 , 0
		
		
		
		Pass
		{
			Name "Unlit"
			Tags { "LightMode"="ForwardBase" }
			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#pragma shader_feature_local _PRZEKOPTEXTUREGRAPHGRAYSCALEMODE_PRZEKOPTEXTUREGRAPHGRAYSCALELUMINANCE _PRZEKOPTEXTUREGRAPHGRAYSCALEMODE_PRZEKOPTEXTUREGRAPHCLASSIC _PRZEKOPTEXTUREGRAPHGRAYSCALEMODE_PRZEKOPTEXTUREGRAPHOLDSCHOOL


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.zw = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				float2 uv_MainTex = i.ase_texcoord1.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode1 = tex2D( _MainTex, uv_MainTex );
				float grayscale2 = Luminance(tex2DNode1.rgb);
				float grayscale5 = dot(tex2DNode1.rgb, float3(0.299,0.587,0.114));
				float grayscale6 = (tex2DNode1.rgb.r + tex2DNode1.rgb.g + tex2DNode1.rgb.b) / 3;
				#if defined(_PRZEKOPTEXTUREGRAPHGRAYSCALEMODE_PRZEKOPTEXTUREGRAPHGRAYSCALELUMINANCE)
				float staticSwitch3 = grayscale2;
				#elif defined(_PRZEKOPTEXTUREGRAPHGRAYSCALEMODE_PRZEKOPTEXTUREGRAPHCLASSIC)
				float staticSwitch3 = grayscale5;
				#elif defined(_PRZEKOPTEXTUREGRAPHGRAYSCALEMODE_PRZEKOPTEXTUREGRAPHOLDSCHOOL)
				float staticSwitch3 = grayscale6;
				#else
				float staticSwitch3 = grayscale2;
				#endif
				float4 temp_cast_3 = (staticSwitch3).xxxx;
				
				
				finalColor = temp_cast_3;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18935
1362;1087;1119;618;946.1332;288.64;1.3;True;False
Node;AmplifyShaderEditor.SamplerNode;1;-768,0;Inherit;True;Property;_MainTex;MainTex;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCGrayscale;6;-432,160;Inherit;False;2;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCGrayscale;5;-432,80;Inherit;False;1;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCGrayscale;2;-432,0;Inherit;False;0;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;3;-192,48;Inherit;False;Property;_PrzekopTextureGraphGrayscaleMode;PrzekopTextureGraphGrayscaleMode;1;0;Create;True;0;0;0;False;0;False;0;0;0;True;;KeywordEnum;3;PrzekopTextureGraphGrayscaleLuminance;PrzekopTextureGraphClassic;PrzekopTextureGraphOldSchool;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;317.3,-61.19999;Float;False;True;-1;2;ASEMaterialInspector;100;1;Przekop/TextureGraph/Grayscale;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;False;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;True;0;False;-1;True;True;2;False;-1;False;True;True;True;True;True;0;False;-1;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;0;1;True;False;;False;0
WireConnection;6;0;1;0
WireConnection;5;0;1;0
WireConnection;2;0;1;0
WireConnection;3;1;2;0
WireConnection;3;0;5;0
WireConnection;3;2;6;0
WireConnection;0;0;3;0
ASEEND*/
//CHKSM=B0226BD866CAD146B1C596C6B5B9A55C2FECDBA1