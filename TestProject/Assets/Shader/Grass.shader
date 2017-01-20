Shader "Custom/Grass" {
	Properties{
		_Scale("Scale", Range(0.0,100.0)) = 100.0
		_Color("Color", Color) = (1.0,1.0,1.0)
		_Scale2("Scale2", Range(0.0,5.0)) = 1.0
	}
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		
		float _Scale;
		float3 _Color;
		float _Scale2;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		float hash(float n)
		{
			return frac(sin(n)*43758.5453);
		}

		float noise(float3 x)
		{
			// The noise function returns a value in the range -1.0f -> 1.0f

			float3 p = floor(x);
			float3 f = frac(x);

			f = f*f*(3.0 - 2.0*f);
			float n = p.x + p.y*57.0 + 113.0*p.z;

			return lerp(lerp(lerp(hash(n + 0.0), hash(n + 1.0), f.x),
				lerp(hash(n + 57.0), hash(n + 58.0), f.x), f.y),
				lerp(lerp(hash(n + 113.0), hash(n + 114.0), f.x),
					lerp(hash(n + 170.0), hash(n + 171.0), f.x), f.y), f.z);
		}

		float rand(float3 co)
		{
			return frac(sin(dot(co.xyz, float3(12.9898, 78.233, 45.5432))) * 43758.5453);
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			float alt = 1 - frac(noise(IN.worldPos.z + noise(IN.worldPos * _Scale2))) / 3;
			float3 color = { 0.5,0.2,0.3 };
			float seed = 1-frac(noise(IN.worldPos*_Scale))/7.;

			o.Albedo = _Color*half3(seed,seed,seed)*alt;

			// Metallic and smoothness come from slider variables
			o.Metallic = 0.1;
			o.Smoothness = 0.1;
			o.Alpha = 1;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
