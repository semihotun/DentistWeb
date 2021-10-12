import { ClassGetter } from "@angular/compiler/src/output/output_ast";
import { FormControl, FormGroup } from "@angular/forms";

export function formData(data:any) {
    const form = new FormData();     
    for(let prop of Object.keys(data))
    {
        form.append(prop,data[prop]);
    }
    return form;
}