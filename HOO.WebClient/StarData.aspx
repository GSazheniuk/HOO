<%@ Page Language="C#" Inherits="HOO.WebClient.StarData" MasterPageFile="~/Main.master" EnableViewState="false" %>
<asp:Content ContentPlaceHolderID="cphBody" id="defBody" runat="server">
<table border="1" class="table">
<tr style="height:25px;">
	<td rowspan="2" width="250px">
		<table border="0" width="100%" class="table">
		<tr>
			<td colspan="3">-<asp:Literal id="ltUniverse" runat="server"></asp:Literal>-</td>
		</tr>
		<tr>
			<td>Tick-<asp:Literal id="ltUniverseTick" runat="server"></asp:Literal>-</td>
			<td>Turn-<asp:Literal id="ltUniverseTurn" runat="server"></asp:Literal>-</td>
			<td>Period-<asp:Literal id="ltUniversePeriod" runat="server"></asp:Literal>-</td>
		</tr>
		<tr>
			<td colspan="3">-<asp:Literal id="ltGalaxy" runat="server"></asp:Literal>-</td>
		</tr>
		<tr>
			<td colspan="3"><div style="position:relative"><div id="starCanvas"></div><div id="starImg" style="position:absolute;z-index:100;left:49px;top:45px;width:150px;height:150px;display:none;"><img id="imgStar" runat="server" width="150" /></div></div></td>
		</tr>
		<tr>
			<td colspan="3">-<asp:Literal id="ltStarName" runat="server"></asp:Literal>-</td>
		</tr>
		<tr>
			<td colspan="3">-<asp:Literal id="ltStarClass" runat="server"></asp:Literal>-</td>
		</tr>
		<tr>
			<td>X: <asp:Literal id="ltX" runat="server"></asp:Literal></td>
			<td>Y: <asp:Literal id="ltY" runat="server"></asp:Literal></td>
			<td>Z: <asp:Literal id="ltZ" runat="server"></asp:Literal></td>
		</tr>
		</table>
<asp:Button id="btnNewStar" runat="server" Text="Add New Star" OnClick="btnNewStar_Click" /><asp:Button id="btnNextStar" runat="server" Text="Show Random Star" OnClick="btnNextStar_Click" /><asp:Button id="btnTurn" runat="server" OnClick="btnTurn_Click" Text="End Turn" />
</td>
	<td width="450px">--Orbital Bodies--</td>
</tr>
<tr><td><asp:GridView id="gvOrbits" runat="server" AutoGenerateColumns="false" ShowHeader="false" OnRowCreated="gvOrbits_OnRowCreated" BorderWidth="0" Width="100%" CssClass="table">
<Columns>
	<asp:TemplateField HeaderText="">
		<ItemTemplate>
			<asp:Literal id="ltOrbitNo" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="">
		<ItemTemplate>
			<asp:Literal id="ltName" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="">
		<ItemTemplate>
			<asp:Literal id="ltSize" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="">
		<ItemTemplate>
			<asp:Literal id="ltClass" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="">
		<ItemTemplate>
			<asp:Literal id="ltBaseAttrs" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
</Columns>
</asp:GridView></td></tr>
<tr><td style="vertical-align:middle"><div id="canvas"></div></td><td>
<asp:GridView id="gvNearestStars" runat="server" AutoGenerateColumns="false" ShowHeader="true" OnRowCreated="gvNearestStars_OnRowCreated" BorderWidth="0" Width="100%" CssClass="table">
<Columns>
	<asp:TemplateField HeaderText="Nearest Stars">
		<ItemTemplate>
			<asp:Literal id="ltName2" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="Class">
		<ItemTemplate>
			<asp:Literal id="ltClass2" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="X">
		<ItemTemplate>
			<asp:Literal id="ltXCoo" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="Y">
		<ItemTemplate>
			<asp:Literal id="ltYCoo" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="Z">
		<ItemTemplate>
			<asp:Literal id="ltZCoo" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="Distance">
		<ItemTemplate>
			<asp:Literal id="ltDistance" runat="server" />
		</ItemTemplate>
	</asp:TemplateField>
</Columns>
</asp:GridView>
</td></tr>
</table>
<br />
<script id="vs" type="not-js">
attribute vec4 position;
void main()	{
  gl_Position = position;
}
</script>
<script id="shadertoy-boilerplate" type="not-js">
#extension GL_OES_standard_derivatives : enable
//#extension GL_EXT_shader_texture_lod : enable
#ifdef GL_ES
precision highp float;
#endif
uniform vec3      iResolution;
uniform float     iGlobalTime;
uniform float     iChannelTime[4];
uniform vec4      iMouse;
uniform vec4      iDate;
uniform float     iSampleRate;
uniform vec3      iChannelResolution[4];
uniform int       iFrame;
uniform float     iTimeDelta;
uniform float     iFrameRate;
struct Channel
{
    vec3  resolution;
    float time;
};
uniform Channel iChannel[4];
uniform sampler2D iChannel0;
uniform sampler2D iChannel1;
uniform sampler2D iChannel2;
uniform sampler2D iChannel3;
void mainImage( out vec4 c,  in vec2 f );

