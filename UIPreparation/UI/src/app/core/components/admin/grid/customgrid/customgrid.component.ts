import { HttpClient } from "@angular/common/http";
import {
  Component,
  ElementRef,
  Injector,
  Input,
  OnInit,
  ViewChild,
} from "@angular/core";
import { QueryString } from "app/core/helper/queryString";
import { PagedList } from "app/core/models/pagedlist";
import { environment } from "environments/environment";
import { GridPostData } from "./models/gridPostData";
import * as $ from "jquery";
import { GridSettingsDTO } from "./models/gridSettingsDTO";
import { GridPropertyInfo } from "./models/gridPropertyInfo";
import { ValueText } from "./models/valueText";
import { FilterModel } from "./models/filterModel";
import { AlertifyService } from "app/core/services/alertify.service";


@Component({
  selector: "app-customgrid",
  templateUrl: "./customgrid.component.html",
  styleUrls: ["./customgrid.component.css"],
})
export class CustomGridComponent implements OnInit {
  @Input() url: string;
  @Input() btnColumnOn:Boolean=false;
  @Input() editBtnUrl:string;
  @Input() deleteBtnUrl:string;
  @ViewChild("prevBtn") prevBtn: ElementRef<HTMLElement>;
  @ViewChild("nextBtn") nextBtn: ElementRef<HTMLElement>;
  datas: object[];
  totalPages: number;
  goToPageIndex: number;
  gridSettingsDTO: GridSettingsDTO = new GridSettingsDTO();
  keys: GridPropertyInfo[];
  filters:ValueText[];
  propertyName:string;
  filterType:string;
  filter:string;
  filterPostJson:string;
  filterModelList:FilterModel[]=[];
  andOrSelected:string;

  gridPostData: GridPostData = new GridPostData(1, 10);

  

  constructor(
    private httpClient: HttpClient,
    private elementRef: ElementRef,
    private injector: Injector,
    private alertifyService: AlertifyService
  ) { }

  ngAfterViewInit() { }
  ngOnInit(): void {
    this.getSettingsGrid();
  }
  
  deleteBtn(id: number) {
    let deleteData= this.httpClient.request('delete', environment.getApiUrl + this.deleteBtnUrl, { body: { id: id } }).subscribe(data => {
			this.alertifyService.success(data.toString());
      this.getAllList();
		});
  }

  propertyList(data:GridPropertyInfo[]){
      let splitData = this.gridSettingsDTO.propertyInfo.split(",") ?? new Array(0);
      
      this.keys = data.filter(function (propertInfo) {
        let IsContain = splitData.filter(x => x == propertInfo.propertyName) ;
        if (IsContain.length > 0){
          propertInfo.checked=true;
          return propertInfo;
        }
        else{
          propertInfo.checked=false;
          return propertInfo;
        }         
      });
  }
  getAllList() {
    this.httpClient
      .post<PagedList<object>>(environment.getApiUrl + this.url ,this.gridPostData)
      .subscribe((data) => {
        this.datas = data.data;
        this.propertyList(data.propertyInfos);
        this.totalPages = data.totalPages;
        this.gridPostData.pageIndex = data.pageIndex;
        this.gridPostData.pageSize = data.pageSize;
        this.prevDisabled();
        this.nextDisabled();
     
      });
  }
  getSettingsGrid() {
    this.httpClient.get<GridSettingsDTO>(
      environment.getApiUrl + "/GridSettingses/getbyid" + "?path=" + this.url
    ).subscribe((data) => {    
      this.gridSettingsDTO = data;
      this.getAllList();
    });
  }

