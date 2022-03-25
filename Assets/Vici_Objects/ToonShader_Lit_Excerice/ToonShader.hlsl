void ToonShader_float(in float3 AmbientLight, in float3 Normal, in float ToonRampSmoothness, in float3 ClipSpacePos, in float3 WorldPos, in float ToonRampTinting, in float4 RimTinting, in float ToonRampOffset, out float3 ToonRampOutput, out float3 Direction)
{
#ifdef SHADERGRAPH_PREVIEW
ToonRampOutput = float3(0.5,0.5,0);
Direction = float3(0.5,0.5,0);

#else
	#if SHADOWS_SCREEN
	half4 shadowCoord = ComputeScreenPos(ClipSpacePos);
	half3 AmbientLight = UNITY_LIGHTMODEL_AMBIENT;

	#else 
	half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);

	#endif

	#if _MAIN_LIGHT_SHADOWS_CASCADE || _MAIN_LIGHT_SHADOWS
		Light light = GetMainLight(shadowCoord);

	#else
		Light light = GetMainLight();
	#endif

	half d = dot(Normal, light.direction) * 0.5 + 0.5;
	
	half toonRamp = smoothstep(ToonRampOffset, ToonRampOffset + ToonRampSmoothness, d);

	toonRamp *= light.shadowAttenuation;

	ToonRampOutput = light.color *  (toonRamp + ToonRampTinting);

	Direction = light.color * (toonRamp + RimTinting);

	Direction = light.direction;
	#endif
}