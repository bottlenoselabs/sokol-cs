// https://www.shadertoy.com/view/4lK3Rc
// Created by inigo quilez - iq/2017
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

// Code for the making of this video: https://www.youtube.com/watch?v=aNR4n0i2ZlM


// #if HW_PERFORMANCE==0
#define AA 1
// #else
// #define AA 2
// #endif

float hash1( float n )
{
    return fract(sin(n)*43758.5453123);
}


const float PI = 3.1415926535897932384626433832795;
const float PHI = 1.6180339887498948482045868343656;

vec3 forwardSF( float i, float n) 
{
    float phi = 2.0*PI*fract(i/PHI);
    float zi = 1.0 - (2.0*i+1.0)/n;
    float sinTheta = sqrt( 1.0 - zi*zi);
    return vec3( cos(phi)*sinTheta, sin(phi)*sinTheta, zi);
}

float almostIdentity( float x, float m, float n )
{
    if( x>m ) return x;
    float a = 2.0*n - m;
    float b = 2.0*m - 3.0*n;
    float t = x/m;
    return (a*t + b)*t*t + n;
}


vec2 map( vec3 q )
{
    q *= 100.0;

    vec2 res = vec2( q.y, 2.0 );


    float r = 15.0;
    q.y -= r;
    float ani = pow( 0.5+0.5*sin(6.28318*iTime + q.y/25.0), 4.0 );
    q *= 1.0 - 0.2*vec3(1.0,0.5,1.0)*ani;
    q.y -= 1.5*ani;
    float x = abs(q.x);
    
    // x = almostIdentity( x, 1.0, 0.5 ); // remove discontinuity (http://www.iquilezles.org/www/articles/functions/functions.htm)

        
    float y = q.y;
    float z = q.z;
    y = 4.0 + y*1.2 - x*sqrt(max((20.0-x)/15.0,0.0));
    z *= 2.0 - y/15.0;
    float d = sqrt(x*x+y*y+z*z) - r;
    d = d/3.0;
    if( d<res.x ) res = vec2( d, 1.0 );
    
    res.x /= 100.0;
    return res;
}

vec2 intersect( in vec3 ro, in vec3 rd )
{
	const float maxd = 1.0;

    vec2 res = vec2(0.0);
    float t = 0.2;
    for( int i=0; i<300; i++ )
    {
	    vec2 h = map( ro+rd*t );
        if( (h.x<0.0) || (t>maxd) ) break;
        t += h.x;
        res = vec2( t, h.y );
    }

    if( t>maxd ) res=vec2(-1.0);
	return res;
}

vec3 calcNormal( in vec3 pos )
{
    vec3 eps = vec3(0.005,0.0,0.0);
	return normalize( vec3(
           map(pos+eps.xyy).x - map(pos-eps.xyy).x,
           map(pos+eps.yxy).x - map(pos-eps.yxy).x,
           map(pos+eps.yyx).x - map(pos-eps.yyx).x ) );
}

float calcAO( in vec3 pos, in vec3 nor )
{
	float ao = 0.0;
    for( int i=0; i<64; i++ )
    {
        vec3 kk;
        vec3 ap = forwardSF( float(i), 64.0 );
		ap *= sign( dot(ap,nor) ) * hash1(float(i));
        ao += clamp( map( pos + nor*0.01 + ap*0.2 ).x*20.0, 0.0, 1.0 );
    }
	ao /= 64.0;
	
    return clamp( ao, 0.0, 1.0 );
}

vec3 render( in vec2 p )
{
    //-----------------------------------------------------
    // camera
    //-----------------------------------------------------
	
	float an = 0.1*iTime;

	vec3 ro = vec3(0.4*sin(an),0.25,0.4*cos(an));
    vec3 ta = vec3(0.0,0.15,0.0);
    // camera matrix
    vec3 ww = normalize( ta - ro );
    vec3 uu = normalize( cross(ww,vec3(0.0,1.0,0.0) ) );
    vec3 vv = normalize( cross(uu,ww));
	// create view ray
	vec3 rd = normalize( p.x*uu + p.y*vv + 1.7*ww );


    //-----------------------------------------------------
	// render
    //-----------------------------------------------------
    
	vec3 col = vec3(1.0,0.9,0.7);

	// raymarch
    vec3 uvw;
    vec2 res = intersect(ro,rd);
    float t = res.x;

    if( t>0.0 )
    {
        vec3 pos = ro + t*rd;
        vec3 nor = calcNormal(pos);
		vec3 ref = reflect( rd, nor );
        float fre = clamp( 1.0 + dot(nor,rd), 0.0, 1.0 );
        
        float occ = calcAO( pos, nor ); occ = occ*occ;

        if( res.y<1.5 ) // heart
        {
            col = vec3(0.9,0.02,0.01);
            col = col*0.72 + 0.2*fre*vec3(1.0,0.8,0.2);
            
            vec3 lin  = 4.0*vec3(0.7,0.80,1.00)*(0.5+0.5*nor.y)*occ;
                 lin += 0.8*fre*vec3(1.0,1.0,1.00)*(0.6+0.4*occ);
            col = col * lin;
            col += 4.0*vec3(0.8,0.9,1.00)*smoothstep(0.0,0.4,ref.y)*(0.06+0.94*pow(fre,5.0))*occ;

            col = pow(col,vec3(0.4545));
        }
        else // ground
        {
            col *= clamp(sqrt(occ*1.8),0.0,1.0);
        }
    }

    col = clamp(col,0.0,1.0);
	return col;
}
void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
#if AA>1
    vec3 col = vec3(0.0);
    for( int m=0; m<AA; m++ )
    for( int n=0; n<AA; n++ )
    {
        vec2 px = fragCoord + vec2(float(m),float(n))/float(AA);
        vec2 p = (2.0*px-iResolution.xy)/iResolution.y;
    	col += render( p );    
    }
    col /= float(AA*AA);
    
#else
    vec2 p = (2.0*fragCoord-iResolution.xy)/iResolution.y;

    vec3 col = render( p );
#endif    
    
    vec2 q = fragCoord/iResolution.xy;
    col *= 0.2 + 0.8*pow(16.0*q.x*q.y*(1.0-q.x)*(1.0-q.y),0.2);
    
    fragColor = vec4( col, 1.0 );
}