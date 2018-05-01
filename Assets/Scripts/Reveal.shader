Shader "Custom/Reveal" {
	Properties {
		[HideInInspector]_DrawingTex("Drawing texture", 2D) = "" {}

		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _Outline ("Outline width", Range (0, 0.1)) = .005
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 300
		
		Pass
        {
            // Pass drawing outline
            Cull Front
           
            Blend SrcAlpha OneMinusSrcAlpha
               
            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag
               
            uniform float _Outline;
            uniform float4 _OutlineColor;
            uniform float4 _MainTex_ST;
            uniform sampler2D _MainTex;
			uniform sampler2D _DrawingTex;
     
            struct v2f
            {
                float4 pos : POSITION;
                float4 color : COLOR;
            };
               
            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos (v.vertex);
                float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
                float2 offset = TransformViewToProjection(norm.xy);

				float drawData = tex2Dlod(_DrawingTex, v.texcoord ).a;

                o.pos.xy += offset  * _Outline;
                o.color =  lerp((1,1,1,1), _OutlineColor, drawData);
                return o;
            }
               
            half4 frag(v2f i) :COLOR
            {
                return i.color;
            }
                       
            ENDCG
        }

		CGPROGRAM
		#pragma surface surf Standard
		#pragma target 3.0

		sampler2D _DrawingTex;
		sampler2D _MainTex;
		float4 _Color;

		struct Input {
			float2 uv_DrawingTex;	
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float4 drawData = tex2D(_DrawingTex, IN.uv_DrawingTex);
			float4 mainData = tex2D(_MainTex, IN.uv_MainTex) * _Color;

			fixed4 c = lerp((1,1,1,1), mainData, drawData.a);
			o.Albedo = c.rgb;
			// Reset other values
			o.Metallic = 0.0;
			o.Smoothness = 0.0;
			o.Alpha = 1.0;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
