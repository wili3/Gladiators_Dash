﻿// Toony Colors Pro+Mobile 2
// (c) 2014-2017 Jean Moreno

Shader "Toony Colors Pro 2/User/My TCP2 Shader2"
{
	Properties
	{
	[TCP2HeaderHelp(BASE, Base Properties)]
		//TOONY COLORS
		_Color ("Color", Color) = (0.5,0.5,0.5,1.0)
		_HColor ("Highlight Color", Color) = (0.6,0.6,0.6,1.0)
		_SColor ("Shadow Color", Color) = (0.3,0.3,0.3,1.0)
		
		//DIFFUSE
		_MainTex ("Main Texture (RGB)", 2D) = "white" {}
	[TCP2Separator]
		
		//TOONY COLORS RAMP
		_RampThreshold ("Ramp Threshold", Range(0,1)) = 0.5
		_RampSmooth ("Ramp Smoothing", Range(0.001,1)) = 0.1
	[TCP2Separator]
	
	[TCP2HeaderHelp(NORMAL MAPPING, Normal Bump Map)]
		//BUMP
		_BumpMap ("Normal map (RGB)", 2D) = "bump" {}
	[TCP2Separator]
	
	[TCP2HeaderHelp(SPECULAR, Specular)]
		//SPECULAR
		_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
		_Shininess ("Shininess", Range(0.0,2)) = 0.1
	[TCP2Separator]
	
	[TCP2HeaderHelp(REFLECTION, Reflection)]
		//REFLECTION
		[NoScaleOffset] _Cube ("Reflection Cubemap", Cube) = "_Skybox" {}
	[TCP2Separator]
		
		//Avoid compile error if the properties are ending with a drawer
		[HideInInspector] __dummy__ ("unused", Float) = 0
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		Cull Off
		
		CGPROGRAM
		
		#pragma surface surf ToonyColorsCustom 
		#pragma target 3.0
		
		//================================================================
		// VARIABLES
		
		fixed4 _Color;
		sampler2D _MainTex;
		samplerCUBE _Cube;
		sampler2D _BumpMap;
		fixed _Shininess;
		
		struct Input
		{
			half2 uv_MainTex;
			half2 uv_BumpMap;
			float3 worldRefl;
			INTERNAL_DATA
			float vFace : VFACE;
		};
		
		//================================================================
		// CUSTOM LIGHTING
		
		//Lighting-related variables
		fixed4 _HColor;
		fixed4 _SColor;
		float _RampThreshold;
		float _RampSmooth;
		
		//Custom SurfaceOutput
		struct SurfaceOutputCustom
		{
			fixed3 Albedo;
			fixed3 Normal;
			fixed3 Emission;
			half Specular;
			fixed Gloss;
			fixed Alpha;
			float vFace;
		};
		
		inline half4 LightingToonyColorsCustom (inout SurfaceOutputCustom s, half3 lightDir, half3 viewDir, half atten)
		{
			s.Normal = normalize(s.Normal);
			s.Normal.z *= s.vFace;
			fixed ndl = max(0, dot(s.Normal, lightDir));
			fixed3 ramp = smoothstep(_RampThreshold-_RampSmooth*0.5, _RampThreshold+_RampSmooth*0.5, ndl);
		#if !(POINT) && !(SPOT)
			ramp *= atten;
		#endif
			_SColor = lerp(_HColor, _SColor, _SColor.a);	//Shadows intensity through alpha
			ramp = lerp(_SColor.rgb, _HColor.rgb, ramp);
			//Specular
			half3 h = normalize(lightDir + viewDir);
			float ndh = max(0, dot (s.Normal, h));
			float spec = pow(ndh, s.Specular*128.0) * s.Gloss * 2.0;
			spec *= atten;
			fixed4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * ramp;
		#if (POINT || SPOT)
			c.rgb *= atten;
		#endif
			c.rgb += _LightColor0.rgb * _SpecColor.rgb * spec;
			c.a = s.Alpha + _LightColor0.a * _SpecColor.a * spec;
			return c;
		}

		//================================================================
		// SURFACE FUNCTION

		void surf(Input IN, inout SurfaceOutputCustom o)
		{
			fixed4 mainTex = tex2D(_MainTex, IN.uv_MainTex);
			
			o.Albedo = mainTex.rgb * _Color.rgb;
			o.Alpha = mainTex.a * _Color.a;
			
			//Specular
			o.Gloss = 1;
			o.Specular = _Shininess;
			
			//Normal map
			half4 normalMap = tex2D(_BumpMap, IN.uv_BumpMap.xy);
			o.Normal = UnpackNormal(normalMap);
			
			//Reflection
			half3 worldRefl = WorldReflectionVector(IN, o.Normal);
			fixed4 reflColor = texCUBE(_Cube, worldRefl);
			reflColor *= mainTex.a;
			o.Emission += reflColor.rgb;
			
			//VFace Register (backface lighting)
			o.vFace = IN.vFace;
		}
		
		ENDCG
	}
	
	Fallback "Diffuse"
	CustomEditor "TCP2_MaterialInspector_SG"
}
