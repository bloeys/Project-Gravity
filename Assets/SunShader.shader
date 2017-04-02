Shader "My Shaders/SunShader"
{
	Properties
	{
		_mainTex("Texture", 2D) = "white" {}
		_col("Color", Color) = (1, 1, 1, 1)
		_scaleCol("Scale Color", Color) = (0.5, 0.5, 0.5, 0.5)
		_haloRange("Halo Range", Vector) = (0.1, 0.3, 0, 0)
		_haloSpd("Halo Speed Multiplier", Range(0, 5)) = 0.5
		_alpha("Alpha", Range(0, 1)) = 0.5
	}
		
	Subshader
	{
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha	//Allow transparency
		
		//Draw normal sun
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			//Input data
			struct appData
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};
			
			//Output data
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			//Vars
			sampler2D _mainTex;
			fixed4 _col;
			
			v2f vert(appData input)
			{
				v2f output;
				
				output.vertex = mul(UNITY_MATRIX_MVP, input.vertex);
				output.uv = input.uv;
				return output;
			}
			
			fixed4 frag(v2f input) : SV_Target
			{
				return tex2D(_mainTex, input.uv) * _col;
			}
			ENDCG
		}
		
		//Draw transparent halo around sun
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			//Input data
			struct appData
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};
			
			//Output data
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};
			
			//Vars
			sampler2D _mainTex;
			fixed4 _scaleCol;
			fixed _alpha;
			half2 _haloRange;
			half _minHalo, _haloSpd;
			
			v2f vert(appData input)
			{
				v2f output;
				
				input.vertex.xyz += input.normal.xyz * (_haloRange.y * abs(sin(_Time.y * _haloSpd)) + _haloRange.x);	//Oscillate halo
				output.vertex = mul(UNITY_MATRIX_MVP, input.vertex);
				output.uv = input.uv;
				return output;
			}
			
			fixed4 frag(v2f input) : SV_Target
			{
				float4 finalCol = tex2D(_mainTex, input.uv) * _scaleCol;
				finalCol.a = _alpha;
				return finalCol;
			}
			ENDCG
		}
		
	}
}