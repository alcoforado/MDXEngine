
namespace MDXEngine
    {
        static internal class HLSLResources
    {

		 static public readonly string Color2D_hlsl= @"

// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
struct VS_IN
{
	float4 pos : POSITION;
	float4 col : COLOR;
};

struct PS_IN
{
	float4 pos : SV_POSITION;
	float4 col : COLOR;
};



PS_IN VS(VS_IN input)
{
	PS_IN output = (PS_IN)0;

	output.pos = input.pos;
	output.col = input.col;

	return output;
}

float4 PS(PS_IN input) : SV_Target
{
	return input.col;
}

";

		 static public readonly string Color3D_hlsl= @"// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
struct VS_IN
{
	float4 pos : POSITION;
	float4 col : COLOR;
};

struct PS_IN
{
	float4 pos : SV_POSITION;
	float4 col : COLOR;
};

cbuffer TViewChange : register(b2)
{
	float4x4 worldViewProj;
};

PS_IN VS(VS_IN input)
{
	PS_IN output = (PS_IN)0;

	output.pos = mul(input.pos, worldViewProj);
	output.col = input.col;

	return output;
}

float4 PS(PS_IN input) : SV_Target
{
	return input.col;
}

";

		 static public readonly string Texture2D_hlsl= @"

// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
struct VS_IN
{
	float4 pos : POSITION;
	float2 tex : TEXCOORD;
};

struct PS_IN
{
	float4 pos : SV_POSITION;
	float2 tex : TEXCOORD;
};

Texture2D gTexture;

SamplerState samAnisotropic
{
	Filter = ANISOTROPIC;
	MaxAnisotropy = 4;
	AdressU = WRAP;
	AdressV = WRAP;
};

PS_IN VS(VS_IN input)
{
	PS_IN output = (PS_IN)0;

	output.pos = input.pos;
	output.tex = input.tex;

	return output;
}

float4 PS(PS_IN input) : SV_Target
{
	return gTexture.Sample(samAnisotropic,input.tex);
}

";


    }
}

