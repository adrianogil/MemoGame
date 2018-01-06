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
			#pragma geometry geom

			#include "UnityCG.cginc"

			struct v2g
	        {
	            float4 vertex : POSITION;
	            float2 uv : TEXCOORD0;
	        };

	        struct g2f
	        {
	            float4 pos : SV_POSITION;
	            float2 uv : TEXCOORD0;
	            float3 normal : TEXCOORD1;
	        };

			float4 _FrontColor;
			float4 _BackColor;

			v2g vert (appdata_base v)
	        {
	            v2g o;
	            o.vertex = UnityObjectToClipPos(v.vertex);
	            o.uv = v.texcoord;
	            return o;
	        }

			[maxvertexcount(24)]
	        void geom(triangle v2g IN[3], inout TriangleStream<g2f> tristream)
	        {
	        	float3 normal = cross(IN[1].vertex - IN[0].vertex, IN[2].vertex - IN[1].vertex);

	        	g2f o;

	        	for (int i = 0; i < 3; i++)
	        	{
	        		o.pos = IN[i].vertex;
		            o.uv = IN[i].uv;
		            o.normal = normal;

		            tristream.Append(o);	
	        	}
	        }

	        fixed4 frag (g2f i) : SV_Target
	        {
	            if (dot(i.normal, float3(1,0,0)) > 0)
	            {
	            	return _FrontColor;
	            } 
	            else 
	            {
	            	return _BackColor;
	            }

	            return float4(0,0,0,1);
	        }
			
			ENDCG
		}
	}
	FallBack "Diffuse"
}
