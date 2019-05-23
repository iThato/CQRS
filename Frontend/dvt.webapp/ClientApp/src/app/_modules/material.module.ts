import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import * as Material from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    Material.MatToolbarModule,
    Material.MatGridListModule,
    Material.MatInputModule,
    Material.MatFormFieldModule,
    Material.MatSelectModule,
    BrowserAnimationsModule,
    Material.MatButtonModule,
    Material.MatDialogModule,
    Material.MatIconModule,
    Material.MatCheckboxModule,
  //  Material.MatTableModule,
    Material.MatSortModule,
    Material.MatPaginatorModule,
    Material.MatDialogModule,
    Material.MatCardModule
  ],
  exports: [Material.MatToolbarModule,
    Material.MatGridListModule,
    Material.MatInputModule,
    Material.MatFormFieldModule,
    BrowserAnimationsModule,
    Material.MatSelectModule,
    Material.MatIconModule,
    Material.MatButtonModule,
    Material.MatCheckboxModule,
    Material.MatDialogModule,
   // Material.MatTableModule,
    Material.MatSortModule,
    Material.MatPaginatorModule,
    Material.MatDialogModule,
    Material.MatCardModule]
})
export class MaterialModule { }
