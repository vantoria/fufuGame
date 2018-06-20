﻿Shader "Hidden/cameraShader"
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
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			uniform float _fade;
			uniform float _shake;
			uniform float _haba;

			fixed4 frag (v2f i) : SV_Target
			{
				float2 pxPos = float2(i.uv);
				//pxPos.y += sin(pxPos.x*_shake)*_haba;
				fixed4 col = tex2D(_MainTex, pxPos);

				//フェードアウト
			    col *= _fade;
				return col;
			}
			ENDCG
		}
	}
}
