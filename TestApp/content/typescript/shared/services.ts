/// <reference path="./wpf.ts" />



import Interop = require('./wpf');
 

export class Settings {

    constructor(private $wpf: Interop.Wpf) {


    }

    private DeepCopy(src: any, dst: any) {
        if (dst == null || src== null)
            return;
        for (var property in dst) {
            if (src.hasOwnProperty(property) && dst.hasOwnProperty(property)) {
                if (typeof (src[property]) == "object" && typeof (dst[property] == "object")) {
                    this.DeepCopy(src[property], dst[property])
                }
                else
                    dst[property] = src[property];
            }
        }
    }

    public Save(object: any) {
        this.$wpf.postSync("Persistence/Save", { json: JSON.stringify(object) });
    }

    public Load(key:string): any {
        var value = this.$wpf.postSync("Persistence/Load", { key: key });
        return value;
    }

    public SetObject(key: string, object: any) {
        var value = this.$wpf.postSync("Persistence/Load", { key: key });
        this.DeepCopy(value, object);
        return object;
    }



}