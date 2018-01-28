Shader "Custom/Dissolve" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "" {}
		_RGBVal("RGB Value", Range(0,1)) = 0.0
		_Lights("Lights", 2D) = "black" {}
		_EmissionVal("Emission", Range(0,0.4)) = 0.0
		_SliceGuide("Slice Guide (RGB)", 2D) = "white" {}
		_SliceAmount("Slice Amount", Range(0.0, 1.0)) = 0.5
		_BurnSize("Burn Size", Range(0.0, 1.0)) = 0.15
		_BurnRamp("Burn Ramp (RGB)", 2D) = "white" {}
	}
		SubShader{
		Tags{"Queue" = "Geometry-100"}
		ZWrite On
		Cull Back
		LOD 200

		Lighting Off
		CGPROGRAM
#pragma surface surf Lambert
		sampler2D _MainTex;
		float _RGBVal;
		sampler2D _Lights;
		float _EmissionVal;
		sampler2D _SliceGuide;
		float _SliceAmount;
		sampler2D _BurnRamp;
		float _BurnSize;

		struct Input
		{
			float2 uv_MainTex;
			float2 uv_Lights;
			float2 uv_SliceGuide;
			float _SliceAmount;
		};

	void surf(Input IN, inout SurfaceOutput o)
	{
		half4 c = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = _RGBVal + _RGBVal + _RGBVal;
		o.Alpha = c.a;

		//for dissolve effect
		o.Emission = _EmissionVal + _EmissionVal + _EmissionVal;
		clip(tex2D(_SliceGuide, IN.uv_SliceGuide).rgb - _SliceAmount);

		//for dissolving edges effect
		half test = tex2D(_SliceGuide, IN.uv_MainTex).rgb - _SliceAmount;
		if (test < _BurnSize && _SliceAmount > 0 && _SliceAmount < 1)
		{
			o.Emission = tex2D(_BurnRamp, float2(test *(1 / _BurnSize), 0));
			o.Albedo *= o.Emission;
		}
	}
	ENDCG
	}
		FallBack "Diffuse"
}