  prevClick() {
    if (this.gridPostData.pageIndex >= 1) {
      this.gridPostData.pageIndex = this.gridPostData.pageIndex - 1;
      this.getAllList();
    }
  }
  nextClick() {
    if (this.gridPostData.pageIndex <= this.totalPages) {
      this.gridPostData.pageIndex = this.gridPostData.pageIndex + 1;
      this.getAllList();
    }
  }
  prevDisabled() {
    if (this.gridPostData.pageIndex <= 1) {
      this.prevBtn.nativeElement.className = "prevNextDisabled prev-btn";
    } else {
      this.prevBtn.nativeElement.className = "prev-btn";
    }
  }
  nextDisabled() {
    if (this.gridPostData.pageIndex <= this.totalPages) {
      this.nextBtn.nativeElement.className = "next-btn";
    } else {
      this.nextBtn.nativeElement.className = "prevNextDisabled next-btn";
    }
  }
  pageSizeChange(event: any) {
    this.gridPostData.pageSize = event.target.value;
    this.getAllList();
  }
  changePageIndex() {
    this.gridPostData.pageIndex = this.goToPageIndex;
    this.getAllList();
  }
  changeOrderBy(orderBy: string) {
    if (this.gridPostData.orderByColumnName != undefined && this.gridPostData.orderByColumnName.indexOf("desc") != -1) {
      this.gridPostData.orderByColumnName = undefined;
    } else {     
      let orderByPropertyName = orderBy.charAt(0).toUpperCase() + orderBy.slice(1);
      this.gridPostData.orderByColumnName = orderByPropertyName + ",desc";
    }
    this.getAllList();
  }

  changeCheckBoxProperty(data: string) {
    if (this.gridSettingsDTO.propertyInfo) {
      let splitData = this.gridSettingsDTO.propertyInfo.split(",");
      let isContain = splitData.indexOf(data);
      if (isContain == -1) {
        this.gridSettingsDTO.propertyInfo = this.gridSettingsDTO.propertyInfo + "," + data;
      }
      else {
        splitData = splitData.filter(x => x != data);
        let joinData = splitData.join(",");
        if (splitData.length > 0)
          this.gridSettingsDTO.propertyInfo = joinData;
        else
          this.gridSettingsDTO.propertyInfo = joinData.substring(0, data.length - 1)
      }
    }
    else{
      this.gridSettingsDTO.propertyInfo = data;
    }
    this.updateGridSettings();
  }

  updateGridSettings() {
    this.httpClient.put(
      environment.getApiUrl + "/GridSettingses/",this.gridSettingsDTO, { responseType: 'text' }).subscribe((data) => {     
         this.propertyList(this.keys);
      });
  }

  changeFilter(event){
    this.propertyName=$(event.target[event.target.selectedIndex]).text().trim();
    let propertyType=event.target.value;
    let filter:ValueText[]=[];

    switch (propertyType) {
      case "Int32" :
        filter.push({value:"1",text:"Eşit"})
        filter.push({value:"2",text:"Eşit Değil"})
        filter.push({value:"5",text:"Büyüktür"})
        filter.push({value:"6",text:"Küçüktür"})
      break;
      case "String":
        filter.push({value:"1",text:"Eşit"})
        filter.push({value:"2",text:"Eşit Değil"})
        filter.push({value:"3",text:"İçerir"})
      break;
      case "DateTime":
        filter.push({value:"1",text:"Eşit"})
      break; 
      case "String":
        filter.push({value:"true",text:"Evet"})
        filter.push({value:"false",text:"Hayır"})
      break;
      case "Json":
        filter.push({value:"3",text:"İçerir"})
      break;
      case "Xml":
        filter.push({value:"3",text:"İçerir"})
      break;
    } 

    this.filters=filter;
  }


  addFilter(){
    let IsContainName=this.keys.filter(x=>x.propertyName==this.propertyName)[0].attrFilterName;
    let filterName=IsContainName ?? this.propertyName;

    let filterModel:FilterModel=new FilterModel();
    filterModel.filter=this.filter;
    filterModel.filterType=this.filterType;
    filterModel.propertyName=filterName;
    filterModel.jsonOrXml=IsContainName?true:false;
    filterModel.andOrOperation=this.andOrSelected;
    
    this.filterModelList.push(filterModel);
    this.gridPostData.filterModelList=this.filterModelList;
    this.getAllList();
  }

  deleteFilter(deleteFilter:FilterModel){
    this.filterModelList=this.filterModelList.filter(x=>x!=deleteFilter);
    this.gridPostData.filterModelList=this.filterModelList;

    if(this.filterModelList.length >= 1)
    this.filterModelList[0].andOrOperation="";

    this.getAllList();
  }




}
