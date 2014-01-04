Shader "Custom/WOW" {
	Properties {
		decal ("Base", 2D) = "white" {}
		breastplate("Breastplate", 2D) = "white"{}
		handguard("Handguard", 2D) = "white"{}
		legguard("Legguard", 2D) = "white"{}
	}
	SubShader {
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			struct VO{
				float4 position : SV_POSITION;
				float4 color : COLOR;
				float4 texCoord : TEXCOORD0;
			};
			
			struct FO
			{
				float4 color : COLOR;
			};
			
			VO vert(float4 position:POSITION, float4 color:COLOR, float4 texcoord:TEXCOORD0)
			{
				 VO _out;
			 	_out.position = mul(UNITY_MATRIX_MVP, position);	
			 	_out.color = color;
			 	_out.texCoord = texcoord;
			 	return _out;
			}
			
			FO frag(float4 texcoord : TEXCOORD0, sampler2D decal, 
					sampler2D breastplate, 
					sampler2D handguard, 
					sampler2D legguard
					)
			{
				FO _out;
				float texcoordX = texcoord.x;
				float texcoordY = texcoord.y;
				
				_out.color = float4(1, 1, 1, 1);
				
				// breastplate
				if(((texcoordX < 0.5f) && (texcoordY >= 0.75)) || ((texcoordX >= 0.5f) && (texcoordY >= 0.625f))){
					_out.color = tex2D(breastplate, texcoord.xy);
				}
				// handguard
				else if((texcoordX < 0.5f) && (texcoordY >= 0.375f) && (texcoordY < 0.75f)){
					_out.color = tex2D(handguard, texcoord.xy);
				}
				// legguard
				else if((texcoordX >= 0.5f) && (texcoordY < 0.625f)){
					_out.color = tex2D(legguard, texcoord.xy);
				}
				else{
					_out.color = tex2D(decal, texcoord.xy);
				}
				
				
				return _out;
			}
			
			ENDCG
		}
		
	}
}
