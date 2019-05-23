import { Injectable, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Injectable({
  providedIn: 'root'
})
export class ModalService {

  constructor(public dialogRef: MatDialogRef<any>,
    @Inject(MAT_DIALOG_DATA) public data: any, public dialog: MatDialog) { }

    openDialog(componentOrTemplateRef: any, data: any, width = '600px'): any {
      return this.dialog.open(componentOrTemplateRef, {
          data: data,
          width: width
      }).afterClosed();
    }
}

