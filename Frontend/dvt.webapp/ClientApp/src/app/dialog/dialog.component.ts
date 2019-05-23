import { Component, Inject, ViewChild, TemplateRef, ComponentFactoryResolver, ViewContainerRef, ComponentRef, OnDestroy,OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.css']
})
export class DialogComponent implements OnInit, OnDestroy {

  @ViewChild('target', {read:ViewContainerRef}) vcRef:ViewContainerRef;
  componentRef: ComponentRef<any>;

  constructor(
    public dialogRef: MatDialogRef<DialogComponent>,
    private resolver: ComponentFactoryResolver,
    @Inject(MAT_DIALOG_DATA) public data: any){}
   

  ngOnInit() {
    const factory = this.resolver.resolveComponentFactory(this.data.component);
    this.componentRef = this.vcRef.createComponent(factory);
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
  
  ngOnDestroy() {
    if (this.componentRef) {
      this.componentRef.destroy();
    }
  }

}
