import { SaleRoutingModule } from './sale-routing.module';
import { ManageSaleInvoiceComponent } from './SaleInvoice/manage-sale-invoice/manage-sale-invoice.component';
import { ListSaleInvoiceComponent } from './SaleInvoice/list-sale-invoice/list-sale-invoice.component';
import { CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule, DatePipe, Location } from '@angular/common';
import { NgxMatDatetimePickerModule, NgxMatNativeDateModule } from '@angular-material-components/datetime-picker';
import { HttpClientModule } from '@angular/common/http';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; 
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatPaginatorModule } from '@angular/material/paginator';
 import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { AccordionModule, BadgeModule, BreadcrumbModule, ButtonModule, CardModule, CollapseModule, GridModule, UtilitiesModule, SharedModule, ListGroupModule, PlaceholderModule, ProgressModule, SpinnerModule, TabsModule, NavModule, TooltipModule, CarouselModule, FormModule, DropdownModule, PaginationModule, PopoverModule, ModalModule, TableModule } from '@coreui/angular';
import { IconModule } from '@coreui/icons-angular';
import { NgxMatTimepickerModule } from 'ngx-mat-timepicker';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatMenuModule } from '@angular/material/menu';
import { ManageItemsComponent } from './Items/manage-items/manage-items.component';
// import { ManageLoginComponent } from './Login/manage-login/manage-login.component';
import { BarcodeGeneratorComponent } from './barcode-generator/barcode-generator.component';
import { SaleInvoicePrintComponent } from './sale-invoice-print/sale-invoice-print.component';
//import { PosSaleInvoiceComponent } from './pos-sale-invoice/pos-sale-invoice.component';

@NgModule({
  declarations: [  
    ManageSaleInvoiceComponent,
    ListSaleInvoiceComponent,
    ManageItemsComponent,
    // ManageLoginComponent,
    BarcodeGeneratorComponent,
    SaleInvoicePrintComponent,
    //PosSaleInvoiceComponent
  ],
  imports: [
    NgxMatDatetimePickerModule,
    MatMomentDateModule,
    MatMenuModule,
    MatInputModule,
    NgxMaterialTimepickerModule,
   // NgxMaterialTimepickerModule,
    NgxMatNativeDateModule,
    FlexLayoutModule,
    FormsModule,
    MatIconModule,
    MatDialogModule,
    MatSelectModule,
    MatNativeDateModule,
    MatMomentDateModule,
    MatTableModule,
    MatDialogModule,
    MatAutocompleteModule,
    MatFormFieldModule,
    MatSortModule,
    SaleRoutingModule,
    MatButtonModule,
    
    MatSnackBarModule,
    HttpClientModule,
    FlexLayoutModule,
    FormsModule,
    MatTooltipModule,
    
    MatListModule,
    MatCardModule,
    MatDatepickerModule,
   
    MatProgressBarModule,
    MatRadioModule,
    MatSelectModule,
    MatCheckboxModule,
    NgxMatTimepickerModule,
    MatButtonModule,
    MatIconModule,
    MatStepperModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    ReactiveFormsModule,
    CommonModule,
    AccordionModule,
    BadgeModule,
    BreadcrumbModule,
    ButtonModule,
    CardModule,
    CollapseModule,
    GridModule,
    UtilitiesModule,
    SharedModule,
    ListGroupModule,
    IconModule,
    ListGroupModule,
    PlaceholderModule,
    ProgressModule,
    SpinnerModule,
    TabsModule,
    NavModule,
    TooltipModule,
    CarouselModule,
    FormModule,
    DropdownModule,
    PaginationModule,
    PopoverModule,
    ModalModule,
    TableModule,
    MatToolbarModule,
    MatProgressSpinnerModule
  ],

  providers: [DatePipe  ,MatNativeDateModule, Location],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
})
export class SaleModule { }
