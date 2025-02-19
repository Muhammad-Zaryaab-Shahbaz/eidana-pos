import { Component, Injector, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { SecurityService } from '../../security.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { UserAdVM } from '../../models/UserAdVM';
import { UserVM } from '../../models/user-vm';
import { MatTableDataSource } from '@angular/material/table';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-users-addition-fields',
  templateUrl: './users-addition-fields.component.html',
  styleUrls: ['./users-addition-fields.component.scss']
})
export class UsersAdditionFieldsComponent {

  /* displayedColumns: string[] = ['statusId','inquiryId', 'date',  'nextAppointmentDate', 'comment', 'isActive', 'actions']; */

  displayedColumns: string[] = ['studentName','guardianName', 'guardianRelationship',  'guardianProfession', 'degree','university','cNIC','joiningDate','address', 'isActive', 'actions'];
  AddMode: boolean = true
  EditMode: boolean = false
  Add: boolean = true;
  Edit: boolean = false;
  dataSource: any
  DataSource: any
  dialogData: any;
  users?: UserAdVM[];
  selectedUser: UserAdVM 
  snack: any;
  proccessing: boolean;
dialogRefe :any;
  dialogRef: any;
  
  constructor(  
   
    public serSvc: SecurityService,
    private injector: Injector,
    private catSvc: CatalogService,
    public isDialog: MatDialog) 
    {
      this.selectedUser = new UserAdVM
   
      this.dialogRefe = this.injector.get(MatDialogRef, null);
      this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);

  }
   


  ngOnInit(): void {
    if (this.dialogData)
      if (this.dialogData.id != undefined) {
        
        this.selectedUser.id = this.dialogData.id
       
       
      }

    this.GetUser();
   
    this.selectedUser.isActive = true;
   
  }


  GetUser() {
    this.serSvc.GetUser().subscribe({
      next: (value: UserAdVM[]) => {
        debugger;
        this.users = value
        this.DataSource = new MatTableDataSource(this.users)
      }, error: (err) => {
        alert('Error to retrieve User');
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
      },
    })
  }

  SaveUser() {
    this.serSvc.SaveUser(this.selectedUser).subscribe({
      next: (value) => {
        this.catSvc.SuccessMsgBar("Successfully Added", 5000)
        this.Refresh();
      }, error: (err) => {
        console.warn(err)
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
      },
    })
  }
  EditUser(user: UserAdVM) {
    this.EditMode = true
    this.AddMode = false
    this.selectedUser = user
  }
  GetuserForEdit(id: string) {
    this.selectedUser = new UserAdVM;
    this.selectedUser.id = id
    console.warn(this.selectedUser);
    this.serSvc.SearchUser(this.selectedUser).subscribe({
      next: (res: UserAdVM[]) => {
        this.users = res;
        console.warn(this.users);
        this.selectedUser = this.users[0]
        this.Edit = true;
        this.Add = false;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
      }
    })
  }
  UpdateUser() {
    debugger
    this.serSvc.UpdateUser(this.selectedUser).subscribe({
      next: (res) => {

        this.catSvc.SuccessMsgBar("User Successfully Updated!", 5000)
        this.Add = true;
        this.Edit = false;
        this.proccessing = false
        this.ngOnInit();
      }, error: (e) => {
        this.catSvc.ErrorMsgBar("Error Occurred", 5000)
        console.warn(e);
        this.proccessing = false
      }
    })
    this.proccessing = false
  }
  Refresh() {
    this.Add = true;
    this.Edit = false;
  }
  DeleteUser(id: string) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.value) {
        this.serSvc.DeleteUser(id).subscribe({
          next: (data) => {
            Swal.fire(
              'Deleted!',
              'FollowUp has been deleted.',
              'success'
            )
            this.Refresh();
          }, error: (e) => {
            this.catSvc.ErrorMsgBar("Error Occurred", 5000)
            console.warn(e);
          }
        })
      }
    })
  }

}

