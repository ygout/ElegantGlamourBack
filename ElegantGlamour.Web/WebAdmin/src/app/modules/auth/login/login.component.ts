import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IUser } from 'src/app/core/models/User';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  userForm: FormGroup;
  loading = false;
  submitted = false;
  constructor(
    private authService: AuthService,
    private formBuilder: FormBuilder
  ) {
    this.userForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  ngOnInit(): void {}

  // convenience getter for easy access to form fields
  get f(): any {
    return this.userForm.controls;
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.userForm.invalid) {
      console.error('Form invalid');
      return;
    }
    const email = this.userForm.get('username').value as string;
    const password = this.userForm.get('password').value as string;
    console.log('username ', email);
    console.log('password ', password);

    this.authService.login({ email, password }).subscribe({
      next: (user: IUser) => {
        console.log('user', user);
      },
      error: (error: Error) => {
        console.log('error', error);
        throw error;
      },
    });
  }
}
