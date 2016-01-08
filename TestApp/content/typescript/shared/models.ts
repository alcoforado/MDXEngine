 

export class DXVector3 {
    constructor(public X: number, public Y: number, public Z: number) { }
}

export class DXVector4 {
    constructor(public X: number, public Y: number, public Z: number, public W: number) { }
}




export class DirectionalLightData {
    constructor(
        public Ambient: DXVector4,
        public Diffuse: DXVector4,
        public Specular: DXVector3,
        public SpecPower: number,
        public Direction: DXVector3) { }
}