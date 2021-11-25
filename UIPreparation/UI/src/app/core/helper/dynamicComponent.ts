import { ComponentFactoryResolver, ViewContainerRef } from "@angular/core";
import { DynamicComponent } from "../utilities/dynamicComponent/dynamic.component";
import { DynamicComponentItem } from "../utilities/dynamicComponent/dynamiccomponent-item";

export function loadComponent(ads:DynamicComponentItem,componentFactoryResolver:ComponentFactoryResolver,viewContainerRef:ViewContainerRef)
{
    const componentFactory=componentFactoryResolver.resolveComponentFactory(ads.component)
    viewContainerRef.clear();
    const componentRef = viewContainerRef.createComponent<DynamicComponent>(componentFactory);
    componentRef.instance.data = ads.data;
}
