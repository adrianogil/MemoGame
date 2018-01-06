Shader "Custom/MemoShader" {
	Properties {
		_FrontColor ("Color", Color) = (1,1,1,1)
		_BackColor ("Color", Color) = (1,1,1,1)
	}
	SubShader {
		Pass {
			Tags { "RenderType"="Opaque" }
			LOD 200
			
			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct v2f
	        {
	            float4 vertex : POSITION;
	            float2 uv : TEXCOORD0;
	        };

			float4 _FrontColor;
			float4 _BackColor;

			v2f vert (appdata_base v)
	        {
	            v2f o;
	            o.vertex = UnityObjectToClipPos(v.vertex);
	            o.uv = v.texcoord;
	            return o;
	        }

	        fixed4 frag (v2f i) : SV_Target
	        {
	        	// return float4(i.uv,0,1);

	            if (i.uv.x > 0.5 && i.uv.y > 0.5)
	            {
	            	return _FrontColor;
	            } else {
	            	return _BackColor;
	            }

	            return float4(0,0,0,1);
	        }
			
			ENDCG
		}
	}
	FallBack "Diffuse"
}
