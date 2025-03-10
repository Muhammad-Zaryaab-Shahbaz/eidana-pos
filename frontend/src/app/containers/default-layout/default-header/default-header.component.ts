import { Component, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { HeaderComponent } from '@coreui/angular';

@Component({
  selector: 'app-default-header',
  templateUrl: './default-header.component.html',
})
export class DefaultHeaderComponent extends HeaderComponent {
  dialogRef: any;

  constructor(public dialog: MatDialog,
    private route: Router,) {
    super();
  }

  OpenUserDialog() {
    
  }

  break(){
    localStorage.clear();
    this.route.navigate(['/secLogin']);
  }

  LogOut() {
    // Open user dialog before logout
    this.OpenUserDialog();


    // LogOut logic after dialog closure
    // this.dialogRef.afterClosed().subscribe(() => {
      // console.error("err");
      // localStorage.clear();
      // Navigate to the logout page (replace '/logout' with your actual logout page URL)
      // window.location.href = '//secLogin';
      // this.route.navigate(['/secLogin']);
      // this.dialogRefe.close();
    // });
  }

  @Input() sidebarId: string = "sidebar";

  public newMessages = new Array(4)
  public newTasks = new Array(5)
  public newNotifications = new Array(5)
}
