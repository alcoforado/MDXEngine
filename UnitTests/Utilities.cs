using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDXEngine;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace UnitTests
{
    public class Utilities
    {
        private static DxControl _control;

        public static IDxContext GetDxContext()
        {
            if (_control == null)
            {
                _control = new DxControl(new Form1());

            }
            return _control;
        }

        public static HLSLProgram GetHLSLProgramWithTwoTextureResources()
        {
            var code=@"

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

Texture2D texture1;
Texture2D texture2;

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
	output.col = input.col;

	return output;
}

float4 PS(PS_IN input) : SV_Target
{
    float4 result = texture1.Sample(samAnisotropic,float2(0.0,1.0));
    result+=texture2.Sample(samAnisotropic,float2(0.0,1.0));
    return result;
}

";

            return new HLSLProgram(Utilities.GetDxContext(), code, new[]
                    {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0)
                    });

        }
    }
}
