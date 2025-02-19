import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-otp',
  templateUrl: './otp.component.html',
  styleUrls: ['./otp.component.scss']
})
export class OtpComponent {

 OtpForm: FormGroup;
  submitted = false;
  displayedColumns = ['otp', 'action'];

  constructor(private formBuilder: FormBuilder) {
    this.OtpForm = this.formBuilder.group({
      password: ['', [Validators.required, Validators.email]]
    });
  }

  // Convenience getter for easy access to form fields
  get f() { return this.OtpForm.controls; }

  onSubmit() {
    this.submitted = true;

    // Stop here if form is invalid
    if (this.OtpForm.invalid) {
      return;
    }

    // Display the form values for demonstration purposes
    console.log('OTP Submitted:', this.OtpForm.value);

    // Here you can implement the logic to send a password reset email
    // For example, you can make an HTTP request to your backend API
    // to handle the forgot password functionality
  }
}