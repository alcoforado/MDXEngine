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

    public Save(key:string, object: any) {
        this.$wpf.postSync("persistence/save", { key: key, json: JSON.stringify(object) });
    }

    public Load(key:string): any {
        var json = <string> this.$wpf.postSync("persistence/load", { key: key });
        var value = JSON.parse(json);
        return value;
    }

    public SetObject(key: string, object: any) {
        var json = <string> this.$wpf.postSync("persistence/load", { key: key });
        var value = JSON.parse(json);
        this.DeepCopy(value, object);
        return object;
    }



}