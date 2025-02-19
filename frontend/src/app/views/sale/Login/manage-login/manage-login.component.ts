import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router,RouterModule } from '@angular/router';
import Swal from 'sweetalert2';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { SaleService } from '../../sale.service';
import { LoginVM } from '../../models/LoginVM';



@Component({
  selector: 'app-manage-login',
  templateUrl: './manage-login.component.html',
  styleUrls: ['./manage-login.component.css']
})
export class ManageLoginComponent {
  //userId = 0;
  info = new LoginVM;
  hide = true;
  login: LoginVM = new LoginVM
  @ViewChild('userForm', { static: true }) userForm!: NgForm;
  dialogRef: any;
  dataSource: any;
  responseData: any;
  constructor(
    public serSvc: SaleService,
    public dialog: MatDialog,
    private route: Router,
  ) {

  }
  ngOnInit(): void {
    //const userId = '0a714c07-6881-4740-8bcb-5a6bfd833eda';
    //this.GetTaskByUserId(userId);
  }
  Login() {
    // this.login.userName = this.login.password;
    this.serSvc.Login(this.login).subscribe({
      next: (data: any) => {
        console.warn(data.succeeded)
        console.warn(data?.result?.succeeded)
        debugger
        //this.userId = data?.id;
        this.responseData = data;

        if (data?.result?.succeeded == true) {
          localStorage.setItem("ClientId", "1");
          localStorage.setItem('userId', this.responseData.id);
          
          // Swal.fire({
          //   position: 'center',
          //   title: 'Wellcome to QamSoft Technologies ',
          //   background: "#FFFFFF",
          //   confirmButtonColor: "#000000",
          //   width: 600
          // })
          // localStorage.setItem("Token", data.token)
          // this.route.navigate(['/catalog/manageSetting']);
        }
        else {
          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Please ReCheck  User Name Or Password ',
            confirmButtonColor: "#000000"
          })
        }
      }, error: (err) => {
        if (err.status == 400) {
          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Invalid Request! ',
            footer: 'Please ReCheck  User Name Or Password',
            confirmButtonColor: "#000000"
          })
        }
        else if (err.status == 0) {
          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Something went wrong! ',
            footer: 'Please Check your Internet Connection',
            confirmButtonColor: "#000000"
          })
        } else {
          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Please ReCheck  User Name Or Password ',
            confirmButtonColor: "#000000"
          })
        }
      }
    })
  }
}