float snoise(vec3 uv, float res)	// by trisomie21
{
	const vec3 s = vec3(1e0, 1e2, 1e4);
	
uv *= res;
	
vec3 uv0 = floor(mod(uv, res))*s;
vec3 uv1 = floor(mod(uv+vec3(1.), res))*s;
	
vec3 f = fract(uv); f = f*f*(3.0-2.0*f);
	
vec4 v = vec4(uv0.x+uv0.y+uv0.z, uv1.x+uv0.y+uv0.z,
              uv0.x+uv1.y+uv0.z, uv1.x+uv1.y+uv0.z);
	
vec4 r = fract(sin(v*1e-3)*1e5);
float r0 = mix(mix(r.x, r.y, f.x), mix(r.z, r.w, f.x), f.y);
	
r = fract(sin((v + uv1.z - uv0.z)*1e-3)*1e5);
float r1 = mix(mix(r.x, r.y, f.x), mix(r.z, r.w, f.x), f.y);
	
return mix(r0, r1, f.z)*2.-1.;
}

float freqs[4];

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	freqs[0] = texture2D( iChannel1, vec2( 0.01, 0.25 ) ).x;
freqs[1] = texture2D( iChannel1, vec2( 0.07, 0.25 ) ).x;
freqs[2] = texture2D( iChannel1, vec2( 0.15, 0.25 ) ).x;
freqs[3] = texture2D( iChannel1, vec2( 0.30, 0.25 ) ).x;

float brightness	= freqs[1] * 0.25 + freqs[2] * 0.25;
float radius		= 0.24 + brightness * 0.2;
float invRadius 	= 1.0/radius;
	
vec3 orange			= vec3( {r}.0/255.0, {g}.0/255.0, {b}.0/255.0 );
vec3 orangeRed		= vec3( 0.8, 0.35, 0.1 );
float time		= iGlobalTime * 0.1;
float aspect	= iResolution.x/iResolution.y;
vec2 uv			= fragCoord.xy / iResolution.xy;
vec2 p 			= -0.5 + uv;
p.x *= aspect;

float fade		= pow( length( 2.0 * p ), 0.5 );
float fVal1		= 1.0 - fade;
float fVal2		= 1.0 - fade;
	
float angle		= atan( p.x, p.y )/6.2832;
float dist		= length(p);
vec3 coord		= vec3( angle, dist, time * 0.1 );
	
float newTime1	= abs( snoise( coord + vec3( 0.0, -time * ( 0.35 + brightness * 0.001 ), time * 0.015 ), 15.0 ) );
float newTime2	= abs( snoise( coord + vec3( 0.0, -time * ( 0.15 + brightness * 0.001 ), time * 0.015 ), 45.0 ) );	
for( int i=1; i<=7; i++ ){
    float power = pow( 2.0, float(i + 1) );
    fVal1 += ( 0.5 / power ) * snoise( coord + vec3( 0.0, -time, time * 0.2 ), ( power * ( 10.0 ) * ( newTime1 + 1.0 ) ) );
    fVal2 += ( 0.5 / power ) * snoise( coord + vec3( 0.0, -time, time * 0.2 ), ( power * ( 25.0 ) * ( newTime2 + 1.0 ) ) );
}
	
float corona		= pow( fVal1 * max( 1.1 - fade, 0.0 ), 2.0 ) * 50.0;
corona				+= pow( fVal2 * max( 1.1 - fade, 0.0 ), 2.0 ) * 50.0;
corona				*= 1.0 - newTime1;
vec3 sphereNormal 	= vec3( 0.0, 0.0, 0.0 );
vec3 dir 			= vec3( 0.0 );
vec3 center			= vec3( 0.1, 0.1, 0.1 );
vec3 starSphere		= vec3( 39.0/255.0, 43.0/255.0, 48.0/255.0 );

