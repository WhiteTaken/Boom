Shader "Hidden/EzComponents/DepthOutput"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float4 scrPos:TEXCOORD1;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
				o.scrPos = ComputeScreenPos(o.pos);
				o.scrPos.y = 1 - o.scrPos.y;
				return o;
			}

			sampler2D _MainTex;
			sampler2D _CameraDepthTexture;

			fixed4 frag (v2f i) : SV_Target
			{
				float depthValue = Linear01Depth (tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.scrPos)).r);
				half4 depth;

				depth.r = depthValue;
				depth.g = depthValue;
				depth.b = depthValue;

				depth.a = 1;
				return depth;
			}
			ENDCG
		}
	}
}