vec2 sp = -1.0 + 2.0 * uv;
sp.x *= aspect;
sp *= ( 2.0 - brightness );
float r = dot(sp,sp);
float f = (1.0-sqrt(abs(1.0-r)))/(r) + brightness * 0.5;
if( dist < radius ){
    corona			*= pow( dist * invRadius, 24.0 );
    vec2 newUv;
    newUv.x = sp.x*f;
    newUv.y = sp.y*f;
    newUv += vec2( time, 0.0 );
		
    vec3 texSample 	= vec3(0xff, 0x99, 0);//texture2D( iChannel0, newUv ).rgb;
    float uOff		= ( texSample.g * brightness * 4.5 + time );
    vec2 starUV		= newUv + vec2( uOff, 0.0 );
    starSphere		= orange;//vec3(0xff, 0x99, 0);//texture2D( iChannel0, starUV ).rgb;
}
	
float starGlow	= min( max( 0.1 - dist * ( 1.0 - brightness ), 0.0 ), 1.0 );
    //fragColor.rgb	= vec3( r );
fragColor.rgb	= starSphere + corona*orange + vec3( f * ( 0.0 + brightness * 0.3 ) * orange );//vec3( f * ( 0.75 + brightness * 0.3 ) * orange ) + starSphere + corona * orange;// + starGlow * orangeRed;
fragColor.a		= 1.0;
}

void main( void ){
    vec4 color = vec4(0.0,0.0,0.0,1.0);
    mainImage( color, gl_FragCoord.xy );
    color.w = 1.0;
    gl_FragColor = color;
}
</script>

<asp:Literal id="ltLoadTime" runat="server" />
<asp:Literal ID="ltScript" runat="server" />
    <script>

        function hexToRgb(hex) {
            var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
            return result ? {
                r: parseInt(result[1], 16),
                g: parseInt(result[2], 16),
                b: parseInt(result[3], 16)
            } : null;
        }

        var cube1 = new THREE.Mesh(geometry, material[m1]);
        scene.add(cube1);

        var cube2 = new THREE.Mesh(geometry, material[m2]);
        scene.add(cube2);
        cube2.position.set(x2, y2, z2);

        var cube3 = new THREE.Mesh(geometry, material[m3]);
        scene.add(cube3);
        cube3.position.set(x3, y3, z3);

        var cube4 = new THREE.Mesh(geometry, material[m4]);
        scene.add(cube4);
        cube4.position.set(x4, y4, z4);

        var cube5 = new THREE.Mesh(geometry, material[m5]);
        scene.add(cube5);
        cube5.position.set(x5, y5, z5);

        var cube6 = new THREE.Mesh(geometry, material[m6]);
        scene.add(cube6);
        cube6.position.set(x6, y6, z6);

        camera.position.set(0, 0, 500);

        renderer.setClearColor(0x2e3338, 1);
    </script>

    <script type="text/javascript">
        var SunCamera = new THREE.Camera();
        SunCamera.position.z = 1;

        var SunScene = new THREE.Scene();

        var SunGeometry = new THREE.BufferGeometry();
        var vertices = new Float32Array([
          -1, -1,
           1, -1,
          -1, 1,
          -1, 1,
           1, -1,
           1, 1,
        ]);
        SunGeometry.addAttribute('position', new THREE.BufferAttribute(vertices, 2));

        var SunUniforms = {
            iGlobalTime: { type: "f", value: 1.0 },
            iResolution: { type: "v3", value: new THREE.Vector3() },
            iChannel0: { type: 't', value: THREE.ImageUtils.loadTexture('Images/tex09.jpg') },
        };

        var SunMaterial = new THREE.RawShaderMaterial({
            uniforms: SunUniforms,
            vertexShader: $('#vs').text(),
            fragmentShader: $('#shadertoy-boilerplate').text().replace("{r}", hexToRgb(c1).r).replace("{g}", hexToRgb(c1).g).replace("{b}", hexToRgb(c1).b),
        });

        var SunMesh = new THREE.Mesh(SunGeometry, SunMaterial);
        SunScene.add(SunMesh);

        var SunRenderer = new THREE.WebGLRenderer();
        $("#starCanvas").append(SunRenderer.domElement);

        SunRenderer.setClearColor(0x2e3338, 1);
        function resize(force) {
            var canvas = SunRenderer.domElement;
            var dpr = 1;
            var width = 240;
            var height = 240;
            if (force || width != canvas.width || height != canvas.height) {
                SunRenderer.setSize(width, height, false);
                SunUniforms.iResolution.value.x = SunRenderer.domElement.width;
                SunUniforms.iResolution.value.y = SunRenderer.domElement.height;
            }
        }

        function SunRender(time) {
            SunUniforms.iGlobalTime.value = time * 0.001;
            SunRenderer.render(SunScene, SunCamera);
            requestAnimationFrame(SunRender);
        }

        resize(true);
        render();
        $("#starImg").show();
    </script>
</asp:Content>